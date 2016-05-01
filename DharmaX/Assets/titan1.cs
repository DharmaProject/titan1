using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.UI;
using Vuforia;
using System;
using System.Threading;

public class titan1 : MonoBehaviour {

    SerialPort sp;
   
    public GameObject[] boton;
    public GameObject tierra;
    public Renderer[] color ;
    string dharma;
    //public Transform text;
   
    public Transform temperatura;
    public Transform title;
    string valor;
    pulse pulso;
    
    string pls;

    int totalLitros = 7;
    float litrosLeft = 0;
    public float porcentaje = 0;
    public TextMesh txtM;
    public static string x;
    public static string[] data;


    // Use this for initialization
    void Start()
    {
        OpenConnection();

    }

    // Update is called once per frame
    void Update() {
        x = sp.ReadLine();
        sp.ReadTimeout = 25;
        data = x.Split(' ');

        Debug.Log("valoooooor: " + data[0]);
        txtM.text = data[0];
    }

    
    public void OpenConnection()
    {
        sp = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
        Debug.Log("OpenConnection started");
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                Debug.Log("Closing port, because it was already open!");
            }
            else
            {
                sp.Open();  // opens the connection
                            // sets the timeout value before reporting error
                Debug.Log("Port Opened!");
            }
        }
        else
        {
            if (sp.IsOpen)
            {
                print("Port is already open");
            }
            else
            {
                print("Port == null");
            }
        }
        Debug.Log("Open Connection finished running");
    }


    float getPercentage(float litrosRestantes) {
        float porcentajeLitros = 100;
        float porcentajeLeft = 0;

        porcentajeLeft = (litrosRestantes * porcentajeLitros) / totalLitros;

        return porcentajeLeft;
    }
   

}
