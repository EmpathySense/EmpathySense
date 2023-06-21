using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    
    public Dialogue dialogue;

    void Start()
    {
        TriggerDialogue();
    }

    public void TestFunction()
    {
        Debug.Log("El boton ha sido presionado");
    }

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

}
