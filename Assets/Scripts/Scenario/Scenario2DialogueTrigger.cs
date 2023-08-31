using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenario2DialogueManager1 : MonoBehaviour
{
    [SerializeField] private TextAsset inkJson;

    void Start()
    {
        Invoke("DialogueTrigger", 2f);
    }

    public void DialogueTrigger()
    {
        Scenario2DialogueManager.GetInstance().EnterDialogueMode(inkJson);
    }
}
