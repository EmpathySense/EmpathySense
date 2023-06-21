using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text narratorText;
    public Animator businessWomanAnimator;

    private Queue<Dialogue.Sentences> sentences;

    // Start is called before the first frame update
    void Awake()
    {
        sentences = new Queue<Dialogue.Sentences>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting dialogue...");

        sentences.Clear();

        foreach (Dialogue.Sentences sentence in dialogue.sentences)
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

        Dialogue.Sentences sentence = sentences.Dequeue();

        narratorText.SetText(sentence.text);

        if (sentence.triggersAnimation) 
        {
            businessWomanAnimator.StopPlayback();
            businessWomanAnimator.SetTrigger(sentence.triggerName);
            
        }
    }

    private void EndDialogue()
    {
        Debug.Log("dialgogue Ended");
    }
}
