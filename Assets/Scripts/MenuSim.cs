using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSim : MonoBehaviour
{

    public GameObject wristUI;
    public Button[] botonesSim;
    public AudioSource audioFondo;
    public AudioSource audioGuia;
    public bool activeWristUI = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayWristUI()
    {
        if (activeWristUI)
        {
            desPause();

        }
        else if (!activeWristUI)
        {
            wristUI.SetActive(true);
            activeWristUI = true;
            SetInteractableState(false);
            audioFondo.Pause();
            audioGuia.Pause();
            Time.timeScale = 0;

        }

    }
    public void desPause()
    {
        wristUI.SetActive(false);
        activeWristUI = false;
        SetInteractableState(true);
        audioFondo.UnPause();
        audioGuia.UnPause();
        Time.timeScale = 1;
    }

    public void PausaButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
            DisplayWristUI();
    }
    void SetInteractableState(bool state)
    {
        // Obtener todos los botones u otros elementos interactivos en la escena y establecer su estado interactable

        foreach (Button button in botonesSim)
        {
            button.interactable = state;
        }

        // Puedes incluir otros tipos de elementos interactivos que desees desactivar durante la pausa
    }


    public void GoToMenu()
    {
        SceneManager.LoadScene("MenúPrincipal");
    }
    public void ResetSim()
    {   

        desPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
