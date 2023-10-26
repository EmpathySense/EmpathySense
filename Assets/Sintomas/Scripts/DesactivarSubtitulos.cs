using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DesactivarSubtitulos : MonoBehaviour
{
    public GameObject subtitulos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void DeactivateWhenPressed(InputAction.CallbackContext context)
    {
        if (context.performed && subtitulos.activeInHierarchy == true)
        {
            Debug.Log("entro");
            subtitulos.SetActive(false);
        }
        else if (context.performed && subtitulos.activeSelf == false)
        {
            subtitulos.SetActive(true);
        }
        
    }
}
