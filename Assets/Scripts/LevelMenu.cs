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

    public Dictionary<int, GameObject> unableToOpenCanvases;

    public bool[] mensaje;
    
    //
    Dictionary<int, string> levelIndexMap = new Dictionary<int, string>()
    {
        { 0, "Paso-A" },
        { 1, "Paso-B" },
        { 2, "Paso-C" },
        { 3, "Paso-D" },
        { 4, "Paso-E" }
    };

    Dictionary<int, string> IndexMap = new Dictionary<int, string>()
    {
        { 0, "InfoA"},
        { 1, "InfoB"},
        { 2, "InfoC"},
        { 3, "InfoD"},
        { 4, "InfoE"}
    };


    void Start()
    {   

        Prefs prefs_User = RealmController.Instance.GetPrefs();
        //Debug.Log(nameof(prefs_User.InfoA));
        //int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        unableToOpenCanvases = new Dictionary<int, GameObject>()
    {
        { 1, canvasB },
        { 2, canvasC },
        { 3, canvasD },
        { 4, canvasE }
        // Agrega las asignaciones para cada nivel y su respectivo canvas
    };




        for (int i = 0; i < botones.Length; i++)
        {
            Debug.Log("index: "+i);
            switch(i)
            {
                case 0:
                    if (prefs_User.InfoA)
                    {   
                        Button b = botones[i].GetComponent<Button>();
                        ColorBlock cb = b.colors;
                        cb.normalColor = Color.grey;
                        cb.highlightedColor = Color.grey;
                        cb.selectedColor = Color.grey;
                        cb.pressedColor = Color.red;
                        b.colors = cb;
                        
                    }else
                    {
                        Button b = botones[i].GetComponent<Button>();
                        ColorBlock cb = b.colors;
                        cb.normalColor = Color.white;
                        cb.highlightedColor = Color.yellow;
                        b.colors = cb;
                        b.onClick.AddListener(()=> newOpenLevel(0));
                    }break;
                case 1:
                    if (prefs_User.InfoB)
                    {
                        Button b = botones[i].GetComponent<Button>();
                        ColorBlock cb = b.colors;
                        cb.normalColor = Color.grey;
                        cb.highlightedColor = Color.grey;
                        cb.selectedColor = Color.grey;
                        cb.pressedColor = Color.red;
                        b.colors = cb;
                        b.onClick.AddListener(() => StartCoroutine(ShowUnableToOpenCanvas(unableToOpenCanvases[1])));
                        //b.onClick.AddListener(() => pico(unableToOpenCanvases[1]));

                        
                    }else
                    {
                        Button b = botones[i].GetComponent<Button>();
                        ColorBlock cb = b.colors;
                        cb.normalColor = Color.white;
                        cb.highlightedColor = Color.yellow;
                        b.colors = cb;
                        b.onClick.AddListener(()=> newOpenLevel(1));
                    }
                    break;
                case 2:
                    if (prefs_User.InfoC)
                    {
                        Button b = botones[i].GetComponent<Button>();
                        ColorBlock cb = b.colors;
                        cb.normalColor = Color.grey;
                        cb.highlightedColor = Color.grey;
                        cb.selectedColor = Color.grey;
                        cb.pressedColor = Color.red;
                        b.colors = cb;
                        b.onClick.AddListener(()=> StartCoroutine(ShowUnableToOpenCanvas(unableToOpenCanvases[2])));
                        
                    }else
                    {
                        Button b = botones[i].GetComponent<Button>();
                        ColorBlock cb = b.colors;
                        cb.normalColor = Color.white;
                        cb.highlightedColor = Color.yellow;
                        b.colors = cb;
                        b.onClick.AddListener(()=> newOpenLevel(2));
                    }
                    break;
                case 3:
                    if (prefs_User.InfoD)
                    {
                        Button b = botones[i].GetComponent<Button>();
                        ColorBlock cb = b.colors;
                        cb.normalColor = Color.grey;
                        cb.highlightedColor = Color.grey;
                        cb.selectedColor = Color.grey;
                        cb.pressedColor = Color.red;
                        b.colors = cb;
                        b.onClick.AddListener(()=> StartCoroutine(ShowUnableToOpenCanvas(unableToOpenCanvases[3])));
                        
                    }else
                    {
                        Button b = botones[i].GetComponent<Button>();
                        ColorBlock cb = b.colors;
                        cb.normalColor = Color.white;
                        cb.highlightedColor = Color.yellow;
                        b.colors = cb;
                        b.onClick.AddListener(()=> newOpenLevel(3));
                    }
                    break;
                case 4:
                    if (prefs_User.InfoE)
                    {
                        Button b = botones[i].GetComponent<Button>();
                        ColorBlock cb = b.colors;
                        cb.normalColor = Color.grey;
                        cb.highlightedColor = Color.grey;
                        cb.selectedColor = Color.grey;
                        cb.pressedColor = Color.red;
                        b.colors = cb;
                        b.onClick.AddListener(()=> StartCoroutine(ShowUnableToOpenCanvas(unableToOpenCanvases[4])));
                    }else
                    {
                        Button b = botones[i].GetComponent<Button>();
                        ColorBlock cb = b.colors;
                        cb.normalColor = Color.white;
                        cb.highlightedColor = Color.yellow;
                        b.colors = cb;
                        b.onClick.AddListener(()=> newOpenLevel(4));
                    }
                    break;
            }
            /* if (mensaje[i] == false)
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

        } */

    }

    
    IEnumerator ShowUnableToOpenCanvas(GameObject canvas)
    {   
        Debug.Log("ALOOOO");
        canvas.SetActive(true); // Activa el canvas

        yield return new WaitForSeconds(2f); // Espera durante 2 segundos

        canvas.SetActive(false); // Desactiva el canvas
    } 

    void newOpenLevel(int num)
    {
        
        SceneManager.LoadScene(levelIndexMap[num]);
    }


    /* void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            
            PlayerPrefs.Save();
        }
        
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

    */
    
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
}
