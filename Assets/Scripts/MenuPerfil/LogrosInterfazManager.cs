using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Globalization;



public class LogrosInterfazManager : MonoBehaviour
{   
    public Slider sliderLogros;

    public GameObject fillSlider;
    public TMP_Text textTotalLogros;
    public TMP_Text textPercentageLogros;

    public GameObject prefabLogro;

    public GameObject contentLogrosDesbloqueados;

    public GameObject contentLogrosBloqueados;

    private Achievements[] achievements;

    public GameObject containerLogros;

    public GameObject containerLogrosBloqueados;
    public GameObject containerLogrosDesbloqueados;

    
    //private float percentageLogros = totalLogrosDesbloqueados/8*100;

    // Start is called before the first frame update
     void Start()
     {   
        Prefs userPrefs = RealmController.Instance.GetPrefs();
         
        GridLayoutGroup gridLayoutGroup = containerLogros.GetComponent<GridLayoutGroup>();
        int totalLogrosDesbloqueados = RealmController.Instance.CountFalse();
        float percentageLogros = (float)totalLogrosDesbloqueados/10*100;
        if (totalLogrosDesbloqueados==0)
        {
            fillSlider.SetActive(false);
            containerLogrosDesbloqueados.SetActive(false);   
            gridLayoutGroup.padding.top = -7;
            //ChangeHeightandPosition(340);
        }
        else if(totalLogrosDesbloqueados==10)
        {
            containerLogrosBloqueados.SetActive(false);
            //ChangeHeightandPosition(340);
        }
        else
        {   
            Vector2 newCellSize = new Vector2(100f,(float) (21.2*totalLogrosDesbloqueados-3));
            gridLayoutGroup.cellSize = newCellSize;
            ChangeHeightandPosition(340+(10-totalLogrosDesbloqueados));
            GridLayoutGroup gridLayoutGroupBlo = containerLogrosBloqueados.transform.Find("GroupLogrosBloqueados").GetComponent<GridLayoutGroup>();
            gridLayoutGroupBlo.padding.top = -49+10*totalLogrosDesbloqueados;
        }
        
        Debug.Log("Total Logros Desbloqueados: "+ totalLogrosDesbloqueados);
        Debug.Log("Porcentaje Logros Desbloqueados: "+ (int)(percentageLogros+0.5));
        sliderLogros.value = totalLogrosDesbloqueados;
        textTotalLogros.text = totalLogrosDesbloqueados.ToString()+ " de 10 Logros Conseguidos";
        textPercentageLogros.text = "("+(int)(percentageLogros+0.5)+"%)";
        achievements = RealmController.Instance.GetAchievements();
        
        
        LogrosDesbloqueados();
    }

    void LogrosDesbloqueados()
    {
        Transform groupTrasformDes = contentLogrosDesbloqueados.transform;
        Transform groupTrasformBlo = contentLogrosBloqueados.transform;
        foreach (var item in achievements)
        {   
            //Text Name y Description
            TMP_Text textName = prefabLogro.transform.Find("Text_Description_Group/Name_text").GetComponent<TMP_Text>();
            TMP_Text textDescription = prefabLogro.transform.Find("Text_Description_Group/Description_text").GetComponent<TMP_Text>();

            textName.text = item.Name;
            Debug.Log("Nombre: "+item.Name);
            textDescription.text = item.Description;

            //Image y Date
            Image imageLogro = prefabLogro.transform.Find("Image_Logro").GetComponent<Image>();
            TMP_Text textDate = prefabLogro.transform.Find("Fecha_text").GetComponent<TMP_Text>();
            Sprite spriteImage = Resources.Load<Sprite>("Fotos/Logros/"+item.Id+item.State);
            imageLogro.sprite = spriteImage;

            if(item.State == true)
            {   
                string day=item.Date.Day.ToString();
                //string month=item.Date.Month.ToString("MMMM");
                string year=item.Date.Year.ToString();
                CultureInfo cultura = CultureInfo.CurrentCulture; // Obtiene la cultura actual para el formato de fechas
                string nombreMes = cultura.DateTimeFormat.GetMonthName(item.Date.Month);
                Debug.Log("Nombre Mes: "+nombreMes);
                textDate.text = "Se desbloqueó el "+day+" de "+nombreMes+" de "+year;
                GameObject logro = Instantiate(prefabLogro);
                logro.transform.SetParent(groupTrasformDes,false);
            }
            else
            {   
                textDate.text = "";
                GameObject logro = Instantiate(prefabLogro);
                logro.transform.SetParent(groupTrasformBlo,false);
            }
        }  
    }
float NewHeight (float len_history)
    {
        return 32*len_history+2;
    } 

    void ChangeHeightandPosition (float Height)
    {
        RectTransform contentRectTransform = containerLogros.GetComponent<RectTransform>();
        contentRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Height);
        
        Vector3 posicionActual = contentRectTransform.anchoredPosition;

        // Cambia la posición en el eje Y
        posicionActual.y -= Height/2.0f;
        //Debug.Log("pos: "+ posicionActual.y);
        // Aplica la nueva posición al RectTransform
        contentRectTransform.anchoredPosition = posicionActual;

    }
}
