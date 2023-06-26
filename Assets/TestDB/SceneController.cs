using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
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
        Debug.Log("estoy en start");
        Button.onClick.AddListener(Sex);
    }

    void Sex()
    {
        Debug.Log("el boton entra");
        Users _user = RealmController.Instance.GetUser();
        Debug.Log(_user.UserId);
        Debug.Log(_user.FirstName);
        
        // Debug.Log("el boton entra");
        // Users _user = RealmController.Instance.GetUser();
        // Debug.Log(_user.UserId);
        // Debug.Log(_user.FirstName);
        // Users _new = new Users();
        // _new.UserId = _user.UserId;
        // _new.FirstName = FirstNameInput.text;
        // _new.LastName = LastNameInput.text;
        // _new.Age = int.Parse(AgeInput.text);
        // _new.Role = RoleInput.text;
        // _new.Organization = OrganizationInput.text;
        // RealmController.Instance.UpdateUser(_new);
        // Debug.Log(_new.UserId);
    }

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    
}
