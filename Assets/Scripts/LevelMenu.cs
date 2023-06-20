using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelMenu : MonoBehaviour
{
    public Button[] botones;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            botones[i].interactable = true;
            
        }
    }

    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
    public void Change()
    {
        string levelName = "MenúPrincipal";
        SceneManager.LoadScene(levelName);
        
    }
   public void OpenLevel(string Level)
    {
        string levelName = "Paso " + Level;
        SceneManager.LoadScene(levelName);
        
        
    }

    

    
}
