using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HardReinicio : MonoBehaviour
{
    private InputAction exitButtonAction;

    void Start()
    {   
        /*
        // Obtener una referencia a la acci�n del bot�n de salida desde el archivo de inputs
        exitButtonAction = new InputAction("HardReset/SalirHard");
        exitButtonAction.Enable();

        // Asignar el m�todo ExitButtonPressed al evento OnPerformed de la acci�n del bot�n
        exitButtonAction.performed += ExitButtonPressed;
        */
    }

    void ExitButtonPressed(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            SceneManager.LoadScene("IntroRecrear");

            
            //Debug.Log(PlayerPrefs.GetInt("MalaExp"));

            
        }
    }
}

