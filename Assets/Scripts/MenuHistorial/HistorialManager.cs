using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using UnityEngine.SceneManagement;

using System;



public class HistorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    private string filter;
    private string order;

    public TMP_Dropdown dropdownFilter;
    public TMP_Dropdown dropdownOrder;

    public GameObject group;

    public GameObject prefabIntento;

    
    
    private History[] allHistory;


    private Dictionary<string, string> Total_Lugar_Cerrado = new Dictionary<string, string>
    {   
        {"A", "6"},
        {"B", "8"},
        {"C", "5"},
        {"D", "3"},
        {"E", "2"},
        {"Total", "24"}
    };

    private Dictionary<string, string> Total_Lugar_Public = new Dictionary<string, string>
    {
        {"A", "3"},
        {"B", "7"},
        {"C", "3"},
        {"D", "2"},
        {"E", "2"},
        {"Total", "17"}
    };
    void Start()
    {   
        
        if (RealmController.Instance != null)
        {
            allHistory = RealmController.Instance.GetHistory();
        }
        else
        {
            Debug.LogError("RealmController.Instance is null. Make sure it's properly initialized.");
        }
        
        
        
        ChangeHeightandPosition(NewHeight(allHistory.Length));
        DropdownItemSelectedFilter(dropdownFilter);
        DropdownItemSelectedOrder(dropdownOrder);
        dropdownFilter.onValueChanged.AddListener( delegate { DropdownItemSelectedFilter(dropdownFilter); });
        dropdownOrder.onValueChanged.AddListener( delegate { DropdownItemSelectedOrder(dropdownOrder); });
        BotonesenGroup();
    }

    void BotonesenGroup()
    {
        Transform groupTransform = group.transform;
        foreach (var item in allHistory)
        {   
            
            //Text Score e Imagen Scene
            TextMeshProUGUI Score_A = prefabIntento.transform.Find("Pasos_Score_Group/Paso_A_text").GetComponent<TextMeshProUGUI>();
            TMP_Text Score_B = prefabIntento.transform.Find("Pasos_Score_Group/Paso_B_text").GetComponent<TMP_Text>();
            TMP_Text Score_C = prefabIntento.transform.Find("Pasos_Score_Group/Paso_C_text").GetComponent<TMP_Text>();
            TMP_Text Score_D = prefabIntento.transform.Find("Pasos_Score_Group/Paso_D_text").GetComponent<TMP_Text>();
            TMP_Text Score_E = prefabIntento.transform.Find("Pasos_Score_Group/Paso_E_text").GetComponent<TMP_Text>();
            Image Image_Scene = prefabIntento.transform.Find("Image_Scene").GetComponent<Image>();


            if (item.Scene=="Lugar Cerrado")
            {   
                Sprite spriteL_Cerrado = Resources.Load<Sprite>("Fotos/SceneFotos/Screenshot_5_trans");
                Image_Scene.sprite = spriteL_Cerrado;

                Score_A.text="Paso A: "+item.ScoreA.ToString()+"/"+Total_Lugar_Cerrado["A"];
                Score_B.text="Paso B: "+item.ScoreB.ToString()+"/"+Total_Lugar_Cerrado["B"];
                Score_C.text="Paso C: "+item.ScoreC.ToString()+"/"+Total_Lugar_Cerrado["C"];
                Score_D.text="Paso D: "+item.ScoreD.ToString()+"/"+Total_Lugar_Cerrado["D"];
                Score_E.text="Paso E: "+item.ScoreE.ToString()+"/"+Total_Lugar_Cerrado["E"];
            }
            
            if (item.Scene=="Lugar Público")
            {   
                Sprite spriteL_Cerrado = Resources.Load<Sprite>("Fotos/SceneFotos/Screenshot_4_trans");
                Image_Scene.sprite = spriteL_Cerrado;
                Score_A.text="Paso A: "+item.ScoreA.ToString()+"/"+Total_Lugar_Public["A"];
                Score_B.text="Paso B: "+item.ScoreB.ToString()+"/"+Total_Lugar_Public["B"];
                Score_C.text="Paso C: "+item.ScoreC.ToString()+"/"+Total_Lugar_Public["C"];
                Score_D.text="Paso D: "+item.ScoreD.ToString()+"/"+Total_Lugar_Public["D"];
                Score_E.text="Paso E: "+item.ScoreE.ToString()+"/"+Total_Lugar_Public["E"];
            }

            //Text Fecha y Scene
            TMP_Text Fecha_text = prefabIntento.transform.Find("Fecha_Lugar_Group/Fecha_text").GetComponent<TMP_Text>();
            TMP_Text Lugar_text = prefabIntento.transform.Find("Fecha_Lugar_Group/Lugar_text").GetComponent<TMP_Text>();

            string fecha_format = item.Date.Day.ToString()+"/"+item.Date.Month.ToString()+"/"+item.Date.Year.ToString();
            
            Fecha_text.text = fecha_format;
            Lugar_text.text = item.Scene;
            

            //Poner Intento en el panel
            GameObject intento = Instantiate(prefabIntento);
            intento.transform.SetParent(groupTransform, false); // Establece el panel como hijo del "Group"


            //panel.GetComponent<TuPanelScript>().Configurar(dato);
        }
    }
    void DropdownItemSelectedFilter( TMP_Dropdown dropdown)
    {
        int index = dropdown.value;

        filter=dropdown.options[index].text;
        Debug.Log("Filtro: "+ filter);
    }


    void DropdownItemSelectedOrder( TMP_Dropdown dropdown)
    {
        int index = dropdown.value;

        order=dropdown.options[index].text;
        Debug.Log("Orden: "+ order);
    }

    float NewHeight (float len_history)
    {
        return 32*len_history+2;
    } 

    void ChangeHeightandPosition (float Height)
    {
        RectTransform contentRectTransform = group.GetComponent<RectTransform>();
        contentRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Height);
        
        Vector3 posicionActual = contentRectTransform.anchoredPosition;

        // Cambia la posición en el eje Y
        posicionActual.y -= Height/2.0f;
        Debug.Log("pos: "+ posicionActual.y);
        // Aplica la nueva posición al RectTransform
        contentRectTransform.anchoredPosition = posicionActual;

    }
    // Update is called once per frame
    
}
