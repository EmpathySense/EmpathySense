using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelMenu : MonoBehaviour
{
    public Button[] botones;
    public GameObject canvasB;
    public GameObject canvasC;
    public GameObject canvasD;
    public GameObject canvasE;

    public Dictionary<string, GameObject> unableToOpenCanvases;





    public bool[] mensaje;
    
    Dictionary<string, int> levelIndexMap = new Dictionary<string, int>()
    {
        { "A", 0 },
        { "B", 1 },
        { "C", 2 },
        { "D", 3 },
        { "E", 4 }
    };


    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        unableToOpenCanvases = new Dictionary<string, GameObject>()
    {
        { "B", canvasB },
        { "C", canvasC },
        { "D", canvasD },
        { "E", canvasE }
        // Agrega las asignaciones para cada nivel y su respectivo canvas
    };


        for (int i = 0; i < unlockedLevel; i++)
        {

            mensaje[i] = true;

        }


        for (int i = 0; i < botones.Length; i++)
        {
            
            if (mensaje[i] == false)
            {
                Button b = botones[i].GetComponent<Button>();
                ColorBlock cb = b.colors;
                cb.normalColor = Color.grey;
                cb.highlightedColor = Color.grey;
                cb.selectedColor = Color.grey;
                cb.pressedColor = Color.red;
                b.colors = cb;
                
            }
            //botones[i].interactable = false;
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            
            Button b = botones[i].GetComponent<Button>();
            ColorBlock cb = b.colors;
            cb.normalColor = Color.white;
            cb.highlightedColor = Color.yellow;
            b.colors = cb;

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
        

        if (levelIndexMap.ContainsKey(Level) && mensaje[levelIndexMap[Level]])
        {
            string levelName = "Paso-" + Level;
            SceneManager.LoadScene(levelName);
        }
        else 
        {
            if (unableToOpenCanvases.ContainsKey(Level))
            {
                GameObject canvas = unableToOpenCanvases[Level];
                StartCoroutine(ShowUnableToOpenCanvas(canvas));
            }

        }
        

    }
    IEnumerator ShowUnableToOpenCanvas(GameObject canvas)
    {
        canvas.SetActive(true); // Activa el canvas

        yield return new WaitForSeconds(2f); // Espera durante 2 segundos

        canvas.SetActive(false); // Desactiva el canvas
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    /*public void Update()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        Debug.Log("update " + unlockedLevel );
        for (int i = 0; i < unlockedLevel; i++)
        {

            mensaje[i] = true;
            Debug.Log("iteracion " + i + mensaje[i]);
        }
    }*/






}
