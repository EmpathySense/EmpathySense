using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DialogoPasoD
{

    public class DialogueManager : MonoBehaviour
    {
        public TMP_Text narratorText;
        public Animator businessManAnimator;
        public Animator businessWomanAnimator;


        public GameObject alertCanvas;
        public GameObject modalPanel;

        [SerializeField]
        private GameObject continueButton, nextStepButton, returnButton;

        [SerializeField, TextArea(3, 5)]
        private string endOfStepText;
        private Queue<Dialogue.Sentences> sentences;


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
            StartCoroutine(ShowUnableToOpenCanvas(alertCanvas));

            UnlockNewLevel();

            // Update Modal
            narratorText.SetText(endOfStepText);
            returnButton.SetActive(true);
            continueButton.SetActive(false);
            nextStepButton.SetActive(true);
        }

        public void GoToMainMenu()
        {
            string levelName = "MenúPrincipal";
            SceneManager.LoadScene(levelName);
        }

        public void GoToNextScene(string Level)
        {
            string levelName = "Paso-" + Level;
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
}
