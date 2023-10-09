using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
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

    public GameObject oneTry_stats;
    
    public GameObject allTry;

    
    
    private List<History> allHistory;
    private History[] arrayHistory;
    private string itemFilter;
    private string itemOrden;


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
            
            //History wea = RealmController.Instance.CreateHistory(4,2,5,3,2,12,3,5,8,2,"Lugar Público", "Muy mas o menos");
            
            
            arrayHistory = RealmController.Instance.GetHistory();        
            allHistory = arrayHistory.ToList();
            allHistory = allHistory.OrderByDescending(try_history => try_history.Date).ToList();
            //Debug.Log(allHistory.Count);
        }
        else
        {
            Debug.LogError("RealmController.Instance is null. Make sure it's properly initialized.");
        }
        
        
        
        ChangeHeightandPosition(NewHeight(allHistory.Count));
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
            
            //Text Score
            TextMeshProUGUI Score_A = prefabIntento.transform.Find("Pasos_Score_Group/Paso_A_text").GetComponent<TextMeshProUGUI>();
            TMP_Text Score_B = prefabIntento.transform.Find("Pasos_Score_Group/Paso_B_text").GetComponent<TMP_Text>();
            TMP_Text Score_C = prefabIntento.transform.Find("Pasos_Score_Group/Paso_C_text").GetComponent<TMP_Text>();
            TMP_Text Score_D = prefabIntento.transform.Find("Pasos_Score_Group/Paso_D_text").GetComponent<TMP_Text>();
            TMP_Text Score_E = prefabIntento.transform.Find("Pasos_Score_Group/Paso_E_text").GetComponent<TMP_Text>();
            

            Score_A.text="Paso A: "+item.ScoreA.ToString()+"/"+item.TotalA;
            Score_B.text="Paso B: "+item.ScoreB.ToString()+"/"+item.TotalB;
            Score_C.text="Paso C: "+item.ScoreC.ToString()+"/"+item.TotalC;
            Score_D.text="Paso D: "+item.ScoreD.ToString()+"/"+item.TotalD;
            Score_E.text="Paso E: "+item.ScoreE.ToString()+"/"+item.TotalE;

            //Imagen Scene

            Image Image_Scene = prefabIntento.transform.Find("Image_Scene").GetComponent<Image>();
            if (item.Scene=="Lugar Cerrado")
            {   
                Sprite spriteL_Cerrado = Resources.Load<Sprite>("Fotos/SceneFotos/Screenshot_5_trans");
                Image_Scene.sprite = spriteL_Cerrado;

                
            }
            
            if (item.Scene=="Lugar Público")
            {   
                Sprite spriteL_Cerrado = Resources.Load<Sprite>("Fotos/SceneFotos/Screenshot_4_trans");
                Image_Scene.sprite = spriteL_Cerrado;
                
            }
            //Imagen Porcentaje

            Image Image_Porcentaje = prefabIntento.transform.Find("Total_Score/Image_Porcentaje").GetComponent<Image>();
            TMP_Text Total_text = prefabIntento.transform.Find("Total_Score/Total_text").GetComponent<TMP_Text>();
            
            string porcentaje_text = GetPhoto(item);
            Sprite sprite_Porcentaje = Resources.Load<Sprite>("Fotos/Porcentaje Historial/"+porcentaje_text);
            Image_Porcentaje.sprite = sprite_Porcentaje;

            int correctas = item.ScoreA + item.ScoreB + item.ScoreC + item.ScoreD + item.ScoreE;
            int intentos = item.TotalA + item.TotalB + item.TotalC + item.TotalD + item.TotalE;
            int porcentaje = (correctas * 100) / intentos; 
            Total_text.text=porcentaje.ToString()+"%";

            //Text Fecha y Scene
            TMP_Text Fecha_text = prefabIntento.transform.Find("Fecha_Lugar_Group/Fecha_text").GetComponent<TMP_Text>();
            TMP_Text Lugar_text = prefabIntento.transform.Find("Fecha_Lugar_Group/Lugar_text").GetComponent<TMP_Text>();

            string fecha_format = item.Date.Day.ToString()+"/"+item.Date.Month.ToString()+"/"+item.Date.Year.ToString();
            
            Fecha_text.text = fecha_format;
            Lugar_text.text = item.Scene;

            //Poner Intento en el panel
            GameObject intento = Instantiate(prefabIntento);
            //CLick Stats Especificas:
            Button boton = intento.GetComponent<Button>();
            boton.onClick.AddListener(() => MostrarStats(item.Id));

            intento.transform.SetParent(groupTransform, false); // Establece el panel como hijo del "Group"


            //panel.GetComponent<TuPanelScript>().Configurar(dato);
        }
    }

    private void MostrarStats(string id)
    {   
        //Paneles activos
        oneTry_stats.SetActive(true);
        allTry.SetActive(false);

        History try_history = RealmController.Instance.HistoryById(id);
        
        //Image Escenario
        Image Image_Scene = oneTry_stats.transform.Find("Panel_Principal_One_Try/Image_Escenario").GetComponent<Image>();
        if (try_history.Scene=="Lugar Cerrado")
        {   
            Sprite spriteL_Cerrado = Resources.Load<Sprite>("Fotos/SceneFotos/Screenshot_5");
            Image_Scene.sprite = spriteL_Cerrado;
        
        }
            
        if (try_history.Scene=="Lugar Público")
        {   
            Sprite spriteL_Cerrado = Resources.Load<Sprite>("Fotos/SceneFotos/Screenshot_4");
            Image_Scene.sprite = spriteL_Cerrado;        
        }

        //Text de arriba

        TMP_Text escenario_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Escenario_text").GetComponent<TMP_Text>();
        TMP_Text date_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Fecha_try_text").GetComponent<TMP_Text>();
        TMP_Text time_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Hora_text").GetComponent<TMP_Text>();
        TMP_Text total_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Porcentaje_general_text").GetComponent<TMP_Text>();

        escenario_text.text = "Escenario: "+try_history.Scene;
        
        string date_format = "Fecha: "+try_history.Date.Day.ToString()+"/"+try_history.Date.Month.ToString()+"/"+try_history.Date.Year.ToString();
        date_text.text=date_format;
        
        time_text.text= "Hora: "+try_history.Date.ToString("HH:mm");


        int correctas = try_history.ScoreA + try_history.ScoreB + try_history.ScoreC + try_history.ScoreD + try_history.ScoreE;
        int intentos = try_history.TotalA + try_history.TotalB + try_history.TotalC + try_history.TotalD + try_history.TotalE;
        int porcentaje = (correctas * 100) / intentos;
        total_text.text = "Porcentaje General: "+porcentaje.ToString()+"%";
        
        //int correctas = item.ScoreA + item.ScoreB + item.ScoreC + item.ScoreD + item.ScoreE;
        //int intentos = item.TotalA + item.TotalB + item.TotalC + item.TotalD + item.TotalE;

        //Text Groups
        TMP_Text pasoA_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Trys_Group/Panel_PasoA/GroupA/PasoA_Try_text").GetComponent<TMP_Text>();
        TMP_Text pasoA_score_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Trys_Group/Panel_PasoA/GroupA/PasoA_Porcentaje_text").GetComponent<TMP_Text>();
        

        pasoA_text.text = "Paso-A: "+try_history.ScoreA+"/"+try_history.TotalA; 
        int porcentaje_a = (try_history.ScoreA*100) / try_history.TotalA;
        pasoA_score_text.text= porcentaje_a.ToString()+"%";

        TMP_Text pasoB_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Trys_Group/Panel_PasoB/GroupB/PasoB_Try_text").GetComponent<TMP_Text>();
        TMP_Text pasoB_score_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Trys_Group/Panel_PasoB/GroupB/PasoB_Porcentaje_text").GetComponent<TMP_Text>();

        pasoB_text.text = "Paso-B: "+try_history.ScoreB+"/"+try_history.TotalB; 
        int porcentaje_b = (try_history.ScoreB*100) / try_history.TotalB;
        pasoB_score_text.text= porcentaje_b.ToString()+"%";

        TMP_Text pasoC_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Trys_Group/Panel_PasoC/GroupC/PasoC_Try_text").GetComponent<TMP_Text>();
        TMP_Text pasoC_score_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Trys_Group/Panel_PasoC/GroupC/PasoC_Porcentaje_text").GetComponent<TMP_Text>();

        pasoC_text.text = "Paso-C: "+try_history.ScoreC+"/"+try_history.TotalC; 
        int porcentaje_c = (try_history.ScoreC*100) / try_history.TotalC;
        pasoC_score_text.text= porcentaje_c.ToString()+"%";

        TMP_Text pasoD_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Trys_Group/Panel_PasoD/GroupD/PasoD_Try_text").GetComponent<TMP_Text>();
        TMP_Text pasoD_score_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Trys_Group/Panel_PasoD/GroupD/PasoD_Porcentaje_text").GetComponent<TMP_Text>();

        pasoD_text.text = "Paso-D: "+try_history.ScoreD+"/"+try_history.TotalD; 
        int porcentaje_d = (try_history.ScoreD*100) / try_history.TotalD;
        pasoD_score_text.text= porcentaje_d.ToString()+"%";

        TMP_Text pasoE_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Trys_Group/Panel_PasoE/GroupE/PasoE_Try_text").GetComponent<TMP_Text>();
        TMP_Text pasoE_score_text = oneTry_stats.transform.Find("Panel_Principal_One_Try/Trys_Group/Panel_PasoE/GroupE/PasoE_Porcentaje_text").GetComponent<TMP_Text>();

        pasoE_text.text = "Paso-E: "+try_history.ScoreE+"/"+try_history.TotalE; 
        int porcentaje_e = (try_history.ScoreE*100) / try_history.TotalE;
        pasoE_score_text.text= porcentaje_e.ToString()+"%";

        //Text Abajo
        TMP_Text feedback = oneTry_stats.transform.Find("Panel_Principal_One_Try/Feedback_text").GetComponent<TMP_Text>();
        feedback.text = "<b>Retroalimentación:</b>\n"+try_history.Feedback;
        
    }
    void DropdownItemSelectedFilter( TMP_Dropdown dropdown)
    {
        int index = dropdown.value;

        filter=dropdown.options[index].text;
        itemFilter=filter;
        //Debug.Log("Filtro: "+ itemFilter);
    }


    void DropdownItemSelectedOrder( TMP_Dropdown dropdown)
    {
        int index = dropdown.value;

        order=dropdown.options[index].text;
        itemOrden=order;
        //Debug.Log("Orden: "+ itemOrden);
    }

    void Borrar()
    {
        //GameObject BotonesCreados = group;
        foreach (Transform hijo in group.transform)
        {
            Destroy(hijo.gameObject);
        }
    }

    public void ActualizarOrden()
    {   
        //DropdownItemSelectedFilter(dropdownFilter);
        //DropdownItemSelectedOrder(dropdownOrder);
        if (itemFilter=="Fecha" && itemOrden=="de mayor a menor")
        {
            Borrar();
            allHistory = allHistory.OrderByDescending(try_history => try_history.Date).ToList();
            BotonesenGroup();   
        }
        else if (itemFilter=="Fecha" && itemOrden=="de menor a mayor")
        {
            Borrar();
            allHistory = allHistory.OrderBy(try_history => try_history.Date).ToList();
            BotonesenGroup(); 
        }
        else if (itemFilter=="Escenario" && itemOrden=="de menor a mayor")
        {
            Borrar();
            allHistory = allHistory.OrderBy(try_history => try_history.Scene).ToList();
            BotonesenGroup(); 
        }
        else if (itemFilter=="Escenario" && itemOrden=="de mayor a menor")
        {
            Borrar();
            allHistory = allHistory.OrderByDescending(try_history => try_history.Scene).ToList();
            BotonesenGroup();   
        }
        else if (itemFilter=="Calificación" && itemOrden=="de mayor a menor")
        {
            Borrar();
            allHistory = allHistory.OrderByDescending(try_history => try_history.TotalScore).ToList();
            BotonesenGroup();   
        }
        else if (itemFilter=="Calificación" && itemOrden=="de menor a mayor")
        {
            Borrar();
            allHistory = allHistory.OrderBy(try_history => try_history.TotalScore).ToList();
            BotonesenGroup();   
        }
        
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
        //Debug.Log("pos: "+ posicionActual.y);
        // Aplica la nueva posición al RectTransform
        contentRectTransform.anchoredPosition = posicionActual;

    }

    public string GetPhoto(History item)
    {
        int correctas = item.ScoreA + item.ScoreB + item.ScoreC + item.ScoreD + item.ScoreE;
        int intentos = item.TotalA + item.TotalB + item.TotalC + item.TotalD + item.TotalE;
        int porcentaje = (correctas * 100) / intentos;
        if (porcentaje == 0)
        {
            return "0";
        }
        else if (0 < porcentaje && porcentaje < 15)
        {
            return "10";
        }
        else if (14 < porcentaje && porcentaje< 25)
        {
            return "20";
        }
        else if (24 < porcentaje && porcentaje< 35)
        {
            return "30";
        }
        else if (34 < porcentaje && porcentaje< 45)
        {
            return "40";
        }
        else if (44 < porcentaje && porcentaje< 55)
        {
            return "50";
        }
        else if (54 < porcentaje && porcentaje< 65)
        {
            return "60";
        }
        else if (64 < porcentaje && porcentaje< 75)
        {
            return "70";
        }
        else if (74 < porcentaje && porcentaje< 85)
        {
            return "80";
        }
        else if (84 < porcentaje && porcentaje< 100)
        {
            return "90";
        }
        else if (porcentaje == 100)
        {
            return "100";
        }
        else
        {
            return "";
        }
        
    }

    
    // Update is called once per frame
    
}
