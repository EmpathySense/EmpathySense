using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerWarning : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextAsset inkJson;

    void Start()
    {
        Invoke("DialogueTrigger", 2f);
    }

    public void DialogueTrigger()
    {
        DialogueManagerWarning.GetInstance().EnterDialogueMode(inkJson);
    }

    public void Restart()
    {
        DialogueManagerWarning.GetInstance().EnterDialogueMode(inkJson);
    }
}
