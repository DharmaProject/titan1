using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {
    AudioSource audio;
    public AudioClip[] audios;
    
    public Renderer right;
    
    void Start() {
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
       
    }

    public void Music()
    {
        StartCoroutine(CoMusic());
    }

    public IEnumerator CoMusic()
    {
        
            audio.clip = audios[0];
            audio.Play();
            right.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            right.gameObject.SetActive(true);
        
    }
    
}
