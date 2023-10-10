using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;





public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    
    public TMP_Text narratorText;
    public TMP_Text titleText;
    public Animator businessManAnimator;
    public Animator businessWomanAnimator;


    public GameObject alertCanvas;
    public GameObject modalPanel;

    [SerializeField]
    private GameObject continueButton, nextStepButton, returnButton;

    [SerializeField, TextArea(3,5)]
    private string endOfStepText;
    private Queue<Dialogue.Sentences> sentences;

    
    
    private Dictionary<string, string> prefsUpdates = new Dictionary<string, string>
    {   
        {"Paso-A", "b"},
        {"Paso-B", "c"},
        {"Paso-C", "d"},
        {"Paso-D", "e"},
        {"Paso-E", "s"}
        
    };
    
    private Button boton;

    
    void Start()
    {
        Prefs prefs_User = RealmController.Instance.GetPrefs();

    }
    
    // Start is called before the first frame update
    
    
    void Awake()
    {
        
        sentences = new Queue<Dialogue.Sentences>();

        if (_instance != null)
        {
            Debug.LogWarning("Hay más de una instancia de DialogueManager en la escena");
        }
        _instance = this;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting dialogue...");

        sentences.Clear();

        foreach (Dialogue.Sentences sentence in dialogue.sentences)
        {
            this.sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (this.sentences.Count == 0)
        {
            Debug.Log("End of conversation");
            EndDialogue();
            return;
        }


        Dialogue.Sentences sentence = sentences.Dequeue();


        if (sentence.characterName != null)
        {
        titleText.SetText(sentence.characterName);
        }
        
        narratorText.SetText(sentence.text);

        if (sentence.triggersAnimation) 
        {
            businessManAnimator.StopPlayback();
            businessManAnimator.SetTrigger(sentence.triggerName);
            businessWomanAnimator.StopPlayback();
            businessWomanAnimator.SetTrigger(sentence.triggerName);
        }
        
    }

    public void EndDialogue()
    {   
        Prefs prefs_User = RealmController.Instance.GetPrefs();
        Debug.Log("hols");
        int unlockedLvel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        Dictionary<string, bool> prefsCheck = new Dictionary<string, bool>
    {   
        {"Paso-A", prefs_User.InfoB},
        {"Paso-B", prefs_User.InfoC},
        {"Paso-C", prefs_User.InfoD},
        {"Paso-D", prefs_User.InfoE},
        {"Paso-E", prefs_User.InfoSim}
        
    };

        Debug.Log("BOOL: "+prefsCheck[SceneManager.GetActiveScene().name]);
        if (prefsCheck[SceneManager.GetActiveScene().name])
        {
            Debug.Log("Tiene alert");
            StartCoroutine(ShowUnableToOpenCanvas(alertCanvas));
            if (SceneManager.GetActiveScene().name == "Paso-A") RealmController.Instance.UpdatePrefs("b");
            if (SceneManager.GetActiveScene().name == "Paso-B") RealmController.Instance.UpdatePrefs("c");
            if (SceneManager.GetActiveScene().name == "Paso-C") RealmController.Instance.UpdatePrefs("d");
            if (SceneManager.GetActiveScene().name == "Paso-D") RealmController.Instance.UpdatePrefs("e");
            narratorText.SetText(endOfStepText);
            boton.gameObject.SetActive(true);
            continueButton.SetActive(false);
            returnButton.SetActive(true);
            
        }

        if (prefs_User.InfoSim==true && SceneManager.GetActiveScene().name == "Paso-E")
        {
            RealmController.Instance.UpdatePrefs("s");
            //PlayerPrefs.SetInt("UnlockSim", 1);
            /*Debug.Log(PlayerPrefs.GetInt("UnlockSim", 1));
            GetButton();
            Debug.Log("NOMBRE DEL BTON: "+ boton.name);
            // Update Modal
            narratorText.SetText(endOfStepText);
            boton.gameObject.SetActive(true);
            continueButton.SetActive(false);
            returnButton.SetActive(true);*/
            narratorText.SetText(endOfStepText);
            boton.gameObject.SetActive(true);
            continueButton.SetActive(false);
            returnButton.SetActive(true);
        }

        if (SceneManager.GetActiveScene().name == "Paso-E" )
        {
            
            narratorText.SetText(endOfStepText);
            continueButton.SetActive(false);
            // nextStepButton.SetActive(false);
            returnButton.SetActive(true);
            // GetButton();
            // boton.gameObject.SetActive(true);
            nextStepButton.SetActive(true);
        }
        else
        {
            // Update Modal
            narratorText.SetText(endOfStepText);
            returnButton.SetActive(true);
            continueButton.SetActive(false);
            nextStepButton.SetActive(true);
        }
    


        //Debug.Log("Desbloqueo: "+ PlayerPrefs.GetInt("UnlockedLevel", 1));


    }

    void GetButton()
    {
        
        GameObject panel = GameObject.Find("Modal");

        Button[] botonesPanel = panel.GetComponentsInChildren<Button>(true);

        foreach (Button bP in botonesPanel)
        {
        
            if (bP.name == "SimButton")
            {
                // Se encontró el botón deseado, puedes acceder a él y realizar las acciones deseadas
                boton = bP;

                break;
            }
        }
    }
    public void GoToMainMenu()
    {
        MenuPrincipal.panelSimulacion = false;
        string levelName = "MenúPrincipal";
        SceneManager.LoadScene(levelName);
    }

    public void GoToNextScene(string Level)
    {
        string levelName = "Paso-" + Level;
        SceneManager.LoadScene(levelName);
    }

    public void GoToMainMenuSim()
        {

        // MenuPrincipal.panelSimulacion = true;
            PanelState.instance.activarMenuSIM = true;
            string levelName = "MenúPrincipal";
            SceneManager.LoadScene(levelName);
        }

    IEnumerator ShowUnableToOpenCanvas(GameObject canvas)
    {
        Debug.Log("Saliendo Canva...");
        
        canvas.SetActive(true); // Activa el canvas

        yield return new WaitForSeconds(2f); // Espera durante 2 segundos

        canvas.SetActive(false); // Desactiva el canvas
    }

    void UnlockNewLevel()
    {   
        Prefs prefs_User = RealmController.Instance.GetPrefs();
        string nameScene = SceneManager.GetActiveScene().name;
        Debug.Log("Nombre SCENE: "+nameScene);
        Debug.Log("Key: "+prefsUpdates[nameScene]);
        RealmController.Instance.UpdatePrefs(prefsUpdates[nameScene]);

        
        /* if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);

            PlayerPrefs.Save();
        } */

    }

    public static DialogueManager GetInstance()
    {
        return _instance;
    }

}
