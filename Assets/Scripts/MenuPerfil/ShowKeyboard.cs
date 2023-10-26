using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;

public class ShowKeyboard : MonoBehaviour
{
   public TMP_InputField inputField;
   public float positionY;
   public float positionX;
   
   void Start()
   {
      inputField = GetComponent<TMP_InputField>();
      inputField.onSelect.AddListener(x=>OpenKeyboard());
   }

   public void OpenKeyboard()
   {
      NonNativeKeyboard.Instance.InputField = inputField;

      NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
      GameObject nonNativeKeyboard = GameObject.Find("NonNativeKeyboard");
      Transform keyboardTransform = nonNativeKeyboard.transform;
      Vector3 newPosition = keyboardTransform.position;
        newPosition.y = positionY;
        newPosition.x = positionX;
        keyboardTransform.position = newPosition;

   }
}
