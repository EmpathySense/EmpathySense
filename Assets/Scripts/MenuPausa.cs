using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject wristUI;

    public bool activeWristUI=true;


    private void Start()
    {
        DisplayWristUI();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DisplayWristUI()
    {
        if (activeWristUI)
        {
            wristUI.SetActive(false);
            activeWristUI = false;
            Time.timeScale = 1;

        }else if (!activeWristUI)
        {
            wristUI.SetActive(true);
            activeWristUI = true;
            Time.timeScale = 0;

        }

    }
    public void PausaButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
            DisplayWristUI();
    }
    public void Change()
    {
        string levelName = "MenúPrincipal";
        SceneManager.LoadScene(levelName);

    }
    public void desPause()
    {
        wristUI.SetActive(false);
        activeWristUI = false;
        Time.timeScale = 1;
    }



}
