using UnityEngine;
using System.IO.Ports;

public class DharmaTest : MonoBehaviour {

    SerialPort miPuerto = new SerialPort("COM5" , 115200);
    string[] vals;
    float angulo;
    float magnetismo;
    float respiracion;
    float anguloMap;
    string dharma;
    public Transform text;
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
        angulo = float.Parse(vals[2]);
        magnetismo = float.Parse(vals[0]);
        respiracion = float.Parse(vals[1]);
        print(vals[2]);
        anguloMap = Map(angulo , 0 , 1023, 0 ,180);
        anguloMap = Mathf.Round(anguloMap);
        magnetismo = Mathf.Round(magnetismo);
        text.GetComponent<TextMesh>().text = "Temperatura:" + anguloMap.ToString();
        text2.GetComponent<TextMesh>().text = "Magnetismo:" + magnetismo.ToString();
        text3.GetComponent<TextMesh>().text = "Respiración:" + respiracion.ToString();
        text4.GetComponent<TextMesh>().text = "DHARMA";
    }

    float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}
