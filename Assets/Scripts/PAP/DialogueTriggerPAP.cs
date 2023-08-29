using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerPAP : MonoBehaviour
{
    [SerializeField] private TextAsset inkJson;

    void Start()
    {
        Invoke("DialogueTrigger", 2f);
    }

    public void DialogueTrigger()
    {
        DialogueManagerPAP.GetInstance().EnterDialogueMode(inkJson);
    }
}
