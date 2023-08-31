using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialoguePart2Trigger : MonoBehaviour
{

    [SerializeField] private TextAsset inkJson;
    [SerializeField] private RectTransform tpTarget;
    [SerializeField] private GameObject UICanvas;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger!");
        // Movemos la interfaz a la posición deseada
        UICanvas.transform.position = tpTarget.position;
        UICanvas.transform.rotation = tpTarget.rotation;

        Scenario2DialogueManager.GetInstance().EnterDialogueMode(inkJson);

    }
}
