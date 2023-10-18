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


    public void Login()
    {
        // if (await RealmController.Instance.Login(UsernameInput.text, PasswordInput.text) != "") //agregar excepciones
        // {
        //     RealmController.Instance.IsCreated();
        // }
        SceneManager.LoadScene("IntroRecrear");
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

}