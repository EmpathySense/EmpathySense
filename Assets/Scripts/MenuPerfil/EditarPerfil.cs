using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

using System;

public class EditarPerfil : MonoBehaviour
{
    private Users user;
    // Start is called before the first frame update
    public TMP_Text namePrincipalPanel ;

    public TMP_Text nombreText;
    public TMP_Text apellidoText;
    public TMP_Text edadText;
    public TMP_Text organizacionText;
    public TMP_Text rolText;


    
    void Start()
    {
        if (RealmController.Instance != null)
        {
            user = RealmController.Instance.GetUser();
            string name= user.FirstName + " "+ user.LastName;
            namePrincipalPanel.text=name;
            //Debug.Log("Nombre: "+ user.FirstName);
            nombreText.text="Nombre: "+user.FirstName;
            apellidoText.text="Apellido: "+user.LastName;
            edadText.text="Edad: "+user.Age;
            organizacionText.text="Organización: "+user.Organization;
            rolText.text="Rol: "+user.Role;
            //Debug.Log("ALO: "+ user._partition);

            //History wea = RealmController.Instance.CreateHistory(4,2,5,3,2,12,3,5,8,2,"Lugar Público", "Muy mas o menos");
            
            
            
            //Debug.Log(allHistory.Count);
        }
        else
        {
            Debug.LogError("RealmController.Instance is null. Make sure it's properly initialized.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
