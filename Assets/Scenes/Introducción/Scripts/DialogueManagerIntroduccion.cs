using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class DialogueManagerIntroduccion : MonoBehaviour
{
    private static DialogueManagerIntroduccion _instance;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text speakerName;
    // [SerializeField] private TMP_Text finalScore;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject endButtons;
    [SerializeField] private GameObject[] choices;

    [SerializeField] private Button BotonSintomas;
    [SerializeField] private Button BotonPAP;
    [SerializeField] private Button BotonTecnica;
    [SerializeField] private Button BotonAyuda;
    private TMP_Text[] choicesText;

    private ColorBlock colorDestacado;
    colorDestacado.normalColor = new Color(253,239,127.255);

    private ColorBlock colorNormal;
    colorDestacado.normalColor = Color.white;



    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private const string SPEAKER_TAG = "title";

    private const string HIGHLIGHT = "highlight";

    private const string UNHIGHLIGHT = "unhighlight";
    

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

                case HIGHLIGHT_TAG:
                    
                    BotonSintomas.colorDestacado;

                    // TODO: HANDLE HIGHLIGHT


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


    public static DialogueManagerIntroduccion GetInstance()
    {
        return _instance;
    }
}
