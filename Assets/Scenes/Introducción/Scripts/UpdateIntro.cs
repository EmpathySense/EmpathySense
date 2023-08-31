using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class UpdateIntro : MonoBehaviour
{
    public Button Button;
    void Start()
    {
        Button.onClick.AddListener(UpdateIntroDB);
    }

    // Update is called once per frame
    void UpdateIntroDB()
    {
        RealmController.Instance.UpdateIntro();
    }
}
