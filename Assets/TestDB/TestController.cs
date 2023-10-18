using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class TestController : MonoBehaviour
{
    public Button RecrearButton;
    public Button ExitButton;

    void Start()
    {
        RecrearButton.onClick.AddListener(Recrear);
        ExitButton.onClick.AddListener(Exit);
    }

    public void Recrear()
    {
        SceneManager.LoadScene("Sintomas");
    }
    public void Exit()
    {
        Application.Quit();
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
