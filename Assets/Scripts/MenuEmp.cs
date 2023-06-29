using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuEmp : MonoBehaviour
{
    [System.Serializable]
    public class Sintoma
    {
        public string text;
        public string title;
    }

    public TMP_Text explicationText;
    public TMP_Text titleText;
    public Sintoma[] sintomas;
    private int currentSubtitleIndex = 0;
    public GameObject continueButton;
    public GameObject menuButton;
    private string NewText = "Menú Principal";

    public GameObject panelAntes;
    public GameObject panelActual;
    // Start is called before the first frame update
    void Start()
    {
        currentSubtitleIndex = 0;
        ShowSintoma();
    }

    // Update is called once per frame
    private void ShowSintoma()
    {
        Sintoma currentSubtitle = sintomas[currentSubtitleIndex];
        UpdateSubtitleText(currentSubtitle);
        
    }
    private void UpdateSubtitleText(Sintoma sintoma)
    {
        titleText.text = sintoma.title;
        explicationText.text = sintoma.text;
    }

    public void GoToNextSintoma()
    {
        currentSubtitleIndex++;
        
        if (currentSubtitleIndex == sintomas.Length-1)
        {
            ShowSintoma();
            continueButton.SetActive(false);
            menuButton.SetActive(true);
            
        }
        else
        {
            
            ShowSintoma();
        }
        
    }

    public void GoToBackSintoma()
    {
        
        currentSubtitleIndex--;
        Debug.Log("INDEX: " + currentSubtitleIndex);
        Debug.Log("LARGO: " + sintomas.Length);
        if (currentSubtitleIndex == 6)
        {
            ShowSintoma();
            continueButton.SetActive(true);
            menuButton.SetActive(false);

        }
        else if (currentSubtitleIndex < 0)
        {
            currentSubtitleIndex = 0;
            volver1();
        }
        else
        {

            ShowSintoma();
        }

        
    }

    private void volver1()
    {
        panelActual.SetActive(false);
        panelAntes.SetActive(true);

    }
    public void Inicial()
    {
        currentSubtitleIndex = 0;
        ShowSintoma();
        continueButton.SetActive(true);
        menuButton.SetActive(false);
    }

    public void ResetIndex()
    {
        currentSubtitleIndex = 0;
    }

    public void Change(string name)
    {
        string sceneName = name;
        SceneManager.LoadScene(name);
    }
    
}
