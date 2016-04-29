using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.UI;
using Vuforia;

public class titan1 : MonoBehaviour {

    SerialPort miPuerto = new SerialPort("COM8", 9600);
    string[] botones;
    int[] valor;
    public GameObject[] boton;
    public Renderer[] color ;
    string dharma;
    //public Transform text;
    public Transform text2;
    public Transform text3;
    public Transform text4;


    // Use this for initialization
    void Start()
    {
        miPuerto.Open();
       
        valor = new int[5];


    }

    // Update is called once per frame
    void Update()
    {

        string valores = miPuerto.ReadLine();
        botones = valores.Split(',');
       
        
        for (int i = 0; i < botones.Length; i++)
        {
           valor[i] = int.Parse(botones[i].ToString());
            Debug.Log(valor[0]);

            if (valor[0] < 160)
            {
                //Debug.Log(valor[i]);
                switch (i)
                {
                    case 0:
                        color[0].material.SetColor("_Color", Color.red);
                        break;
                }
            } else
            {
                color[0].material.SetColor("_Color", Color.white);
            }
        }
        //Temperatura = Mathf.Round(Temperatura);
        //Litros = Mathf.Round(Litros);
        //text.GetComponent<TextMesh>().text = "Temperatura: " + anguloMap.ToString();
       /* text2.GetComponent<TextMesh>().text = "Litros: " + Litros.ToString();
        text3.GetComponent<TextMesh>().text = "Temperatura: " + Temperatura.ToString();
        text4.GetComponent<TextMesh>().text = "DHARMA";*/


    }



    float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }
}
