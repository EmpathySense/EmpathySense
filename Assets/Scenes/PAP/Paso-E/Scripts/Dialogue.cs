using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DialogoPasoB{
[System.Serializable]
public class Dialogue
{
    public string characterName;
    

    public Sentences[] sentences;
    
    // public string[] sentences;

    // public string animationTrigger;

    [System.Serializable]
    public class Sentences{

        [TextArea(3, 5)]
        public string text;
        public bool triggersAnimation;
        public string triggerName;
    }
}}
