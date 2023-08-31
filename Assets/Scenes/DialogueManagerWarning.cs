using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.UI;

public class DialogueManagerWarning : MonoBehaviour
{
    private static DialogueManagerWarning _instance;

    [SerializeField] private GameObject dialoguePanel;
    
    [SerializeField] private TMP_Text dialogueText;
    

    // [SerializeField] private TMP_Text finalScore;
    [SerializeField] private GameObject continueButton;
    
    [SerializeField] private GameObject finButton;
    
    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private const string SPEAKER_TAG = "speaker";
    
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
           

            HandleTags(currentStory.currentTags);
        }
        
        
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

                    dialoguePanel.SetActive(false);

                    break;


                default:
                    Debug.LogWarning("No se esta manejando el tag: " + tagValue);
                    break;
            }
        }
    }

    public static DialogueManagerWarning GetInstance()
    {
        return _instance;
    }


    
}
