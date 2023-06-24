using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace PasoBDialogue
{
    public class TextExplication : MonoBehaviour
    {
        [System.Serializable]
        public class Subtitle
        {
            public string text;
            public float displayTime;
        }

        public GameObject subtitlePanel;
        public GameObject canvas;
        public TextMeshProUGUI subtitleText;
        public Subtitle[] subtitles;
        public Button nextSubtitleButton;
        public float timeToShowButton = 3f; // Tiempo en segundos para mostrar el bot�n despu�s del subt�tulo
        public float timeToStartSubtitles = 3f;
        private int currentSubtitleIndex = 0;

        private void Start()
        {
            // nextSubtitleButton.gameObject.SetActive(false);
            // subtitlePanel.SetActive(false);
            Invoke("ShowSubtitlePanel", timeToStartSubtitles);
            
        }

        private void ShowSubtitlePanel()
        {
            subtitlePanel.SetActive(true);
            ShowNextSubtitle();
        }
        IEnumerator ShowUnableToOpenCanvas(GameObject canvas)
        {
            canvas.SetActive(true); // Activa el canvas

            yield return new WaitForSeconds(2f); // Espera durante 2 segundos

            canvas.SetActive(false); // Desactiva el canvas
        }
        private void ShowNextSubtitle()
        {
            if (currentSubtitleIndex >= subtitles.Length)
            {
                StartCoroutine(ShowUnableToOpenCanvas(canvas));
            }
            

            Subtitle currentSubtitle = subtitles[currentSubtitleIndex];
            UpdateSubtitleText(currentSubtitle.text);

            float subtitleDisplayTime = currentSubtitle.displayTime;
            float buttonDisplayTime = 2f; // Duraci�n de visualizaci�n del bot�n en segundos

            float hideButtonTime = subtitleDisplayTime + buttonDisplayTime;
            Invoke("HideSubtitleButton", hideButtonTime);
            Invoke("ShowNextSubtitleButton", subtitleDisplayTime);

            float nextSubtitleTime = subtitleDisplayTime + buttonDisplayTime;
            Invoke("GoToNextSubtitle", nextSubtitleTime);
        }

        private void ShowNextSubtitleButton()
        {
            nextSubtitleButton.gameObject.SetActive(true);
        }

        public void OnNextSubtitleButtonClicked()
        {
            GoToNextSubtitle();
            HideSubtitleButton();
        }

        private void GoToNextSubtitle()
        {
            currentSubtitleIndex++;
            ShowNextSubtitle();
        }

        private void UpdateSubtitleText(string subtitle)
        {
            subtitleText.text = subtitle;
        }

        private void HideSubtitleButton()
        {
            nextSubtitleButton.gameObject.SetActive(false);
        }
    }
}