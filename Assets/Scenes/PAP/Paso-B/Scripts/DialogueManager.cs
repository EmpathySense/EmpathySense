using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text narratorText;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting dialogue...");

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            this.sentences.Enqueue(sentence);
        }
    }

    public void DisplayNextSentence()
    {
        if (this.sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        narratorText.SetText(sentence);
    }

    private void EndDialogue()
    {
        Debug.Log("dialgogue Ended");
    }
}
