using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.UI;
using Vuforia;


public class DharmaTest : MonoBehaviour {

    SerialPort miPuerto = new SerialPort("COM8", 115200);
    string[] vals;
    int Temperatura;
    int Litros;
    int anguloMap;
    string dharma;
    //public Transform text;
    public Transform text2;
    public Transform text3;
    public Transform text4;
  

    // Use this for initialization
    void Start () {
        miPuerto.Open();

        
    }
	
	// Update is called once per frame
	void Update () {

        string valores = miPuerto.ReadLine();
        vals = valores.Split(',');
        Litros = int.Parse(vals[1]);
        Temperatura = int.Parse(vals[0]);
        
        //Temperatura = Mathf.Round(Temperatura);
        //Litros = Mathf.Round(Litros);
        //text.GetComponent<TextMesh>().text = "Temperatura: " + anguloMap.ToString();
        text2.GetComponent<TextMesh>().text = "Litros: " + Litros.ToString();
        text3.GetComponent<TextMesh>().text = "Temperatura: " + Temperatura.ToString();
        text4.GetComponent<TextMesh>().text = "DHARMA";

    
	}

 

    float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}

