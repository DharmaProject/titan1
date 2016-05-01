using UnityEngine;
using System.Collections;

public class PlayMusic : MonoBehaviour {
    AudioSource audio;
    public AudioClip[] audios;
    
    public Renderer right;
    
    void Awake()
    {
        audio = GetComponent<AudioSource>();
        Music();
    }
	
	// Update is called once per frame
	public void Update () {
        Awake();
    }

   public void Music()
    {
        StartCoroutine(CoMusic());
    }


    IEnumerator CoMusic()
    {

        
            audio.clip = audios[0];
            audio.Play();
            right.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            right.gameObject.SetActive(true);
    }
    
    
}
