using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class SignUpController : MonoBehaviour
{
    // TMP Button
    public Button Button;
    public TMP_InputField FirstNameInput;
    public TMP_InputField LastNameInput;
    public TMP_InputField AgeInput;
    public TMP_InputField RoleInput;
    public TMP_InputField OrganizationInput;

    void Start()
    {
        Button.onClick.AddListener(SignUp);
    }

    void SignUp()
    {

        Users _user = RealmController.Instance.GetUser();

        Users _new = new Users();
        _new.UserId = _user.UserId;
        _new.FirstName = FirstNameInput.text;
        _new.LastName = LastNameInput.text;
        _new.Age = int.Parse(AgeInput.text);
        _new.Role = RoleInput.text;
        _new.CreationDate = _user.CreationDate;
        _new.Organization = OrganizationInput.text;

        RealmController.Instance.UpdateUser(_new);
        SceneManager.LoadScene("Introduccion");
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    
}
