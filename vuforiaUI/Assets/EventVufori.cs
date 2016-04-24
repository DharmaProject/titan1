using UnityEngine;
using System.Collections;
using Vuforia;
using System;

public class EventVufori : MonoBehaviour, ITrackableEventHandler {

    TrackableBehaviour mTrack;
    public GameObject go;
	// Use this for initialization
	void Start () {
        mTrack = GetComponent<TrackableBehaviour>();
	}
	
    void OnStatusChange(TrackableBehaviour.Status estado)
    {
     
        TrackableBehaviour.Status newStatus = estado;
        

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
            go.SetActive(true);
            
        }
        if (newStatus == TrackableBehaviour.Status.NOT_FOUND) {
            go.SetActive(false);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        throw new NotImplementedException();
    }
}
