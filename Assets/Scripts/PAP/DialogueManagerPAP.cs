using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManagerPAP : MonoBehaviour
{
    private static DialogueManagerPAP _instance;

    [SerializeField] private Animator businessManAnimator;
    [SerializeField] private Animator businessWomanAnimator;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private GameObject logroPanel;
    [SerializeField] private GameObject logroPanel2;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text speakerName;
    // [SerializeField] private TMP_Text finalScore;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject endButtons;
    [SerializeField] private GameObject[] choices;
    private TMP_Text[] choicesText;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private const string SPEAKER_TAG = "title";
    private const string ANIMATION_TRIGGER_TAG = "animation";
    private const string ACHIEVEMENT_TAG = "achievement";
    private const string HIGHLIGHT_TAG = "highlight";


    private void Awake()
    {
        if (_instance != null)
        {
            Debug.LogWarning("Hay más de una instancia de SimulationDialogueManager en la escena");
        }
        _instance = this;
    }

    private void Start()
    {   

        Prefs prefs_User = RealmController.Instance.GetPrefs();
        dialogueIsPlaying = false;
        choicesPanel.SetActive(false);
        
        dialoguePanel.SetActive(false);

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

        

        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        
        
        ContinueDialogueMode();
    }

    public void ContinueDialogueMode()
    {
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();

            // Si hay elecciones disponibles, se mostraran en el panel de elecicones
            DisplayChoices();

            HandleTags(currentStory.currentTags);
        }
        else
        {
            ExitDialogueMode();
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
        // dialoguePanel.SetActive(false);
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
                    
                    businessManAnimator.StopPlayback();
                    businessManAnimator.SetTrigger(tagValue);
                    businessWomanAnimator.StopPlayback();
                    businessWomanAnimator.SetTrigger(tagValue);

                    break;

                case HIGHLIGHT_TAG:

                // TODO: HANDLE HIGHLIGHT

                case ACHIEVEMENT_TAG:

                    switch (tagValue)
                    {
                        case "A":
                            StartCoroutine(ShowAlertCanvas(logroPanel));
                            RealmController.Instance.UpdateLogros("PAP-1-");
                            break;

                        case "B":
                            StartCoroutine(ShowAlertCanvas(logroPanel));
                            RealmController.Instance.UpdateLogros("PAP-2-");
                            break;

                        case "C":
                            StartCoroutine(ShowAlertCanvas(logroPanel));
                            RealmController.Instance.UpdateLogros("PAP-3-");
                            break;

                        case "D":
                            StartCoroutine(ShowAlertCanvas(logroPanel));
                            RealmController.Instance.UpdateLogros("PAP-4-");
                            break;

                        case "LogroPAP":
                            StartCoroutine(ShowAlertCanvas(logroPanel));
                            StartCoroutine(ShowAlertCanvas(logroPanel2));
                            RealmController.Instance.UpdateLogros("PAP-5-");
                            RealmController.Instance.UpdateLogros("PAP-");
                            break;
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


    IEnumerator ShowAlertCanvas(GameObject canvas)
    {

        canvas.SetActive(true); // Activa el canvas
        Debug.Log("Suma correctas y errores");

        yield return new WaitForSeconds(5f); // Espera durante 2 segundos

        canvas.SetActive(false); // Desactiva el canvas
    }

    private void ExitDialogueMode()
    {
        currentStory.RemoveVariableObserver(null, "correctAnswers");
        currentStory.RemoveVariableObserver(null, "mistakes");
        //TODO: Aquí es donde se tienen que manejar los erroes del jugador
        //usando las variables correctAnswers y mistakes
        
        // int suma = correctAnswers + mistakes;
        // Debug.Log("Suma correctas y errores" + suma);


        dialogueIsPlaying = false;
        // dialoguePanel.SetActive(false);
        // choicesPanel.SetActive(false);
        dialogueText.text = "";
        speakerName.text = "";

        DialogueManager.GetInstance().EndDialogue();
    }


    public static DialogueManagerPAP GetInstance()
    {
        return _instance;
    }
}
