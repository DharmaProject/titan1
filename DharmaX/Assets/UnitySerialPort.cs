// <copyright file="UnitySerialPort.cs" company="dyadica.co.uk">
// Copyright (c) 2010, 2014 All Right Reserved, http://www.dyadica.co.uk

// This source is subject to the dyadica.co.uk Permissive License.
// Please see the http://www.dyadica.co.uk/permissive-license file for more information.
// All other rights reserved.

// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// </copyright>

// <author>SJB</author>
// <email>SJB@dyadica.co.uk</email>
// <date>04.09.2013</date>
// <summary>A MonoBehaviour type class containing several functions which can be utilised 
// to perform serial communication within Unity3D</summary>

using UnityEngine;
using System.Collections;

using System.IO;
using System.IO.Ports;
using System;

using System.Threading;

public class UnitySerialPort : MonoBehaviour
{
    // Init a static reference if script is to be accessed by others when used in a 
    // none static nature eg. its dropped onto a gameObject. The use of "Instance"
    // allows access to public vars as such as those available to the unity editor.
    public static UnitySerialPort Instance;
    int totalLitros = 7;
    float litrosLeft = 0;
    public double porcentaje = 0;
    public Renderer sombra;

    public TextMesh txtMesh;

    string[] botones;
    float[] valor;
    public GameObject[] boton;
    public GameObject[] planetas;
    public float speed;
    public Renderer[] color;
    string dharma;
    bool spaceGPSestado = false;
    bool missionControlEstado = false;
    bool compoundAnalyzerEstado = false;
    bool ArmRobotEstado = false;
    bool musicPlayerEstado = false;
    bool biometricsEstado = false;
    float radius = 2f;
    float theta;
    public GameObject selector;
    float posicionXselector;
    float posicionYselector;
    float posicionZselector;
    float scaleXselector;
    float scaleYselector;
    float scaleZselector;
    int index;

    //public Transform text;

    public TextMesh temperatura;
    public Transform title;
    public Transform nombresPlanetas;
    public GameObject fondoNombresPlanetas;
    public Transform atmosfera;
    public Transform textoAtmosfera;
    public Transform diameter;
    public Transform textoDiametro;
    public Transform nucleo;
    public Transform textoNucleo;
    public Transform fondoInfoPlanetas;
    public GameObject[] biometrics;
    public Transform date;
    public Transform Ambient;
    public Transform battery;
    public Transform magnetic;
    public GameObject PhotoID;
    public GameObject fondoPhotoID;
    public GameObject fondoFecha;
    public GameObject fondoAmbient;
    public GameObject fondoMagnetic;
    public GameObject fondoBateria;
    #region Properties

    // The serial port
    public SerialPort SerialPort;

    // The script update can run as either a seperate thread
    // or as a standard coroutine. This can be selected via 
    // the unity editor.

    public enum LoopUpdateMethod
    { Threading, Coroutine }

    // This is the public property made visible in the editor.
    public LoopUpdateMethod UpdateMethod =
        LoopUpdateMethod.Threading;

    // Thread used to recieve and send serial data
    private Thread serialThread;

    // List of all baudrates available to the arduino platform
    private ArrayList baudRates =
        new ArrayList() { 300, 600, 1200, 2400, 4800, 9600, 14400, 19200, 28800, 38400, 57600, 115200 };

    // List of all com ports available on the system
    private ArrayList comPorts =
        new ArrayList();

    // If set to true then open the port when the start
    // event is called.
    public bool OpenPortOnStart = false;

    // Holder for status report information
    private string portStatus = "";
    public string PortStatus
    {
        get { return portStatus; }
        set { portStatus = value; }
    }

    // Current com port and set of default
    public string ComPort = "COM8";

    // Current baud rate and set of default
    public int BaudRate = 9600;

    public int ReadTimeout = 10;

    public int WriteTimeout = 10;

    // Property used to run/keep alive the serial thread loop
    private bool isRunning = false;
    public bool IsRunning
    {
        get { return isRunning; }
        set { isRunning = value; }
    }

    // Set the gui to show ready
    private string rawData = "Ready";
    public string RawData
    {
        get { return rawData; }
        set { rawData = value; }
    }

    // Storage for parsed incoming data
    private string[] chunkData;
    public string[] ChunkData
    {
        get { return chunkData; }
        set { chunkData = value; }
    }

    // Refs populated by the editor inspector for default gui
    // functionality if script is to be used in a non-static
    // context.
    //public GameObject ComStatusText;

    //public GameObject RawDataText;

    

    #endregion Properties

    #region Unity Frame Events

    /// <summary>
    /// The awake call is used to populate refs to the gui elements used in this 
    /// example. These can be removed or replaced if needed with bespoke elements.
    /// This will not affect the functionality of the system. If we are using awake
    /// then the script is being run non staticaly ie. its initiated and run by 
    /// being dropped onto a gameObject, thus enabling the game loop events to be 
    /// called e.g. start, update etc.
    /// </summary>
    void Awake()
    {
        // Define the script Instance
        Instance = this;

        // If we have used the editor inspector to populate any included gui
        // elements then lets initiate them and set some default values.
        spaceGPSestado = false;
        biometricsEstado = true;
        // Details if the port is open or closed

    }

    void GameObjectSerialPort_DataRecievedEvent(string[] Data, string RawData)
    {
        print("Data Recieved: " + RawData);
    }

    /// <summary>
    /// The start call is used to populate a list of available com ports on the
    /// system. The correct port can then be selected via the respective guitext
    /// or a call to UpdateComPort();
    /// </summary>
    void Start()
    {
        // Population of comport list via system.io.ports
        PopulateComPorts();

        // If set to true then open the port. You must 
        // ensure that the port is valid etc. for this! 
        

        // Biometrics initialization
        date.gameObject.SetActive(true);
        Ambient.gameObject.SetActive(true);
        battery.gameObject.SetActive(true);
        magnetic.gameObject.SetActive(true);
        PhotoID.gameObject.SetActive(true);
        fondoPhotoID.gameObject.SetActive(true);
        fondoFecha.gameObject.SetActive(true);
        fondoAmbient.gameObject.SetActive(true);
        fondoMagnetic.gameObject.SetActive(true);
        fondoBateria.gameObject.SetActive(true);

        //Serial port values
        valor = new float[7];
        if (OpenPortOnStart) { OpenSerialPort(); }
    }

    /// <summary>
    /// The update frame call is used to provide caps for sending data to the arduino
    /// triggered via keypress. This can be replaced via use of the static functions
    /// SendSerialData() & SendSerialDataAsLine(). Additionaly this update uses the
    /// RawData property to update the gui. Again this can be removed etc.
    /// </summary>
    void Update()
    {
        // Check if the serial port exists and is open
        if (SerialPort == null || SerialPort.IsOpen == false) { return; }

        // Example calls from system to the arduino. For more detail on the
        // structure of the calls see: http://www.dyadica.co.uk/journal/simple-serial-string-parsing/
        try
        {
            // Example of sending space press event to arduino
            if (Input.GetKeyDown("space"))
            { SerialPort.WriteLine(""); }

            // Example of sending key 1 press event to arduino.
            // The "A,1" string will call functionA and pass a
            // char value of 1
            if (Input.GetKeyDown(KeyCode.Alpha1))
            { SerialPort.WriteLine("A,1"); }

            // Example of sending key 1 press event to arduino.
            // The "A,2" string will call functionA and pass a
            // char value of 2
            if (Input.GetKeyDown(KeyCode.Alpha2))
            { SerialPort.WriteLine("A,2"); }
        }
        catch (Exception ex)
        {
            // Failed to send serial data
            Debug.Log("Error 6: " + ex.Message.ToString());
        }

        try
        {
            // If we have set a GUI Text object then update it. This can only be
            // run on the thread that initialised the object thus cnnot be run
            // in the ParseSerialData() call below... Unless run as a coroutine!

            // I have also included a raw data example which is called from a
            // seperate script... see RawDataExample.cs
            biometricsPanel();
            spaceGPS();
            botones = RawData.Split(',');
          
            for (int i = 0; i < botones.Length; i++)
            {
                valor[i] = float.Parse(botones[i].ToString());
                
                litrosLeft = totalLitros - float.Parse(botones[0]);
                Debug.Log("litros: " + float.Parse(botones[0]));
                porcentaje = Math.Round(getPercentage(litrosLeft));
                Debug.Log("Porcentaje en litros: " + porcentaje);
                Debug.Log("biometrics estado = " + biometricsEstado);
                Debug.Log("spaceGPS = " + spaceGPSestado);
                txtMesh.color = Color.white;
                temperatura.text = botones[1] + " °C";
                    //sombra.material.shader = Shader.Find("Particles/Additive");
                    if (porcentaje >= 35)
                    {

                        sombra.material.SetColor("_Color", Color.cyan);
                        txtMesh.text = "Oxygen: " + porcentaje.ToString() + " %";
                    }
                    if (porcentaje <= 35)
                    {

                        sombra.material.SetColor("_Color", Color.yellow);
                        txtMesh.text = "Oxygen: " + porcentaje.ToString() + " %";
                    }
                    if (porcentaje <= 15)
                    {

                        sombra.material.SetColor("_Color", Color.red);
                        txtMesh.text = "Oxygen: " + porcentaje.ToString() + " %";
                    }
                    if (porcentaje <= 0)
                    {
                        porcentaje = 0;
                        sombra.material.SetColor("_Color", Color.black);
                        txtMesh.text = "Oxygen: " + porcentaje.ToString() + " %";
                    }
                
                    if (float.Parse(botones[2]) < 160)
                    {
                        Debug.Log("primer boton - space gps");
                        color[0].material.SetColor("_Color", Color.green);
                        color[1].material.SetColor("_Color", Color.white);
                        color[2].material.SetColor("_Color", Color.white);
                        color[3].material.SetColor("_Color", Color.white);
                        color[4].material.SetColor("_Color", Color.white);

                        spaceGPSestado = true;
                        biometricsEstado = false;
                        missionControlEstado = false;
                        ArmRobotEstado = false;
                        compoundAnalyzerEstado = false;
                        musicPlayerEstado = false;
                        title.GetComponent<TextMesh>().text = "Space gps";

                    }
                    if (float.Parse(botones[3]) < 160)
                    {
                        Debug.Log("segundo boton - mission control");
                        color[0].material.SetColor("_Color", Color.white);
                        color[1].material.SetColor("_Color", Color.green);
                        color[2].material.SetColor("_Color", Color.white);
                        color[3].material.SetColor("_Color", Color.white);
                        color[4].material.SetColor("_Color", Color.white);


                        if (!spaceGPSestado)
                        {
                            spaceGPSestado = false;
                            missionControlEstado = true;
                        }
                        
                        biometricsEstado = false;
                        ArmRobotEstado = false;
                        compoundAnalyzerEstado = false;
                        musicPlayerEstado = false;
                        title.GetComponent<TextMesh>().text = "Mission Control";

                    }
                    if (float.Parse(botones[4]) < 160)
                    {
                        Debug.Log("tercer boton - toolbox");
                        color[0].material.SetColor("_Color", Color.white);
                        color[1].material.SetColor("_Color", Color.white);
                        color[2].material.SetColor("_Color", Color.green);
                        color[3].material.SetColor("_Color", Color.white);
                        color[4].material.SetColor("_Color", Color.white);

                        spaceGPSestado = false;
                        biometricsEstado = false;
                        ArmRobotEstado = true;
                        missionControlEstado = false;
                        compoundAnalyzerEstado = false;
                        musicPlayerEstado = false;
                    title.GetComponent<TextMesh>().text = "ToolBox";

                    }
                    if (float.Parse(botones[5]) < 160)
                    {
                        Debug.Log("quinto botón - compound analyzer");
                        color[1].material.SetColor("_Color", Color.white);
                        color[0].material.SetColor("_Color", Color.white);
                        color[2].material.SetColor("_Color", Color.white);
                        color[3].material.SetColor("_Color", Color.green);
                        color[4].material.SetColor("_Color", Color.white);

                        spaceGPSestado = false;
                        ArmRobotEstado = false;
                        missionControlEstado = false;
                        musicPlayerEstado = false;
                        biometricsEstado = false;
                        compoundAnalyzerEstado = true;
                        title.GetComponent<TextMesh>().text = "Compound analyzer";
                    }
                    if (float.Parse(botones[6]) < 160)
                    {
                        Debug.Log("sexto botón - music player");
                        color[1].material.SetColor("_Color", Color.white);
                        color[0].material.SetColor("_Color", Color.white);
                        color[2].material.SetColor("_Color", Color.white);
                        color[3].material.SetColor("_Color", Color.white);
                        color[4].material.SetColor("_Color", Color.green);

                        spaceGPSestado = false;
                        missionControlEstado = false;
                        ArmRobotEstado = false;
                        compoundAnalyzerEstado = false;
                        biometricsEstado = false;
                        musicPlayerEstado = true;
                        title.GetComponent<TextMesh>().text = "Music player";
                    }
                    if (float.Parse(botones[2]) > 160 && float.Parse(botones[3]) > 160 && float.Parse(botones[4]) > 160 && float.Parse(botones[5]) > 160 && float.Parse(botones[6]) > 160)
                    {
                        Debug.Log("valores default");
                        color[1].material.SetColor("_Color", Color.white);
                        color[0].material.SetColor("_Color", Color.white);
                        color[2].material.SetColor("_Color", Color.white);
                        color[3].material.SetColor("_Color", Color.white);
                        color[4].material.SetColor("_Color", Color.white);

                        /*
                        spaceGPSestado = false;
                        missionControlEstado = false;
                        ArmRobotEstado = false;
                        compoundAnalyzerEstado = false;
                        biometricsEstado = true;*/
                        biometricsEstado = true;
                        title.GetComponent<TextMesh>().text = "Biometrics";
                        //temperatura.GetComponent<TextMesh>().text = "Temperatura: " + valor[6].ToString();
                    }

                if (spaceGPSestado)
                {
                    title.GetComponent<TextMesh>().text = "SpaceGPS";
                }
                        
                if (biometricsEstado && spaceGPSestado==false) {
                    title.GetComponent<TextMesh>().text = "Biometrics";
                }
                if (missionControlEstado && spaceGPSestado==false) {
                    title.GetComponent<TextMesh>().text = "Mission Control";
                }
                if (ArmRobotEstado) {
                    title.GetComponent<TextMesh>().text = "ToolBox";
                }
                if (compoundAnalyzerEstado) {
                    title.GetComponent<TextMesh>().text = "Compound Analyzer";
                }
                if (musicPlayerEstado) {
                    title.GetComponent<TextMesh>().text = "Music Player";
                }
            }
            
        }
        catch (Exception ex)
        {
            // Failed to update serial data
            Debug.Log("Error 7: " + ex.Message.ToString());
        }
    }

    /// <summary>
    /// Clean up the thread and close the port on application close event.
    /// </summary>
    void OnApplicationQuit()
    {
        // Call to cloase the serial port
        CloseSerialPort();

        Thread.Sleep(500);

        if (UpdateMethod == LoopUpdateMethod.Threading)
        {
            // Call to end and cleanup thread
            StopSerialThread();
        }

        if (UpdateMethod == LoopUpdateMethod.Coroutine)
        {
            // Call to end and cleanup coroutine
            StopSerialCoroutine();
        }

        Thread.Sleep(500);
    }

    #endregion Unity Frame Events

    #region Object Serial Port

    /// <summary>
    /// Opens the defined serial port and starts the serial thread used
    /// to catch and deal with serial events.
    /// </summary>
    public void OpenSerialPort()
    {
        try
        {
            // Initialise the serial port
            SerialPort = new SerialPort(ComPort, BaudRate);

            SerialPort.ReadTimeout = ReadTimeout;

            SerialPort.WriteTimeout = WriteTimeout;

            // Open the serial port
            SerialPort.Open();

            // Update the gui if applicable


            if (UpdateMethod == LoopUpdateMethod.Threading)
            {
                // If the thread does not exist then start it
                if (serialThread == null)
                { StartSerialThread(); }
            }

            if (UpdateMethod == LoopUpdateMethod.Coroutine)
            {
                if (isRunning == false)
                {
                    StartSerialCoroutine();
                }
                else
                {
                    isRunning = false;

                    // Give it chance to timeout
                    Thread.Sleep(100);

                    try
                    {
                        // Kill it just in case
                        StopCoroutine("SerialCoroutineLoop");
                    }
                    catch (Exception ex)
                    {
                        print("Error N: " + ex.Message.ToString());
                    }

                    // Restart it once more
                    StartSerialCoroutine();
                }
            }

            print("SerialPort successfully opened!");

        }
        catch (Exception ex)
        {
            // Failed to open com port or start serial thread
            Debug.Log("Error 1: " + ex.Message.ToString());
        }
    }

    /// <summary>
    /// Cloases the serial port so that changes can be made or communication
    /// ended.
    /// </summary>
    public void CloseSerialPort()
    {
        try
        {
            // Close the serial port
            SerialPort.Close();
        }
        catch (Exception ex)
        {
            if (SerialPort == null || SerialPort.IsOpen == false)
            {
                // Failed to close the serial port. Uncomment if
                // you wish but this is triggered as the port is
                // already closed and or null.

                // Debug.Log("Error 2A: " + "Port already closed!");
            }
            else
            {
                // Failed to close the serial port
                Debug.Log("Error 2B: " + ex.Message.ToString());
            }
        }

        print("Serial port closed!");
    }

    #endregion Object Serial Port

    #region Serial Coroutine

    /// <summary>
    /// Function used to start coroutine for reading serial 
    /// data.
    /// </summary>
    public void StartSerialCoroutine()
    {
        isRunning = true;

        StartCoroutine("SerialCoroutineLoop");
    }

    /// <summary>
    /// A Coroutine used to recieve serial data thus not 
    /// affecting generic unity playback etc.
    /// </summary>
    public IEnumerator SerialCoroutineLoop()
    {
        while (isRunning)
        {
            GenericSerialLoop();
            yield return null;
        }

        print("Ending Coroutine!");
    }

    /// <summary>
    /// Function used to stop the coroutine and kill
    /// off any instance
    /// </summary>
    public void StopSerialCoroutine()
    {
        isRunning = false;

        Thread.Sleep(100);

        try
        {
            StopCoroutine("SerialCoroutineLoop");
        }
        catch (Exception ex)
        {
            print("Error 2A: " + ex.Message.ToString());
        }

        // Reset the serial port to null
        if (SerialPort != null)
        { SerialPort = null; }

        // Update the port status... just in case :)
        portStatus = "Ended Serial Loop Coroutine!";

        print("Ended Serial Loop Coroutine!");
    }

    #endregion Serial Coroutine

    #region Serial Thread

    /// <summary>
    /// Function used to start seperate thread for reading serial 
    /// data.
    /// </summary>
    public void StartSerialThread()
    {
        try
        {
            // define the thread and assign function for thread loop
            serialThread = new Thread(new ThreadStart(SerialThreadLoop));
            // Boolean used to determine the thread is running
            isRunning = true;
            // Start the thread
            serialThread.Start();

            print("Serial thread started!");
        }
        catch (Exception ex)
        {
            // Failed to start thread
            Debug.Log("Error 3: " + ex.Message.ToString());
        }
    }

    /// <summary>
    /// The serial thread loop. A Seperate thread used to recieve
    /// serial data thus not affecting generic unity playback etc.
    /// </summary>
    private void SerialThreadLoop()
    {
        while (isRunning)
        { GenericSerialLoop(); }

        print("Ending Thread!");
    }

    /// <summary>
    /// Function used to stop the serial thread and kill
    /// off any instance
    /// </summary>
    public void StopSerialThread()
    {
        // Set isRunning to false to let the while loop
        // complete and drop out on next pass
        isRunning = false;

        // Pause a little to let this happen
        Thread.Sleep(100);

        // If the thread still exists kill it
        // A bit of a hack using Abort :p
        if (serialThread != null)
        {
            serialThread.Abort();
            // serialThread.Join();
            Thread.Sleep(100);
            serialThread = null;
        }

        // Reset the serial port to null
        if (SerialPort != null)
        { SerialPort = null; }

        // Update the port status... just in case :)
        portStatus = "Ended Serial Loop Thread";

        print("Ended Serial Loop Thread!");
    }

    #endregion Serial Thread 

    #region Static Functions

    /// <summary>
    /// Function used to send string data over serial with
    /// an included line return
    /// </summary>
    /// <param name="data">string</param>
    public void SendSerialDataAsLine(string data)
    {
        if (SerialPort != null)
        { SerialPort.WriteLine(data); }

        print("Sent data: " + data);
    }

    /// <summary>
    /// Function used to send string data over serial without
    /// a line return included.
    /// </summary>
    /// <param name="data"></param>
    public void SendSerialData(string data)
    {
        if (SerialPort != null)
        { SerialPort.Write(data); }

        print("Sent data: " + data);
    }

    #endregion Static Functions

    /// <summary>
    /// The serial thread loop & the coroutine loop both utilise
    /// the same code with the exception of the null return on 
    /// the coroutine, so we share it here.
    /// </summary>
    private void GenericSerialLoop()
    {
        try
        {
            // Check that the port is open. If not skip and do nothing
            if (SerialPort.IsOpen)
            {
                // Read serial data until a '\n' character is recieved
                string rData = SerialPort.ReadLine();

                // If the data is valid then do something with it
                if (rData != null && rData != "")
                {
                    // Store the raw data
                    RawData = rData;
                    // split the raw data into chunks via ',' and store it
                    // into a string array
                    ChunkData = RawData.Split(',');

                    // Or you could call a function to do something with
                    // data e.g.
                    ParseSerialData(ChunkData, RawData);
                }
            }
        }
        catch (TimeoutException timeout)
        {
            // This will be triggered lots with the coroutine method
        }
        catch (Exception ex)
        {
            // This could be thrown if we close the port whilst the thread 
            // is reading data. So check if this is the case!
            if (SerialPort.IsOpen)
            {
                // Something has gone wrong!
                Debug.Log("Error 4: " + ex.Message.ToString());
            }
            else
            {
                // Error caused by closing the port whilst in use! This is 
                // not really an error but uncomment if you wish.

                // Debug.Log("Error 5: Port Closed Exception!");
            }
        }
    }

    /// <summary>
    /// Function used to filter and act upon the data recieved. You can add
    /// bespoke functionality here.
    /// </summary>
    /// <param name="data">string[] of raw data seperated into chunks via ','</param>
    /// <param name="rawData">string of raw data</param>
    private void ParseSerialData(string[] data, string rawData)
    {
        // Examples of reading a value from the recieved data
        // for use if required - remove or replase with bespoke
        // functionality etc

        if (data.Length == 2)
        { int ReceviedValue = int.Parse(data[1]); }
        else { print(rawData); }

        if (data == null || data.Length != 2)
        { print(rawData); }

        // The following can be run if the code is run via the coroutine method.

        //if (RawDataText != null)
        //    RawDataText.guiText.text = RawData;
    }

    /// <summary>
    /// Function that utilises system.io.ports.getportnames() to populate
    /// a list of com ports available on the system.
    /// </summary>
    public void PopulateComPorts()
    {
        // Loop through all available ports and add them to the list
        foreach (string cPort in System.IO.Ports.SerialPort.GetPortNames())
        {
            comPorts.Add(cPort); // Debug.Log(cPort.ToString());
        }

        // Update the port status just in case :)
        portStatus = "ComPort list population complete";
    }

    /// <summary>
    /// Function used to update the current selected com port
    /// </summary>
    public string UpdateComPort()
    {
        // If open close the existing port
        if (SerialPort != null && SerialPort.IsOpen)
        { CloseSerialPort(); }

        // Find the current id of the existing port within the
        // list of available ports
        int currentComPort = comPorts.IndexOf(ComPort);

        // check against the list of ports and get the next one.
        // If we have reached the end of the list then reset to zero.
        if (currentComPort + 1 <= comPorts.Count - 1)
        {
            // Inc the port by 1 to get the next port
            ComPort = (string)comPorts[currentComPort + 1];
        }
        else
        {
            // We have reached the end of the list reset to the
            // first available port.
            ComPort = (string)comPorts[0];
        }

        // Update the port status just in case :)
        portStatus = "ComPort set to: " + ComPort.ToString();

        // Return the new ComPort just in case
        return ComPort;
    }

    /// <summary>
    /// Function used to update the current baudrate
    /// </summary>
    public int UpdateBaudRate()
    {
        // If open close the existing port
        if (SerialPort != null && SerialPort.IsOpen)
        { CloseSerialPort(); }

        // Find the current id of the existing rate within the
        // list of defined baudrates
        int currentBaudRate = baudRates.IndexOf(BaudRate);

        // check against the list of rates and get the next one.
        // If we have reached the end of the list then reset to zero.
        if (currentBaudRate + 1 <= baudRates.Count - 1)
        {
            // Inc the rate by 1 to get the next rate
            BaudRate = (int)baudRates[currentBaudRate + 1];
        }
        else
        {
            // We have reached the end of the list reset to the
            // first available rate.
            BaudRate = (int)baudRates[0];
        }

        // Update the port status just in case :)
        portStatus = "BaudRate set to: " + BaudRate.ToString();

        // Return the new BaudRate just in case
        return BaudRate;
    }

    float getPercentage(float litrosRestantes)
    {
        float porcentajeLitros = 100;
        float porcentajeLeft = 0;
        

        porcentajeLeft = (litrosRestantes * porcentajeLitros) / totalLitros;

        return porcentajeLeft;
    }
    bool spaceGPS()
    {

        if (spaceGPSestado == true)
        {

            // Planet animations
            biometricsEstado = false;
            planetas[0].gameObject.SetActive(true);
            planetas[1].gameObject.SetActive(true);
            planetas[2].gameObject.SetActive(true);
            planetas[3].gameObject.SetActive(true);
            planetas[4].gameObject.SetActive(true);

            //Planet Tag
            nombresPlanetas.gameObject.SetActive(true);
            fondoNombresPlanetas.gameObject.SetActive(true);
            // Information about planets 
            atmosfera.gameObject.SetActive(true);
            textoAtmosfera.gameObject.SetActive(true);
            diameter.gameObject.SetActive(true);
            textoDiametro.gameObject.SetActive(true);
            nucleo.gameObject.SetActive(true);
            textoNucleo.gameObject.SetActive(true);
            fondoInfoPlanetas.gameObject.SetActive(true);

            //Shut down main screen
            for (int i = 0; i < biometrics.Length; i++)
            {
                biometrics[i].gameObject.SetActive(false);
            }
            date.gameObject.SetActive(false);
            Ambient.gameObject.SetActive(false);
            battery.gameObject.SetActive(false);
            magnetic.gameObject.SetActive(false);
            PhotoID.gameObject.SetActive(false);
            fondoPhotoID.gameObject.SetActive(true);
            fondoFecha.gameObject.SetActive(false);
            fondoAmbient.gameObject.SetActive(false);
            fondoMagnetic.gameObject.SetActive(false);
            fondoBateria.gameObject.SetActive(false);
            boton[3].gameObject.SetActive(false);
            boton[4].gameObject.SetActive(false);

            //Title screen

            title.GetComponent<TextMesh>().text = "Space GPS";
            color[0].material.SetColor("_Color", Color.white);
            color[1].material.SetColor("_Color", Color.white);
            color[2].material.SetColor("_Color", Color.white);
            color[3].material.SetColor("_Color", Color.white);
            color[4].material.SetColor("_Color", Color.white);

            //To rotate the planets
            for (int u = 0; u < planetas.Length; u++)
            {
                planetas[u].transform.Rotate(new Vector3(0.0f, 0.0f, speed) * Time.deltaTime);
            }

            //to iterate  through planets array
            switch (index)
            {
                case 0:
                    planetas[0].gameObject.SetActive(true);
                    planetas[1].gameObject.SetActive(false);
                    planetas[2].gameObject.SetActive(false);
                    planetas[3].gameObject.SetActive(false);
                    planetas[4].gameObject.SetActive(false);
                    nombresPlanetas.GetComponent<TextMesh>().text = "Mercury";
                    atmosfera.GetComponent<TextMesh>().text = "Atmosphere: ";
                    textoAtmosfera.GetComponent<TextMesh>().text = " O2 42%, Na 29%,\n H2 22%, He 6%,\n K 0.5%.";
                    diameter.GetComponent<TextMesh>().text = "Diameter: ";
                    textoDiametro.GetComponent<TextMesh>().text = "4,879 km.";
                    nucleo.GetComponent<TextMesh>().text = "Core: ";
                    textoNucleo.GetComponent<TextMesh>().text = "3,600 km, mostly Iron.";
                    break;
                case 1:
                    planetas[0].gameObject.SetActive(false);
                    planetas[1].gameObject.SetActive(true);
                    planetas[2].gameObject.SetActive(false);
                    planetas[3].gameObject.SetActive(false);
                    planetas[4].gameObject.SetActive(false);
                    nombresPlanetas.GetComponent<TextMesh>().text = "Venus";
                    atmosfera.GetComponent<TextMesh>().text = "Atmosphere: ";
                    textoAtmosfera.GetComponent<TextMesh>().text = " CO2 96%, N 4%.";
                    diameter.GetComponent<TextMesh>().text = "Diameter: ";
                    textoDiametro.GetComponent<TextMesh>().text = "6,052 km.";
                    nucleo.GetComponent<TextMesh>().text = "Core: ";
                    textoNucleo.GetComponent<TextMesh>().text = "Similar to earth,\n composition unknown.";
                    break;
                case 2:
                    planetas[0].gameObject.SetActive(false);
                    planetas[1].gameObject.SetActive(false);
                    planetas[2].gameObject.SetActive(true);
                    planetas[3].gameObject.SetActive(false);
                    planetas[4].gameObject.SetActive(false);
                    nombresPlanetas.GetComponent<TextMesh>().text = "Earth";
                    atmosfera.GetComponent<TextMesh>().text = "Atmosphere: ";
                    textoAtmosfera.GetComponent<TextMesh>().text = " N2 78%, 20.94.";
                    diameter.GetComponent<TextMesh>().text = "Diameter: ";
                    textoDiametro.GetComponent<TextMesh>().text = "12,756 km.";
                    nucleo.GetComponent<TextMesh>().text = "Core: ";
                    textoNucleo.GetComponent<TextMesh>().text = "Iron and Nickel.";
                    break;
                case 3:
                    planetas[0].gameObject.SetActive(false);
                    planetas[1].gameObject.SetActive(false);
                    planetas[2].gameObject.SetActive(false);
                    planetas[3].gameObject.SetActive(true);
                    planetas[4].gameObject.SetActive(false);
                    nombresPlanetas.GetComponent<TextMesh>().text = "Mars";
                    atmosfera.GetComponent<TextMesh>().text = "Atmosphere: ";
                    textoAtmosfera.GetComponent<TextMesh>().text = " CO2 96%, Ar 1.9%,\n  1.9% N";
                    diameter.GetComponent<TextMesh>().text = "Diameter: ";
                    textoDiametro.GetComponent<TextMesh>().text = "6,779 km.";
                    nucleo.GetComponent<TextMesh>().text = "Core: ";
                    textoNucleo.GetComponent<TextMesh>().text = "Iron, Nickel and Sulfur.";
                    break;
                case 4:
                    planetas[0].gameObject.SetActive(false);
                    planetas[1].gameObject.SetActive(false);
                    planetas[2].gameObject.SetActive(false);
                    planetas[3].gameObject.SetActive(false);
                    planetas[4].gameObject.SetActive(true);
                    nombresPlanetas.GetComponent<TextMesh>().text = "Jupiter";
                    atmosfera.GetComponent<TextMesh>().text = "Atmosphere: ";
                    textoAtmosfera.GetComponent<TextMesh>().text = "Mostly Hydrgen and Helium";
                    diameter.GetComponent<TextMesh>().text = "Diameter: ";
                    textoDiametro.GetComponent<TextMesh>().text = "139,822 km.";
                    nucleo.GetComponent<TextMesh>().text = "Core: ";
                    textoNucleo.GetComponent<TextMesh>().text = "Hydrogen and Helium";
                    break;
            }

            if (valor[2] < 160)
            {

                if (index == 0)
                {
                    index = 0;
                }
                else
                {
                    index--;
                }

            }

            if (valor[3] < 160)
            {

                if (index >= 0 && index < planetas.Length - 1)
                {
                    index++;
                }
                else if (index >= 5)
                {
                    index = planetas.Length - 1;
                }

            }
            if (valor[4] < 160)
            {
                //To exit the space GPS screen
                //biometricsEstado = true;
                biometricsEstado = true;
                spaceGPSestado = false;
               

            }
        }
        else
        {
            planetas[0].gameObject.SetActive(false);
            planetas[1].gameObject.SetActive(false);
            planetas[2].gameObject.SetActive(false);
            planetas[3].gameObject.SetActive(false);
            planetas[4].gameObject.SetActive(false);
            nombresPlanetas.gameObject.SetActive(false);

            nombresPlanetas.gameObject.SetActive(false);
            fondoNombresPlanetas.gameObject.SetActive(false);
            atmosfera.gameObject.SetActive(false);
            textoAtmosfera.gameObject.SetActive(false);
            diameter.gameObject.SetActive(false);
            textoDiametro.gameObject.SetActive(false);
            nucleo.gameObject.SetActive(false);
            textoNucleo.gameObject.SetActive(false);
            fondoInfoPlanetas.gameObject.SetActive(false);
            boton[3].gameObject.SetActive(true);
            boton[4].gameObject.SetActive(true);


        }
        return spaceGPSestado;

    }
    bool biometricsPanel()
    {
        if (biometricsEstado == true)
        {
            date.gameObject.SetActive(true);
            Ambient.gameObject.SetActive(true);
            battery.gameObject.SetActive(true);
            magnetic.gameObject.SetActive(true);
            PhotoID.gameObject.SetActive(true);
            fondoPhotoID.gameObject.SetActive(true);
            fondoFecha.gameObject.SetActive(true);
            fondoAmbient.gameObject.SetActive(true);
            fondoMagnetic.gameObject.SetActive(true);
            fondoBateria.gameObject.SetActive(true);
            for (int i = 0; i < biometrics.Length; i++)
            {
                biometrics[i].gameObject.SetActive(true);
            }

        }
        return biometricsEstado;

    }
}
