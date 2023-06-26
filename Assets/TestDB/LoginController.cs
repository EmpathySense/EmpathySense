using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginController : MonoBehaviour
{
    public Button LoginButton;
    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    void Start()
    {
        UsernameInput.text = "test@admin"; //ingresar correo
        PasswordInput.text = "12345666";  //ingresar password
        LoginButton.onClick.AddListener(Login);
    }

    async void Login()
    {
        if (await RealmController.Instance.Login(UsernameInput.text, PasswordInput.text) != "")
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

}