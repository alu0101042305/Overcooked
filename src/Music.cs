using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Music : Selectable
{
    private AudioSource audioSource;
    Container container;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    


    override public void OnAction()
    {

        audioSource.mute = !audioSource.mute;

        Done();
    }

}
