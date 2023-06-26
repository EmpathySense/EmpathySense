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
        // Obtener una referencia a la acción del botón de salida desde el archivo de inputs
        exitButtonAction = new InputAction("HardReset/SalirHard");
        exitButtonAction.Enable();

        // Asignar el método ExitButtonPressed al evento OnPerformed de la acción del botón
        exitButtonAction.performed += ExitButtonPressed;
        */
    }

    void ExitButtonPressed(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            SceneManager.LoadScene("MenúPrincipal");

            if (PlayerPrefs.HasKey("MalaExp")==false)
            {
            PlayerPrefs.SetInt("MalaExp", 1);
            //Debug.Log(PlayerPrefs.GetInt("MalaExp"));

            }
        }
    }
}

