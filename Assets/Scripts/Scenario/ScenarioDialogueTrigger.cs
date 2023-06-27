using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioDialogueTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJson;

    void Start()
    {
        Invoke("DialogueTrigger", 2f);
    }

    public void DialogueTrigger()
    {
        ScenarioDialogueManager.GetInstance().EnterDialogueMode(inkJson);
    }
}
