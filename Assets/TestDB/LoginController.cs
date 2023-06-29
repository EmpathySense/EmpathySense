using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginController : MonoBehaviour
{
    public Button LoginButton;
    // public TMP_InputField UsernameInput;
    // public TMP_InputField PasswordInput;
    private string Username;
    private string Password;
    void Start()
    {
        //UsernameInput.text = "admin@admin"; //ingresar correo
        // UsernameInput.text = "test@admin"; //ingresar correo
        // PasswordInput.text = "12345666";  //ingresar password
        Username = "test@admin"; //ingresar correo
        Password = "12345666";  //ingresar password
        LoginButton.onClick.AddListener(Login);
    }

    async void Login()
    {
        if (await RealmController.Instance.Login(Username, Password) != "")
        {
            RealmController.Instance.IsCreated();
            // SceneManager.LoadScene("Men�Principal");
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