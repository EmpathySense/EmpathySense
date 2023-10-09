using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{

    public GameObject wristUI;
    public GameObject pasosUI;
    public AudioSource audioSource;
    public bool activeWristUI=true;
    public Button[] botones;
    public GameObject levelBotones;
    private float yOffset;
    public Button[] botonesPausa;

    Dictionary<string, int> levelIndexMap = new Dictionary<string, int>()
    {
        { "A", 0 },
        { "B", 1 },
        { "C", 2 },
        { "D", 3 },
        { "E", 4 }
    };
    private void Start()
    {   
        
        DisplayWristUI();
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void DisplayWristUI()
    {
        if (activeWristUI)
        {
            desPause();
            

        }else if (!activeWristUI)
        {
            wristUI.SetActive(true);
            activeWristUI = true;
            SetInteractableState(false);
            audioSource.Pause();
            Time.timeScale = 0;


        }

    }
    void SetInteractableState(bool state)
    {
        // Obtener todos los botones u otros elementos interactivos en la escena y establecer su estado interactable

        foreach (Button button in botonesPausa)
        {
            button.interactable = state;
        }

        // Puedes incluir otros tipos de elementos interactivos que desees desactivar durante la pausa
    }
    public void PausaButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
            DisplayWristUI();
    }
    public void Change()
    {
        string levelName = "MenúPrincipal";
        SceneManager.LoadScene(levelName);

    }

    public void GoToLevel()
    {
        int unlockedLevel = CountFalse();
        Debug.Log("Unlock " + unlockedLevel);
        foreach (Button boton in botones)
        {
            boton.gameObject.SetActive(false);
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            if (i < botones.Length)
            {
                
                botones[i].gameObject.SetActive(true);
            }
        }
        GridLayoutGroup gridLayout = levelBotones.GetComponent<GridLayoutGroup>();

        // Ajusta el valor según la ubicación deseada en el eje Y

        yOffset = (unlockedLevel - 1) * 100;
        levelBotones.transform.localPosition = new Vector3(levelBotones.transform.localPosition.x, yOffset, levelBotones.transform.localPosition.z);
        // Ajusta el GridLayoutGroup para refrescar la disposición de los botones
        //GridLayoutGroup gridLayout = levelBotones.GetComponent<GridLayoutGroup>();
        //gridLayout.enabled = false;
        //gridLayout.enabled = true;
    }


    public void desPause()
    {
        wristUI.SetActive(false);
        activeWristUI = false;
        SetInteractableState(true);
        Time.timeScale = 1;
        audioSource.UnPause();
        pasosUI.SetActive(false);
    }
    public void OpenLevel(string Level)
    {


        if (levelIndexMap.ContainsKey(Level))
        {
            string levelName = "Paso-" + Level;
            SceneManager.LoadScene(levelName);
        }
    }

    void ButtonsToArray()
    {
        int childCount = PlayerPrefs.GetInt("UnlockedLevel", 1);
        botones = new Button[childCount];
        for (int i=0; i< childCount; i++)
        {
            botones[i] = levelBotones.transform.GetChild(i).gameObject.GetComponent<Button>();
        }
    }

    int CountFalse()
    {
        Prefs prefs_User = RealmController.Instance.GetPrefs();
    
        int falseCount = 0; // Inicializamos el contador a 0
    
        // Comprobamos cada campo y si es false, incrementamos el contador
        if (!prefs_User.InfoA)
        {
            falseCount++;
        }

        if (!prefs_User.InfoB)
        {
            falseCount++;
        }

        if (!prefs_User.InfoC)
        {
            falseCount++;
        }

        if (!prefs_User.InfoD)
        {
            falseCount++;
        }

        if (!prefs_User.InfoE)
        {
            falseCount++;
        }

        // Imprimimos el conteo
        Debug.Log($"Cantidad de campos con 'false': {falseCount}");
        return falseCount;
    }
    private void Awake()
    {   
        
        CountFalse();
        ButtonsToArray();
        GoToLevel();

    }



}
