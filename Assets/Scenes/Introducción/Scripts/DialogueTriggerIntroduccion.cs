using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerIntroduccion : MonoBehaviour
{
    [SerializeField] private TextAsset inkJson;

    void Start()
    {
        Invoke("DialogueTrigger", 2f);
    }

    public void DialogueTrigger()
    {
        DialogueManagerIntroduccion.GetInstance().EnterDialogueMode(inkJson);
    }
}
