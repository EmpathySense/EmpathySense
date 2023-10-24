using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AjustesValue : MonoBehaviour
{
    public Slider slider; // Asigna el Slider en el Inspector
    public TMP_Text valueText;
    public Toggle toggleElement;

    public Button saveButton;
    private float prevSliderValue;
    private void Start()
    {
        Prefs prefs = RealmController.Instance.GetPrefs();
        Debug.Log("Volumen Pref: "+ prefs.Volumen);
        if (prefs.Volumen==0)
        {   
            Debug.Log("Volumen 0");
            
            Debug.Log("Valor del Togle unu actual: " + toggleElement.isOn);
            toggleElement.isOn = true;
            Debug.Log("Valor del Togle unu cambiado: " + toggleElement.isOn);
            valueText.text = prefs.Volumen.ToString();
            prevSliderValue=100;
        }
        else
        {
            toggleElement.isOn = false;
            prevSliderValue=slider.value;
            valueText.text = prevSliderValue.ToString();
        }
        slider.value = prefs.Volumen;
        saveButton.gameObject.SetActive(false);
        // Puedes acceder al valor inicial del Slider así:
        //float prevSliderValue = slider.value;
        //Debug.Log("Valor inicial del Slider: " + prevSliderValue);

        //bool isToggled = toggleElement.isOn;
        //Debug.Log("Estado inicial del Toggle: " + isToggled);
    }

    public void OnSliderValueChanged()
    {
        // Esta función se llama cuando cambia el valor del Slider
        float currentSliderValue = slider.value;
        if (currentSliderValue==0)
        {   
            
            toggleElement.isOn = true;
        }
        else
        {
            toggleElement.isOn = false;
        }
        valueText.text = currentSliderValue.ToString();
        //Debug.Log("Valor del Slider actual: " + currentSliderValue);
        if (toggleElement.isOn==false)
        {   
            prevSliderValue=currentSliderValue;
            // El Toggle está activado
            // Realiza acciones cuando el Toggle está encendido
        }
        //Debug.Log("Valor del Slider Anterior: " + prevSliderValue);
    }
    public void OnToggleValueChanged()
    {
        // Esta función se llama cuando cambia el estado del Toggle
        bool isToggled = toggleElement.isOn;
        //Debug.Log("Estado actual del Toggle: " + isToggled);

        // Puedes realizar acciones basadas en el estado del Toggle aquí
        if (isToggled)
        {   
            //Debug.Log("ACTIVOO: ");
            //Debug.Log("ValorPrev: "+ prevSliderValue);
            slider.value = 0;
            //Debug.Log("Volumen toggle active: "+ slider.value);
            //Debug.Log("Valor del Togle unu actual: " + toggleElement.isOn);
            // El Toggle está activado
            // Realiza acciones cuando el Toggle está encendido
        }
        else
        {   
            //Debug.Log("DESACTIVADOOO: ");
            //Debug.Log("ValorPrev: "+ prevSliderValue);
            //Debug.Log("Volumen tal cual: "+prevSliderValue);
            slider.value = prevSliderValue;
            // El Toggle está desactivado
            // Realiza acciones cuando el Toggle está apagado
        }
    }

    public void SaveVolumenValue()
    {

        RealmController.Instance.UpdateVolume((int)slider.value);
        //Debug.Log("Volumen guardado: "+ prefs.Volumen);
    }
}

