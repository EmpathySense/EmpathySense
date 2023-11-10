using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DesactivarSubtitulos : MonoBehaviour
{
    public GameObject subtitulos;
    private bool activado = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DeactivateWhenPressed(InputAction.CallbackContext context)
    {
        Debug.Log("HOLAHOLAHOLA");
        if (context.performed && activado)
        {
            Debug.Log("entro");
            subtitulos.SetActive(false);
            activado = false;
        }
        else if (context.performed && !activado)
        {
            subtitulos.SetActive(true);
            activado = true;
            Debug.Log("Salgo");
        }
        
    }
}
