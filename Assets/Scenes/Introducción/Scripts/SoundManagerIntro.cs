using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerIntro : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClipArray;

    public float time;
    private static int cont=0;

    AudioClip lastClip;
    AudioClip newClip;

    //Inicializar
    public void start()
    {
        
        lastClip = null;
    }

    void Start()
    {
        cont = 0;
        Invoke("sonidoPorPaso", 1.7f);
        
        /*
        if (SceneManager.GetActiveScene().name == "Paso-A")
        {
        }*/
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
