using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public static bool panelSimulacion;
    public Button buttonSim;
    public GameObject panelSimulacionesDesactivado;
    public GameObject panelMenuPrincipal;
    // Start is called before the first frame update
    void Start()
    {
        if (panelSimulacion == true)
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

            Debug.Log("Entro en el activo");
            buttonSim.enabled=false;

        }
        else if (PlayerPrefs.HasKey("UnlockSim")==false)
        {
            Debug.Log("Entro en el Desactivo");
            buttonSim.enabled = false;
        }
    }



    
    // Update is called once per frame
    
}

