using System.Collections;
using System.Collections.Generic;
using UnityEngine;





[System.Serializable]
public class Dialogue
{
    

    public Sentences[] sentences;
    
    // public string[] sentences;

    // public string animationTrigger;

    [System.Serializable]
    public class Sentences{

        public string characterName;
        [TextArea(3, 5)]
        public string text;
        public bool triggersAnimation;
        public string triggerName;
    }
}

