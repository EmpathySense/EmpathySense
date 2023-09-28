using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Ink.Runtime;

/* 
    Esta es la tercera (creo?) vez que he hecho un copy paste de un
    script de tipo dialogue manager solo para hacer cambios menores
    en partes muy especificas y aun me da paja econtrar una solucion
    mejor para este problema. Se que esto seria muy facil de resolver
    con herencia (o al menos eso creo) pero no logro juntar la fuerza
    para hacerlo y cada sprint que pasa la solucion se vuelve mas
    dificil de hacer. Dios sabe que si pudiera planificar y hacer
    buenas estructuras de codigo/datos a la primera seria demasiado
    poderoso.
    */
public class EscenarioConFeedbackManager : MonoBehaviour
{
    

    private static EscenarioConFeedbackManager _instance;

    [SerializeField] private Animator igancioAnimator;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text speakerName;
    // [SerializeField] private TMP_Text finalScore;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject endButtons;
    [SerializeField] private GameObject[] choices;
    private TMP_Text[] choicesText;

    [SerializeField] private GameObject guia;
    [SerializeField] private TMP_Text guiaText;
    private bool guiaIsTalking = false;

    private int correctAnswers;
    private int mistakes;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private const string SPEAKER_TAG = "title";
    private const string ANIMATION_TRIGGER_TAG = "animation";
    private const string END_DIALOGUE_TAG = "EndDialogue";
    private const string SWITCH_DIALOGUE_TAG = "dialog";

    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Hay más de una instancia de Scenario2DialogueManager en la escena");
        }
        _instance = this;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        choicesPanel.SetActive(false);
        
        dialoguePanel.SetActive(false);
        correctAnswers = 0;
        mistakes = 0;

        choicesText = new TMP_Text[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TMP_Text>();
            index++;
        }
    }

    public void EnterDialogueMode(TextAsset inkJson)
    {
        currentStory = new Story(inkJson.text);
        // currentStory.ObserveVariable("correctAnswers", (variableName, newValue) => {
        //     correctAnswers = (int)newValue;
        //     Debug.Log("Correct answers: " + correctAnswers);
        // });
        // currentStory.ObserveVariable("mistakes", (variableName, newValue) => {
        //     mistakes = (int)newValue;
        //     Debug.Log("Mistakes: " + mistakes);
        // });
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        

        ContinueDialogueMode();
    }

    public void ContinueDialogueMode()
    {
        if (currentStory.canContinue)
        {
            

            if (guiaIsTalking)
            {
                guia.SetActive(true);
                continueButton.SetActive(false);
                guiaText.text = currentStory.Continue();
            }
            else
            {
                guia.SetActive(false);
                continueButton.SetActive(true);
                dialogueText.text = currentStory.Continue();
            }


            // Si hay elecciones disponibles, se mostraran en el panel de elecicones
            DisplayChoices();

            HandleTags(currentStory.currentTags);
        }
        else
        {
            CloseDialoguePanel();
        }

        
    }

    

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;


        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("Hay muchas elecciones en el archivo .ink para la UI");
        }

        if (currentChoices.Count == 0)
        {
            return;
        }

        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        continueButton.SetActive(false);
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(true);
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            
            string[] splitTag = tag.Split(":");
            if (splitTag.Length != 2)
            {
                Debug.LogError("Hubo un error parseando el tag: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case SPEAKER_TAG:

                    speakerName.SetText(tagValue);

                    break;

                case ANIMATION_TRIGGER_TAG:
                    
                    igancioAnimator.StopPlayback();
                    igancioAnimator.SetTrigger(tagValue);

                    break;

                case END_DIALOGUE_TAG:

                    ExitDialogueMode();

                    break;
                
                case SWITCH_DIALOGUE_TAG:

                    if (tagValue == "guide")
                    {
                        guiaIsTalking = true;
                    }
                    else if (tagValue == "normal")
                    {
                        guiaIsTalking = false;
                    }

                    break;

                default:
                    Debug.LogWarning("No se esta manejando el tag: " + tagValue);
                    break;
            }
        }
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        dialoguePanel.SetActive(true);
        continueButton.SetActive(true);
        choicesPanel.SetActive(false);
        ContinueDialogueMode();
    }

    private void CloseDialoguePanel()
    {
        choicesPanel.SetActive(false);
        dialoguePanel.SetActive(false);
    }

    private void ExitDialogueMode()
    {
        currentStory.RemoveVariableObserver(null, "correctAnswers");
        currentStory.RemoveVariableObserver(null, "mistakes");
        //TODO: Aquí es donde se tienen que manejar los erroes del jugador
        //usando las variables correctAnswers y mistakes
        
        int suma = correctAnswers + mistakes;
        Debug.Log("Suma correctas y errores" + suma);


        dialogueIsPlaying = false;
        dialoguePanel.SetActive(true);
        choicesPanel.SetActive(false);
        dialogueText.text = "";
        speakerName.text = "";

        EndScreen();
    }

    private void EndScreen()
    {
        speakerName.text = "Resultados";
        dialogueText.alignment = TextAlignmentOptions.Center;
        dialogueText.text = string.Format("Respuestas correctas: {0}\nRespuestas incorrectas: {1}\nCantidad de intentos: {2}", correctAnswers, mistakes, correctAnswers+mistakes);
        endButtons.SetActive(true);
        continueButton.SetActive(false);

        // RealmController.Instance.CreateHistory(correctAnswers, mistakes, correctAnswers+mistakes, 0, 0);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenúPrincipal");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static EscenarioConFeedbackManager GetInstance()
    {
        return _instance;
    }
}
