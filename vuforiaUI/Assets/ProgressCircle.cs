using UnityEngine;
using ProgressBar;


public class ProgressCircle : MonoBehaviour
{

    public ProgressRadialBehaviour prb;

    public float valor;
    // Use this for initialization
    void Start () {
        
	}

    // Update is called once per frame
    void Update() {

        Debug.Log(valor);
        
	}

    void LateUpdate() {
        prb.Value = valor;
    }

}
