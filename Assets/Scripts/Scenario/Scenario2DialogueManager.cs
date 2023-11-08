using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Ink.Runtime;
using System;
using UnityEngine.UI;
using System.Net.NetworkInformation;

public class Scenario2DialogueManager : MonoBehaviour
{
    public AudioSource audioSource;
    private static Scenario2DialogueManager _instance;

    [SerializeField] private Animator igancioAnimator;
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

    [SerializeField] private GameObject guia;
    [SerializeField] private TMP_Text guiaText;
    [SerializeField] private GameObject guiaContinueButton;
    private bool guiaIsTalking = true;

    private int correctAnswers;
    private int mistakes;
    private int scoreSectionA;
    private int scoreSectionB;
    private int scoreSectionC;
    private int scoreSectionD;
    private int scoreSectionE;

    private int intentoA;
    private int intentoB;
    private int intentoC;
    private int intentoD;
    private int intentoE;

    private Story currentStory;
    public bool dialogueIsPlaying { get; private set; }

    private const string SPEAKER_TAG = "title";
    private const string AUDIO_TAG = "audio";
    private const string ACHIEVEMENT_TAG = "achievement";
    private const string ANIMATION_TRIGGER_TAG = "animation";
    private const string END_DIALOGUE_TAG = "EndDialogue";
    private const string SWITCH_DIALOGUE_TAG = "dialog";

    private Queue<string> feedbackQueue = new Queue<string>();
    private Dictionary<string, string> feedback = new Dictionary<string, string>()
    {
        {"40a", "Parece que tuviste un pequeño desafío en el Paso A, ¡pero eso es completamente normal! Todos aprendemos a nuestro propio ritmo. Intenta de nuevo y ten en cuenta que cada intento es una oportunidad para mejorar. Puedes volver a revisar este paso desde el menú principal."},
        {"40b", "No te desanimes por el Paso B. A veces, las cosas pueden ser un poco complicadas al principio. Toma un respiro y vuelve a intentarlo cuando te sientas listo. ¡Estoy seguro de que puedes hacerlo! Puedes volver a revisar este paso desde el menú principal."},
        {"40c", "No te preocupes por el Paso C. A veces, las cosas pueden ponerse un poco difíciles, pero eso no significa que no puedas hacerlo. Tómate tu tiempo y sigue intentándolo. Puedes volver a revisar este paso desde el menú principal."},
        {"40d", "El Paso D puede ser un poco complicado, pero eso no te impide aprender. Tómatelo con calma, repasa las instrucciones y da otro intento cuando te sientas listo."},
        {"40e", "No te preocupes si el Paso E te resulta difícil en este momento. Todos enfrentamos obstáculos en nuestro camino. Tómatelo con calma, repasa y sigue practicando. ¡Estoy aquí para apoyarte en cada paso del camino! Puede volver a revisar este paso desde el menú principal."},

        {"4080a", "En el Paso A tu desempeño fue bueno, y sé que puedes hacerlo aún mejor. No te preocupes si necesitas repetirlo, la práctica te llevará a la excelencia."},
        {"4080b", "Tu desempeño en el Paso B fue bueno. Si sientes la necesidad de repasarlo, adelante. La práctica te llevará a la perfección, y estoy aquí para apoyarte."},
        {"4080c", "Tu desempeño en el Paso C fue bastante bueno. Si decides repasar o practicar más, estaré aquí para apoyarte en el camino."},
        {"4080d", "Tuviste un desempeño bueno en el Paso D. Si sientes que necesitas mejorar aún más, estoy seguro de que puedes hacerlo con un poco de práctica adicional."},
        {"4080e", "Tuviste un buen desempeño en el Paso E. Si sientes que necesitas repasarlo o practicar un poco más, no dudes en hacerlo. Estoy seguro de que mejorarás."},

        {"80a", "¡Maravilloso! Has superado el Paso A con éxito. Continúa avanzando con confianza y entusiasmo."},
        {"80b", "¡Fantástico trabajo en el Paso B! Has superado otro obstáculo. ¡Sigue así y verás cuánto progresas!"},
        {"80c", "¡Increíble! Has superado el Paso C sin problemas. ¡Estás demostrando una gran habilidad en esto!"},
        {"80d", "¡Excelente trabajo en el Paso D! Has superado este desafío con éxito. ¡Estás haciendo un progreso asombroso!"},
        {"80e", "¡Bravo! Dominaste el Paso E con gran habilidad. Tu perseverancia está dando sus frutos. ¡Sigue adelante con confianza!"},
    };
    private bool feedbackMode = false;

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

        if (inkJson.name != "Dialogo2_parte1")
        {
            ObserveStoryVariables();
            dialoguePanel.SetActive(true);
        }

        dialogueIsPlaying = true;

        

        ContinueDialogueMode();
    }

    private void ObserveStoryVariables()
    {
        currentStory.ObserveVariable("scoreSectionA", (variableName, newValue) => {
            scoreSectionA = (int)newValue;
            Debug.Log("Score Section A: " + scoreSectionA);
        });
        currentStory.ObserveVariable("scoreSectionB", (variableName, newValue) => {
            scoreSectionB = (int)newValue;
            Debug.Log("Score SectionB: " + scoreSectionB);
        });
        currentStory.ObserveVariable("scoreSectionC", (variableName, newValue) => {
            scoreSectionC = (int)newValue;
            Debug.Log("Score SectionC: " + scoreSectionC);
        });
        currentStory.ObserveVariable("scoreSectionD", (variableName, newValue) => {
            scoreSectionD = (int)newValue;
            Debug.Log("Score SectionD: " + scoreSectionD);
        });
        currentStory.ObserveVariable("scoreSectionE", (variableName, newValue) => {
            scoreSectionE = (int)newValue;
            Debug.Log("Score SectionE: " + scoreSectionE);
        });

        currentStory.ObserveVariable("intentoA", (variableName, newValue) => {
            intentoA = (int)newValue;
            Debug.Log("Intento A: " + intentoA);
        });
        currentStory.ObserveVariable("intentoB", (variableName, newValue) => {
            intentoB = (int)newValue;
            Debug.Log("Intento B: " + intentoB);
        });
        currentStory.ObserveVariable("intentoC", (variableName, newValue) => {
            intentoC = (int)newValue;
            Debug.Log("Intento C: " + intentoC);
        });
        currentStory.ObserveVariable("intentoD", (variableName, newValue) => {
            intentoD = (int)newValue;
            Debug.Log("Intento D: " + intentoD);
        });
        currentStory.ObserveVariable("intentoE", (variableName, newValue) => {
            intentoE = (int)newValue;
            Debug.Log("Intento A: " + intentoE);
        });
        
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
                dialoguePanel.SetActive(true);
                dialogueText.text = currentStory.Continue();
            }
            
            StopCoroutine("IdleReminder");
            StartCoroutine("IdleReminder");

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

                case ACHIEVEMENT_TAG:

                    StartCoroutine(ShowAlertCanvas(logroPanel2));
                    RealmController.Instance.UpdateLogros("SimB-");

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

                case AUDIO_TAG:
                    // AudioManager.Instance.Play(tagValue);
                    AudioClip clip = Resources.Load<AudioClip>("AudioFeedback/AudioScene2/" + tagValue);
                    //reproducir audio clip
                    audioSource.PlayOneShot(clip);
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

        StopCoroutine("IdleReminder");

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(true);
        choicesPanel.SetActive(false);
        dialogueText.text = "";
        speakerName.text = "";

        EndScreen();
    }

    private void EndScreen()
    {
        int totalCorrectas = scoreSectionA + scoreSectionB + scoreSectionC + scoreSectionD + scoreSectionE;
        int totalIntentos = intentoA + intentoB + intentoC + intentoD + intentoE;
        int porcentaje = (totalCorrectas * 100) / totalIntentos;

        feedbackQueue.Enqueue(GetFeedbackString(scoreSectionA, intentoA, "a"));
        feedbackQueue.Enqueue(GetFeedbackString(scoreSectionB, intentoB, "b"));
        feedbackQueue.Enqueue(GetFeedbackString(scoreSectionC, intentoC, "c"));
        feedbackQueue.Enqueue(GetFeedbackString(scoreSectionD, intentoD, "d"));
        feedbackQueue.Enqueue(GetFeedbackString(scoreSectionE, intentoE, "e"));

        dialoguePanel.SetActive(false);
        guia.SetActive(true);
        //guiaContinueButton.SetActive(false);

        if (porcentaje == 100)
        {
            StartCoroutine(ShowAlertCanvas(logroPanel));
            RealmController.Instance.UpdateLogros("SimB-100-");
        }

        guiaText.text = "Felicidades, has completado la simulación. A continuación se mostrarán tus resultados";

        feedbackMode = true;

        RealmController.Instance.CreateHistory(scoreSectionA, scoreSectionB, scoreSectionC, scoreSectionD, scoreSectionE, intentoA, intentoB, intentoC, intentoD, intentoE, porcentaje, "Lugar Cerrado", GeneralFeedback());

    }

    private string GeneralFeedback()
    {
        int totalCorrectas = scoreSectionA + scoreSectionB + scoreSectionC + scoreSectionD + scoreSectionE;
        int totalIntentos = intentoA + intentoB + intentoC + intentoD + intentoE;
        int porcentaje = (totalCorrectas * 100) / totalIntentos;


        if(porcentaje<=40)
        {
            return "¡No te desanimes! Las crisis de pánico son un tema complejo, pero esta es una oportunidad para aprender y crecer. Aprovecha esta instancia para identificar las áreas en las que necesitas trabajar más.";
        }
        else if (porcentaje <= 80)  
        {
            return "¡Estás en el camino correcto! Tu puntuación muestra que tienes una comprensión sólida del tema, pero siempre hay espacio para mejorar. Continúa estudiando y profundizando en los conceptos clave.";
        }
        else 
        {
            return "¡Felicidades! Tener una calificación tan alta muestra que ya tienes un dominio sobre los primeros auxilios psicológicos, tu dedicación y habilidades son clave para marcar una diferencia significativa en la vida de quienes enfrentan estas situaciones difíciles. ¡Sigue así!";
        }
    }

    public void ContinueFeedbackMode()
    {
        if (feedbackMode)
        {
            Debug.Log("BOTON FEEEDBACK");
            if (feedbackQueue.Count > 0)
            {
                guiaText.text = feedbackQueue.Dequeue();
            }
            else
            {
                endButtons.SetActive(true);
                continueButton.SetActive(false);
                guiaContinueButton.SetActive(false);
            }
        }
            
    }

    private string GetFeedbackString(int scoreSection, int intentos, string paso)
    {
        int percentageScore = (scoreSection / intentos) * 100;

        if (percentageScore <= 40)
        {
            if(paso == "a"){
                return feedback["40a"];
            }
            else if(paso == "b"){
                return feedback["40b"];
            }
            else if(paso == "c"){
                return feedback["40c"];
            }
            else if(paso == "d"){
                return feedback["40d"];
            }
            else if(paso == "e"){
                return feedback["40e"];
            }
        }
        else if (percentageScore <= 80)
        {
            if(paso == "a"){
                return feedback["4080a"];
            }
            else if(paso == "b"){
                return feedback["4080b"];
            }
            else if(paso == "c"){
                return feedback["4080c"];
            }
            else if(paso == "d"){
                return feedback["4080d"];
            }
            else if(paso == "e"){
                return feedback["4080e"];
            }
        }
        else
        {
            if(paso == "a"){
                return feedback["80a"];
            }
            else if(paso == "b"){
                return feedback["80b"];
            }
            else if(paso == "c"){
                return feedback["80c"];
            }
            else if(paso == "d"){
                return feedback["80d"];
            }
            else if(paso == "e"){
                return feedback["80e"];
            }
        }

        return "";
    }

    IEnumerator ShowAlertCanvas(GameObject canvas)
    {
        canvas.SetActive(true); // Activa el canvas

        yield return new WaitForSeconds(5f); // Espera durante 2 segundos

        canvas.SetActive(false); // Desactiva el canvas
    }

    private IEnumerator IdleReminder()
    {
        yield return new WaitForSeconds(60.0f);
        Debug.Log("Despierta woms");

        string tmpText = guiaText.text;
        bool shouldDeactivateGuide = true;
        if (guia.activeInHierarchy)
        {
            shouldDeactivateGuide = false;
        }
        guia.SetActive(true);
        guiaContinueButton.SetActive(false);
        guiaText.text = "¿Estas ahí? Recuerda que estás tratnado de ayudar a una persona en crisis de pánico";

        yield return new WaitForSeconds(5.0f);

        guiaContinueButton.SetActive(true);

        if (shouldDeactivateGuide)
        {
            guia.SetActive(false);
        }
        guiaText.text = tmpText;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenúPrincipal");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static Scenario2DialogueManager GetInstance()
    {
        return _instance;
    }
}
