using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscenarioConFeedbackTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset inkJson;

    void Start()
    {
        Invoke("DialogueTrigger", 2f);
    }

    public void DialogueTrigger()
    {
        EscenarioConFeedbackManager.GetInstance().EnterDialogueMode(inkJson);
    }

}
