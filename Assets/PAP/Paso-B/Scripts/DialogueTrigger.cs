using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PasoADialogue
{
    public class DialogueTrigger : MonoBehaviour
    {

        public Dialogue dialogue;

        void Start()
        {
            TriggerDialogue();
        }


        public void TriggerDialogue ()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            
        }

    }
}