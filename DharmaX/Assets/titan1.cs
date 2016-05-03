using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.UI;
using Vuforia;

public class titan1 : MonoBehaviour {

    SerialPort miPuerto = new SerialPort("COM4", 9600);
    string[] botones;
    int[] valor;
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


    // Use this for initialization
    void Start()
    {
        miPuerto.Open();
         spaceGPSestado = false;
         missionControlEstado = false;
         compoundAnalyzerEstado = false;
         ArmRobotEstado = false;
         musicPlayerEstado = false;
         biometricsEstado = true;

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
        valor = new int[5];


    }

    // Update is called once per frame
    void Update()
    {
        //Receive data from Arduino
        string valores = miPuerto.ReadLine();
        botones = valores.Split(',');
        //Call all the functions, and display only the one in true
        biometricsPanel();
        spaceGPS();
        missionContrtol();
        /*
        Debug.Log(valor[0]);
        Debug.Log(valor[1]);
        Debug.Log(valor[2]);
        Debug.Log(valor[3]);
        Debug.Log(valor[4]);*/
        //Separate the values received from the arduino
        for (int i = 0; i < botones.Length; i++)
        {
            valor[i] = int.Parse(botones[i].ToString());
 
            if (spaceGPSestado == false && missionControlEstado == false && ArmRobotEstado == false && compoundAnalyzerEstado == false && musicPlayerEstado == false && biometricsEstado == true)
            {
                 
                if (valor[0] < 160)
                {
                    color[0].material.SetColor("_Color", Color.green);
                    color[1].material.SetColor("_Color", Color.white);
                    color[2].material.SetColor("_Color", Color.white);
                    color[3].material.SetColor("_Color", Color.white);
                    color[4].material.SetColor("_Color", Color.white);
                    /*
                    spaceGPSestado = true; 
                    missionControlEstado = false;
                    compoundAnalyzerEstado = false;
                    ArmRobotEstado = false;
                    musicPlayerEstado = false;
                    biometricsEstado = false;
                    */
                }
                if (valor[1] < 100)
                {
                    color[0].material.SetColor("_Color", Color.white);
                    color[1].material.SetColor("_Color", Color.green);
                    color[2].material.SetColor("_Color", Color.white);
                    color[3].material.SetColor("_Color", Color.white);
                    color[4].material.SetColor("_Color", Color.white);
                    /*
                    spaceGPSestado = false;
                    missionControlEstado = true;
                    compoundAnalyzerEstado = false;
                    ArmRobotEstado = false;
                    musicPlayerEstado = false;
                    biometricsEstado = false;*/
  

                }
                if (valor[2] < 100)
                {
                    color[0].material.SetColor("_Color", Color.white);
                    color[1].material.SetColor("_Color", Color.white);
                    color[2].material.SetColor("_Color", Color.green);
                    color[3].material.SetColor("_Color", Color.white);
                    color[4].material.SetColor("_Color", Color.white);
                    
                    spaceGPSestado = false;
                    missionControlEstado = false;
                    ArmRobotEstado = true;
                    compoundAnalyzerEstado = false;
                    musicPlayerEstado = false;
                    biometricsEstado = false;
          
                }
               if (valor[3] < 160)
                {
                    color[1].material.SetColor("_Color", Color.white);
                    color[0].material.SetColor("_Color", Color.white);
                    color[2].material.SetColor("_Color", Color.white);
                    color[3].material.SetColor("_Color", Color.green);
                    color[4].material.SetColor("_Color", Color.white);
     /*
                    spaceGPSestado = false;
                    missionControlEstado = false;
                    ArmRobotEstado = false;
                    compoundAnalyzerEstado = true;
                    musicPlayerEstado = false;
                    biometricsEstado = false;*/

                }
                if (valor[4] < 100)
                {
                    color[1].material.SetColor("_Color", Color.white);
                    color[0].material.SetColor("_Color", Color.white);
                    color[2].material.SetColor("_Color", Color.white);
                    color[3].material.SetColor("_Color", Color.white);
                    color[4].material.SetColor("_Color", Color.green);
                    /*
                    spaceGPSestado = false;
                    missionControlEstado = false;
                    ArmRobotEstado = false;
                    compoundAnalyzerEstado = false;
                    musicPlayerEstado = true;
                    biometricsEstado = false;*/
                }
                else
                {
                    color[1].material.SetColor("_Color", Color.white);
                    color[0].material.SetColor("_Color", Color.white);
                    color[2].material.SetColor("_Color", Color.white);
                    color[3].material.SetColor("_Color", Color.white);
                    color[4].material.SetColor("_Color", Color.white);

                    spaceGPSestado = false;
                    missionControlEstado = false;
                    ArmRobotEstado = false;
                    compoundAnalyzerEstado = false;
                    musicPlayerEstado = false;
                    biometricsEstado = true;
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
            for(int i=0; i <biometrics.Length; i++)
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
            for(int u=0; u <planetas.Length; u++)
            {
                planetas[u].transform.Rotate(new Vector3(0.0f, 0.0f, speed) * Time.deltaTime);
            }

            //to iterate  through planets array
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

            if (valor[0] < 160)
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

            if (valor[1] <160 )
            {

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
                //To exit the space GPS screen
                biometricsEstado = true;
                spaceGPSestado = false;
                missionControlEstado = false;
                ArmRobotEstado = false;
                compoundAnalyzerEstado = false;
                musicPlayerEstado = false;

            }
        } else
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
        if(biometricsEstado == true)
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
        /*
            date.gameObject.SetActive(false);
            Ambient.gameObject.SetActive(false);
            battery.gameObject.SetActive(false);
            magnetic.gameObject.SetActive(false);
            PhotoID.gameObject.SetActive(false);
            fondoPhotoID.gameObject.SetActive(false);
            fondoFecha.gameObject.SetActive(false);
            fondoAmbient.gameObject.SetActive(false);
            fondoMagnetic.gameObject.SetActive(false);
            fondoBateria.gameObject.SetActive(false);
            for (int i = 0; i < biometrics.Length; i++)
            {
                biometrics[i].gameObject.SetActive(false);
            }*/
        

        return biometricsEstado;

    }

    bool missionContrtol()
    {

        if(missionControlEstado == true)
        {
            title.GetComponent<TextMesh>().text = "Mission Control";
            color[1].material.SetColor("_Color", Color.white);
            color[0].material.SetColor("_Color", Color.white);
            color[2].material.SetColor("_Color", Color.white);
            PhotoID.gameObject.SetActive(true);
            fondoPhotoID.gameObject.SetActive(true);
            boton[3].gameObject.SetActive(false);
            boton[4].gameObject.SetActive(false);
            Debug.Log(valor[1]);

            if (valor[0] <160)
            {
                color[1].material.SetColor("_Color", Color.green);
                color[0].material.SetColor("_Color", Color.white);
                color[2].material.SetColor("_Color", Color.white);
            }

            if(valor[1]<160)
            {
                color[1].material.SetColor("_Color", Color.white);
                color[0].material.SetColor("_Color", Color.green);
                color[2].material.SetColor("_Color", Color.white);
                Debug.Log(valor[1]);
            }

            if (valor[2] < 160)
            {
                biometricsEstado = true;
                spaceGPSestado = false;
                missionControlEstado = false;
                ArmRobotEstado = false;
                compoundAnalyzerEstado = false;
                musicPlayerEstado = false;
              }
        } else
        {
            PhotoID.gameObject.SetActive(true);
            fondoPhotoID.gameObject.SetActive(true);
            boton[3].gameObject.SetActive(true);
            boton[4].gameObject.SetActive(true);
            missionControlEstado = false;
        }

        return missionControlEstado;
    }

    bool ArmRobotPanel()
    {
        if(ArmRobotEstado == true)
        {
            title.GetComponent<TextMesh>().text = "Tool box";
            color[1].material.SetColor("_Color", Color.white);
            color[0].material.SetColor("_Color", Color.white);
            color[2].material.SetColor("_Color", Color.white);
            PhotoID.gameObject.SetActive(true);
            fondoPhotoID.gameObject.SetActive(true);
            boton[3].gameObject.SetActive(false);
            boton[4].gameObject.SetActive(false);

            if (valor[0] < 160)
            {
                color[1].material.SetColor("_Color", Color.green);
                color[0].material.SetColor("_Color", Color.white);
                color[2].material.SetColor("_Color", Color.white);
            }

            if (valor[1] < 160)
            {
                color[1].material.SetColor("_Color", Color.white);
                color[0].material.SetColor("_Color", Color.green);
                color[2].material.SetColor("_Color", Color.white);
            }

            if (valor[2] < 160)
            {
                biometricsEstado = true;
                spaceGPSestado = false;
                missionControlEstado = false;
                ArmRobotEstado = false;
                compoundAnalyzerEstado = false;
                musicPlayerEstado = false;
            }
        } else
        {
            PhotoID.gameObject.SetActive(true);
            fondoPhotoID.gameObject.SetActive(true);
            boton[3].gameObject.SetActive(true);
            boton[4].gameObject.SetActive(true);
        }
        return ArmRobotEstado; 
    }

    bool compoundAnalyzerPanel ()
    {
        if(compoundAnalyzerEstado == true) {
            title.GetComponent<TextMesh>().text = "Compound Analyzer";
            color[1].material.SetColor("_Color", Color.white);
            color[0].material.SetColor("_Color", Color.white);
            color[2].material.SetColor("_Color", Color.white);
            PhotoID.gameObject.SetActive(true);
            fondoPhotoID.gameObject.SetActive(true);
            boton[3].gameObject.SetActive(false);
            boton[4].gameObject.SetActive(false);

            if (valor[0] < 160)
            {
                color[1].material.SetColor("_Color", Color.green);
                color[0].material.SetColor("_Color", Color.white);
                color[2].material.SetColor("_Color", Color.white);
            }

            if (valor[1] < 160)
            {
                color[1].material.SetColor("_Color", Color.white);
                color[0].material.SetColor("_Color", Color.green);
                color[2].material.SetColor("_Color", Color.white);
            }

            if (valor[2] < 160)
            {
                biometricsEstado = true;
                spaceGPSestado = false;
                missionControlEstado = false;
                ArmRobotEstado = false;
                compoundAnalyzerEstado = false;
                musicPlayerEstado = false;
            }
        } else
        {
            PhotoID.gameObject.SetActive(true);
            fondoPhotoID.gameObject.SetActive(true);
            boton[3].gameObject.SetActive(true);
            boton[4].gameObject.SetActive(true);
        }

        return compoundAnalyzerEstado;
    }

    bool musicPlayerPanel()
    {
        if(musicPlayerEstado == true)
        {
            title.GetComponent<TextMesh>().text = "Music Player";
            color[1].material.SetColor("_Color", Color.white);
            color[0].material.SetColor("_Color", Color.white);
            color[2].material.SetColor("_Color", Color.white);
            PhotoID.gameObject.SetActive(true);
            fondoPhotoID.gameObject.SetActive(true);
            boton[3].gameObject.SetActive(false);
            boton[4].gameObject.SetActive(false);

            if (valor[0] < 160)
            {
                color[1].material.SetColor("_Color", Color.green);
                color[0].material.SetColor("_Color", Color.white);
                color[2].material.SetColor("_Color", Color.white);
            }

            if (valor[1] < 160)
            {
                color[1].material.SetColor("_Color", Color.white);
                color[0].material.SetColor("_Color", Color.green);
                color[2].material.SetColor("_Color", Color.white);
            }

            if (valor[2] < 160)
            {
                biometricsEstado = true;
                spaceGPSestado = false;
                missionControlEstado = false;
                ArmRobotEstado = false;
                compoundAnalyzerEstado = false;
                musicPlayerEstado = false;
            }
        }
        else
        {
            PhotoID.gameObject.SetActive(true);
            fondoPhotoID.gameObject.SetActive(true);
            boton[3].gameObject.SetActive(true);
            boton[4].gameObject.SetActive(true);
        }
        return musicPlayerEstado;
    }


    float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}
