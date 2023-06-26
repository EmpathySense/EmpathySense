using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

    public float time;
    private int cont = 0;

    AudioClip lastClip;
    AudioClip newClip;

    //Inicializar
    public void start()
    {
        lastClip = null;
    }


    public void sonidoPorPaso()
    {
        start();

        audioSource.Stop();

        AudioClip newClip = audioClipArray[cont];
        audioSource.PlayOneShot(newClip);
        lastClip = newClip;
        cont++;


    }
}