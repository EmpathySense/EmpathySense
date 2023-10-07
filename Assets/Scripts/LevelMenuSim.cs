using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LevelMenuSim : MonoBehaviour
{
    private string Lvl;

    // Start is called before the first frame update
    void Start()
    {
        Lvl = ""; 
    }  

    public void ChangeLvl (string GoLevel)
    {
        
        Lvl = GoLevel;
        
    }

    public void GoToLevel () 
    {
        SceneManager.LoadScene(Lvl);

    }

    
}
