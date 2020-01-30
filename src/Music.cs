using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Clase que reproduce la musica de la radio

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
