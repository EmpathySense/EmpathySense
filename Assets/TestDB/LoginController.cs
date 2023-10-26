using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginController : MonoBehaviour
{
    public TMP_InputField UsernameInput;
    public TMP_InputField PasswordInput;
    public Button LoginButton;

    void Start()
    {
        LoginButton.onClick.AddListener(Login);
    }


    async void Login()
    {
         
        if (await RealmController.Instance.Login("test@test", "12345666") != "") //agregar excepciones
        {
            RealmController.Instance.IsCreated();
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