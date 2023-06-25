using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;





public class DialogueManager : MonoBehaviour
{
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
    
    
    
    private Button boton;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<Dialogue.Sentences>();
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

    private void EndDialogue()
    {
        int unlockedLvel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (PlayerPrefs.HasKey("UnlockSim") == false)
        {
            StartCoroutine(ShowUnableToOpenCanvas(alertCanvas));
        }

        if (PlayerPrefs.HasKey("UnlockSim") == false && SceneManager.GetActiveScene().name == "Paso-E")
        {
            
            PlayerPrefs.SetInt("UnlockSim", 1);
            /*Debug.Log(PlayerPrefs.GetInt("UnlockSim", 1));
            GetButton();
            Debug.Log("NOMBRE DEL BTON: "+ boton.name);
            // Update Modal
            narratorText.SetText(endOfStepText);
            boton.gameObject.SetActive(true);
            continueButton.SetActive(false);
            returnButton.SetActive(true);*/
        }

        if (SceneManager.GetActiveScene().name == "Paso-E" )
        {
            
            narratorText.SetText(endOfStepText);
            continueButton.SetActive(false);
            nextStepButton.SetActive(false);
            returnButton.SetActive(true);
            GetButton();
            boton.gameObject.SetActive(true);
        }
        else if (unlockedLvel<6)
        {
            UnlockNewLevel();
            
            // Update Modal
            narratorText.SetText(endOfStepText);
            returnButton.SetActive(true);
            continueButton.SetActive(false);
            nextStepButton.SetActive(true);
        }
        




        //Debug.Log("Desbloqueo: "+ PlayerPrefs.GetInt("UnlockedLevel", 1));

        // Update Modal

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

        MenuPrincipal.panelSimulacion = true;
            
            string levelName = "MenúPrincipal";
            SceneManager.LoadScene(levelName);
        }

    IEnumerator ShowUnableToOpenCanvas(GameObject canvas)
    {
        canvas.SetActive(true); // Activa el canvas

        yield return new WaitForSeconds(2f); // Espera durante 2 segundos

        canvas.SetActive(false); // Desactiva el canvas
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

}
