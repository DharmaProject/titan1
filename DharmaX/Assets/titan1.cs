using UnityEngine;
using System.Collections;
using System.IO.Ports;
using UnityEngine.UI;
using Vuforia;

public class titan1 : MonoBehaviour {
    AudioSource audio;
    public AudioClip[] audios;
    public Renderer[] musicButtons;
    public TextMesh[] canciones;

    SerialPort miPuerto = new SerialPort("COM4", 9600);
    string[] botones;
    int[] valor;
    public GameObject[] boton;
    public GameObject tierra;
    public Renderer[] color ;

    
    string dharma;
    //public Transform text;
   
    public Transform temperatura;
    public Transform title;

    // Use this for initialization
    void Start()
    {
        miPuerto.Open();
        tierra.gameObject.SetActive(false);
        
        
    }

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        musicButtons[0].gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(Music());
        
        string valores = miPuerto.ReadLine();
        botones = valores.Split(',');

        for (int i = 0; i < botones.Length; i++)
        {
           valor[i] = int.Parse(botones[i].ToString());
            Debug.Log(valor[4]);
            
            if (valor[0]<160)
            {
                color[0].material.SetColor("_Color", Color.green);
                color[1].material.SetColor("_Color", Color.white);
                color[2].material.SetColor("_Color", Color.white);
                color[3].material.SetColor("_Color", Color.white);
                color[4].material.SetColor("_Color", Color.white);
                tierra.gameObject.SetActive(true);
                tierra.transform.Rotate(Vector3.up * Time.deltaTime);
                tierra.transform.Rotate(Vector3.left * Time.deltaTime);
                title.GetComponent<TextMesh>().text = "Space GPS";

            } else if(valor[1]<160)
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
                tierra.gameObject.SetActive(false);
                title.GetComponent<TextMesh>().text = "Biometrics";
                //temperatura.GetComponent<TextMesh>().text = "Temperatura: " + valor[6].ToString();
            }

        }
    }



    float Map(float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }


    int index = 0;
    IEnumerator Music()
    {
        //anterior
        if (Input.GetKey(KeyCode.P))
        {
            if (index < audios.Length)
            {
                yield break;
            }
            else {
                index--;
                audio.clip = audios[index];
                audio.Play();
                musicButtons[0].gameObject.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                musicButtons[0].gameObject.SetActive(true);
            }
        }
        
        //siguiente
        if(Input.GetKey(KeyCode.C))
        {
            musicButtons[0].gameObject.SetActive(true);
            if (index > audios.Length)
            {
                yield break;             
            }
            else {
                audio.clip = audios[index];
                audio.Play();
                canciones[index].GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);
                index++;
                
                musicButtons[1].gameObject.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                musicButtons[1].gameObject.SetActive(true);
                
            }
        }
        
    }

}
