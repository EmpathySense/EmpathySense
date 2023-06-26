using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene("EscenaTesting",LoadSceneMode.Single);
    }

}
