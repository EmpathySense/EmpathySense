using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadInputs : MonoBehaviour
{   
    public TMP_InputField inputName;
    public TMP_InputField inputLastName;
    public TMP_InputField inputOrganization;
    public TMP_InputField inputRole;
    // Start is called before the first frame update
    public void ReadInput()
    {
        Debug.Log("Name: " + inputName.text);
        Debug.Log("Last Name: " + inputLastName.text);
        Debug.Log("Organization: " + inputOrganization.text);
        Debug.Log("Role: " + inputRole.text);
    }
    
    public void UpdateInputs()
    {
        Users userActual = RealmController.Instance.GetUser();

        Users userUpdate = new Users();
        userUpdate.UserId= userActual.UserId;
        userUpdate.FirstName = inputName.text;
        userUpdate.LastName = inputLastName.text;
        userUpdate.Organization = inputOrganization.text;
        userUpdate.Role = inputRole.text;
        userUpdate.Age = userActual.Age;
        userUpdate.CreationDate = userActual.CreationDate;

        RealmController.Instance.UpdateUser(userUpdate);
    }

    
}
