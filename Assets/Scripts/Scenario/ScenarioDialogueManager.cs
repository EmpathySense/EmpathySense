using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class ScenarioDialogueManager : MonoBehaviour
{
    private static ScenarioDialogueManager _instance;

    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text finalScore;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject[] choices;
    private TMP_Text[] choicesText;

    private int correctAnswers;
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }


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
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);

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
        currentStory.ObserveVariable("correctAnswers", (variableName, newValue) => {
            correctAnswers = (int)newValue;
        });
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
        choicesPanel.SetActive(true);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
        continueButton.SetActive(true);
        choicesPanel.SetActive(false);
        ContinueDialogueMode();
    }

    private void ExitDialogueMode()
    {
        currentStory.RemoveVariableObserver(null, "correctAnswers");
        finalScore.text = correctAnswers.ToString();

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);
        dialogueText.text = "";
    }

    public static ScenarioDialogueManager GetInstance()
    {
        return _instance;
    }
}
