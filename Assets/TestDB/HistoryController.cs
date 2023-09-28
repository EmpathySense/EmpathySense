using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class HistoryController : MonoBehaviour
{
    // TMP Button
    public Button Button;
    public Button SecondButton;
    public TMP_InputField a;
    public TMP_InputField b;
    public TMP_InputField c;
    public TMP_InputField d;
    public TMP_InputField e;

    void Start()
    {
        /* Button.onClick.AddListener(AddHistory); */
        SecondButton.onClick.AddListener(GetAll);
    }

    /* void AddHistory()
    {
        RealmController.Instance.CreateHistory( int.Parse(a.text), int.Parse(b.text), int.Parse(c.text), int.Parse(d.text), int.Parse(e.text));
    } */

    void GetAll()
    {
        History[] _all = RealmController.Instance.GetHistory();
    }
    

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    
}
