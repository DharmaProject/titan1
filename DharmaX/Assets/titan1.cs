using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.UI;
using Vuforia;
using System;
using System.Threading;

public class titan1 : MonoBehaviour {

<<<<<<< HEAD
    SerialPort miPuerto = new SerialPort("COM3", 9600);
    string[] botones;
    int[] valor;
=======
    SerialPort sp;
   
>>>>>>> 251ddb00986775f13f55ab163e9e35ca352bd26b
    public GameObject[] boton;
    public GameObject[] planetas;
    public float speed;
    public Renderer[] color ;
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
   
    public Transform temperatura;
    public Transform title;
<<<<<<< HEAD
    public Transform nombresPlanetas;
    public GameObject fondoNombresPlanetas;
    public Transform atmosfera;
    public Transform textoAtmosfera;
    public Transform diameter;
    public Transform textoDiametro;
    public Transform nucleo;
    public Transform textoNucleo;
    public Transform fondoInfoPlanetas;
=======
    string valor;
    pulse pulso;
    
    string pls;
>>>>>>> 251ddb00986775f13f55ab163e9e35ca352bd26b

    int totalLitros = 7;
    float litrosLeft = 0;
    public float porcentaje = 0;
    public TextMesh txtM;
    public static string x;
    public static string[] data;


    // Use this for initialization
    void Start()
    {
<<<<<<< HEAD
        miPuerto.Open();
        nombresPlanetas.gameObject.SetActive(false);
        fondoNombresPlanetas.gameObject.SetActive(false);
        selector.gameObject.SetActive(false);
        atmosfera.gameObject.SetActive(false);
        textoAtmosfera.gameObject.SetActive(false);
        diameter.gameObject.SetActive(false);
        textoDiametro.gameObject.SetActive(false);
        nucleo.gameObject.SetActive(false);
        textoNucleo.gameObject.SetActive(false);
        fondoInfoPlanetas.gameObject.SetActive(false);
        for (int i = 0; i < planetas.Length; i++)
        {
            planetas[i].gameObject.SetActive(false);
          

        }
            valor = new int[5];

=======
        OpenConnection();
>>>>>>> 251ddb00986775f13f55ab163e9e35ca352bd26b

    }

    // Update is called once per frame
    void Update() {
        x = sp.ReadLine();
        sp.ReadTimeout = 25;
        data = x.Split(' ');

        Debug.Log("valoooooor: " + data[0]);
        txtM.text = data[0];
    }

<<<<<<< HEAD
        string valores = miPuerto.ReadLine();
        
        botones = valores.Split(',');
        spaceGPS();
     

        for (int i = 0; i < botones.Length; i++)
        {
            valor[i] = int.Parse(botones[i].ToString());
            if (spaceGPSestado == false && missionControlEstado == false && ArmRobotEstado == false && compoundAnalyzerEstado == false && musicPlayerEstado == false)
            {
                biometricsEstado = true;
                 
                if (valor[0] < 160)
                {
                    color[0].material.SetColor("_Color", Color.green);
                    color[1].material.SetColor("_Color", Color.white);
                    color[2].material.SetColor("_Color", Color.white);
                    color[3].material.SetColor("_Color", Color.white);
                    color[4].material.SetColor("_Color", Color.white);

                    spaceGPSestado = true;
                    biometricsEstado = false;

                }
                else if (valor[1] < 160)
                {
                    color[1].material.SetColor("_Color", Color.green);
                    color[0].material.SetColor("_Color", Color.white);
                    color[2].material.SetColor("_Color", Color.white);
                    color[3].material.SetColor("_Color", Color.white);
                    color[4].material.SetColor("_Color", Color.white);
                    title.GetComponent<TextMesh>().text = "Mission Control";
                }
                else if (valor[2] < 160)
                {
                    color[1].material.SetColor("_Color", Color.white);
                    color[0].material.SetColor("_Color", Color.white);
                    color[2].material.SetColor("_Color", Color.green);
                    color[3].material.SetColor("_Color", Color.white);
                    color[4].material.SetColor("_Color", Color.white);
                    title.GetComponent<TextMesh>().text = "Arm Robot";
                }
                else if (valor[3] < 160)
                {
                    color[1].material.SetColor("_Color", Color.white);
                    color[0].material.SetColor("_Color", Color.white);
                    color[2].material.SetColor("_Color", Color.white);
                    color[3].material.SetColor("_Color", Color.green);
                    color[4].material.SetColor("_Color", Color.white);
                    title.GetComponent<TextMesh>().text = "Compound Analyzer";
                }
                else if (valor[4] < 160)
                {
                    color[1].material.SetColor("_Color", Color.white);
                    color[0].material.SetColor("_Color", Color.white);
                    color[2].material.SetColor("_Color", Color.white);
                    color[3].material.SetColor("_Color", Color.white);
                    color[4].material.SetColor("_Color", Color.green);
                    title.GetComponent<TextMesh>().text = "Music Player";
                }
                else
                {
                    color[1].material.SetColor("_Color", Color.white);
                    color[0].material.SetColor("_Color", Color.white);
                    color[2].material.SetColor("_Color", Color.white);
                    color[3].material.SetColor("_Color", Color.white);
                    color[4].material.SetColor("_Color", Color.white);

                    title.GetComponent<TextMesh>().text = "Biometrics";
                    //temperatura.GetComponent<TextMesh>().text = "Temperatura: " + valor[6].ToString();
                }
            }
        }
    }

    bool spaceGPS()
    {
       
        if (spaceGPSestado == true)
        {
            
            planetas[0].gameObject.SetActive(true);
            planetas[1].gameObject.SetActive(true);
            planetas[2].gameObject.SetActive(true);
            planetas[3].gameObject.SetActive(true);
            planetas[4].gameObject.SetActive(true);
            nombresPlanetas.gameObject.SetActive(true);
            fondoNombresPlanetas.gameObject.SetActive(true);
            atmosfera.gameObject.SetActive(true);
            textoAtmosfera.gameObject.SetActive(true);
            diameter.gameObject.SetActive(true);
            textoDiametro.gameObject.SetActive(true);
            nucleo.gameObject.SetActive(true);
            textoNucleo.gameObject.SetActive(true);
            fondoInfoPlanetas.gameObject.SetActive(true);


            title.GetComponent<TextMesh>().text = "Space GPS";
            color[0].material.SetColor("_Color", Color.white);
            color[1].material.SetColor("_Color", Color.white);
            color[2].material.SetColor("_Color", Color.white);
            color[3].material.SetColor("_Color", Color.white);
            color[4].material.SetColor("_Color", Color.white);
            for(int u=0; u <planetas.Length; u++)
            {
                planetas[u].transform.Rotate(new Vector3(0.0f, 0.0f, speed) * Time.deltaTime);
            }

            /*
            posicionXselector = planetas[index].transform.position.x;
            posicionYselector = planetas[index].transform.position.y;
            posicionZselector = planetas[index].transform.position.z;
            scaleXselector = planetas[index].transform.localScale.x + planetas[index].transform.localScale.x/2;
            scaleYselector = planetas[index].transform.localScale.y + planetas[index].transform.localScale.y/2;
            scaleZselector = planetas[index].transform.localScale.z + planetas[index].transform.localScale.z/2;
          //  selector.transform.position = new Vector3(posicionXselector, posicionYselector, posicionZselector);
           // selector.transform.localScale = new Vector3(scaleXselector, scaleYselector, scaleZselector);*/

            if (valor[0] < 160)
            {
               
                if (index == 0)
                {
                    index = 0;
                } else
                {
                    index--;
                }
                //
            }

            switch(index)
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

            if(valor[1] <160 )
            {
              
                Debug.Log(index);
                if (index>=0 && index < planetas.Length-1)
                {
                    index++;
                } else if (index >= 5)
                {
                    index = planetas.Length-1;
                }

            }
            if(valor[2] < 160)
            {

                planetas[0].gameObject.SetActive(false);
                planetas[1].gameObject.SetActive(false);
                planetas[2].gameObject.SetActive(false);
                planetas[3].gameObject.SetActive(false);
                planetas[4].gameObject.SetActive(false);
                nombresPlanetas.gameObject.SetActive(false);
                spaceGPSestado = false;
                missionControlEstado = false;
                ArmRobotEstado = false;
                compoundAnalyzerEstado = false;
                musicPlayerEstado = false;
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
        }
        return spaceGPSestado;

    }


    float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
=======
    
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
>>>>>>> 251ddb00986775f13f55ab163e9e35ca352bd26b
    }
   

}
