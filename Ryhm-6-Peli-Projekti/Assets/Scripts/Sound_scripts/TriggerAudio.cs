using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class TriggerAudio : MonoBehaviour
{


    public string Event;
    public bool PlayOnAwake;
    public bool PlayOnDestroy;

    public void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
    }

    public void PlayOnClick()
    {
        FMODUnity.RuntimeManager.PlayOneShotAttached(Event, gameObject);
    }



    void Start()
    {
        if (PlayOnAwake == true)
        {
            PlayOneShot();
        }
    }


    void Update()
    {

    }

    void OnDestroy()
    {
        if (PlayOnDestroy == true)
        {
            PlayOneShot();
        }
    }
}
