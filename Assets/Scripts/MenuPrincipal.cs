using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public static bool panelSimulacion;
    public Button buttonSim;
    public GameObject panelSimulacionesDesactivado;
    public GameObject panelMenuPrincipal;
    public GameObject alertSim;
    // Start is called before the first frame update
    void Start()
    {   
        Prefs prefs_User = RealmController.Instance.GetPrefs();

        if (prefs_User.InfoSim)
        {
            //Debug.Log("Entro en el Desactivo");
            ColorBlock cb = buttonSim.colors;
            cb.normalColor = Color.grey;
            cb.highlightedColor = Color.grey;
            cb.selectedColor = Color.grey;
            cb.pressedColor = Color.red;
            buttonSim.colors = cb;
            buttonSim.onClick.AddListener(()=>StartCoroutine(ShowAlertCanvas(alertSim)));
            //buttonSim.enabled = false;
            
        }
        else
        {
            ColorBlock cb = buttonSim.colors;
            cb.normalColor = Color.white;
            cb.highlightedColor = Color.yellow;
            cb.selectedColor = Color.green;
            cb.pressedColor = Color.green;
            buttonSim.colors = cb;
            buttonSim.onClick.AddListener(()=>ActivePanels());
        }
    }
    void ActivePanels()
    {
        panelMenuPrincipal.SetActive(false);
        panelSimulacionesDesactivado.SetActive(true);
    }
    public void ReturnHelp()
    {
        SceneManager.LoadScene("Introduccion");
    }

    IEnumerator ShowAlertCanvas(GameObject canvas)
    {
        canvas.SetActive(true); // Activa el canvas

        yield return new WaitForSeconds(2f); // Espera durante 2 segundos

        canvas.SetActive(false); // Desactiva el canvas
    }
        /* if (panelSimulacion == true)
        {
            panelSimulacionesDesactivado.SetActive(true);
            panelMenuPrincipal.SetActive(false);

        }
        else
        {
            panelMenuPrincipal.SetActive(true);
            panelSimulacionesDesactivado.SetActive(false);
        }

        if ( PlayerPrefs.HasKey("UnlockSim")==true)
        {

            //Debug.Log("Entro en el activo");
            ColorBlock cb = buttonSim.colors;
            cb.normalColor = Color.white;
            cb.highlightedColor = Color.yellow;
            cb.selectedColor = Color.green;
            cb.pressedColor = Color.green;
            buttonSim.colors = cb;

           

        }
        else if (PlayerPrefs.HasKey("UnlockSim")==false)
        {
            //Debug.Log("Entro en el Desactivo");
            ColorBlock cb = buttonSim.colors;
            cb.normalColor = Color.grey;
            cb.highlightedColor = Color.grey;
            cb.selectedColor = Color.grey;
            cb.pressedColor = Color.red;
            buttonSim.colors = cb;
            //buttonSim.enabled = false;
        }
    }

    public void Change(string name)
    {
        if (PlayerPrefs.HasKey("UnlockSim") == false)
        {
            StartCoroutine(ShowAlertCanvas(alertSim));
           
        }

        else
        {
           SceneManager.LoadScene(name);
        }
    }

    public void activeMenu()
    {
        if (PlayerPrefs.HasKey("UnlockSim") == false)
        {
            StartCoroutine(ShowAlertCanvas(alertSim));
            panelMenuPrincipal.SetActive(true);
            panelSimulacionesDesactivado.SetActive(false);
        }

        else
        {
            panelSimulacionesDesactivado.SetActive(true);
            panelMenuPrincipal.SetActive(false);
        }
    } */


    // Update is called once per frame

}

