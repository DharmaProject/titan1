using UnityEngine;
using System.Collections;
using UnityEngine.UI;


    public class Texto : MonoBehaviour
    {
    int contar;
        // Use this for initialization
        void Start()
        {
            contar = 0;
        }

        // Update is called once per frame
        void Update()
        {
            contar = contar + 1;
            GetComponent<GUIText>().text = contar.ToString();
            if (contar >= 300)
            {
                GetComponent<GUIText>().enabled = false;
            }
        }
    }
