using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadVolumenPref : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioSource audioSourceScene;
    
    public Slider SliderVolumen;
    void Start()
    {
        Prefs prefs = RealmController.Instance.GetPrefs();
        float volumen = prefs.Volumen/100.00f;
        audioSource.volume=volumen;

        
        if (audioSourceScene!=null)
        {
        float volumenScene = prefs.Volumen/100.00f;
        audioSourceScene.volume=volumenScene;
        }
        //Debug.Log("Volumen Pref: "+ prefs.Volumen);
        //Debug.Log("Volumen Pref: "+ volumen);

    }

    // Update is called once per frame
    public void updateVolume()
    {   

        float volumen = SliderVolumen.value/100.00f;
        audioSource.volume=volumen;

        if (audioSourceScene!=null)
        {
        float volumenScene = SliderVolumen.value/100.00f;
        audioSourceScene.volume=volumenScene;
        }
        Debug.Log("Volumen Pref Update: "+ volumen);
    }
}
