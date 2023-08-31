using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("FirstSesion"))
        {
            Debug.Log("Ya está creado");
        }
        else{
            PlayerPrefs.SetInt("FirstSesion", 0);
        }
    }

    // Update is called once per frame
    public void ChangeScene()
    {
        SceneManager.LoadScene("MenúPrincipal");

    }

    public void ClearPrefs()
    {
        PlayerPrefs.DeleteKey("FirstSesion");
        Debug.Log("PlayerPrefs borradas.");
    }
}
