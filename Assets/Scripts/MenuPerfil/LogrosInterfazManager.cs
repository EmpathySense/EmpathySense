using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;


public class LogrosInterfazManager : MonoBehaviour
{   
    public Slider sliderLogros;
    public TMP_Text textTotalLogros;
    public TMP_Text textPercentageLogros;

    public GameObject prefabLogro;

    public GameObject contentLogrosDesbloqueados;

    public GameObject contentLogrosBloqueados;


    public class PrefLogros 
    {
        public bool InfoA;
        public bool InfoB;
        public bool InfoC;
        public bool InfoD;
        public bool InfoE;
        public bool InfoPAP;
        public bool InfoSIM1;
        public bool InfoSIM2;
    }
    //private float percentageLogros = totalLogrosDesbloqueados/8*100;
    private PrefLogros logros;
    // Start is called before the first frame update
    void Start()
    {   
        Prefs userPrefs = RealmController.Instance.GetPrefs();
        int totalLogrosDesbloqueados = RealmController.Instance.CountFalse();
        float percentageLogros = (float)totalLogrosDesbloqueados/10*100;
        Debug.Log("Total Logros Desbloqueados: "+ totalLogrosDesbloqueados);
        Debug.Log("Porcentaje Logros Desbloqueados: "+ (int)(percentageLogros+0.5));
        sliderLogros.value = totalLogrosDesbloqueados;
        textTotalLogros.text = totalLogrosDesbloqueados.ToString()+ " de 10 Logros Conseguidos";
        textPercentageLogros.text = "("+(int)(percentageLogros+0.5)+"%)";

        logros = createClassLogros(userPrefs);
        //Debug.Log("Logros: "+ logros.InfoA + logros.InfoB + logros.InfoC + logros.InfoD + logros.InfoE + logros.InfoPAP + logros.InfoSIM1 + logros.InfoSIM2);
        
    }

    PrefLogros createClassLogros(Prefs userpref)
    {
         PrefLogros userLogros = new PrefLogros();
        userLogros.InfoA = userpref.InfoB;
        userLogros.InfoB = userpref.InfoC;
        userLogros.InfoC = userpref.InfoD;
        userLogros.InfoD = userpref.InfoE;
        userLogros.InfoE = userpref.InfoSim;
        userLogros.InfoPAP = userpref.Pap;
        userLogros.InfoSIM1 = userpref.Sim_01;
        userLogros.InfoSIM2 = userpref.Sim_02;
        return userLogros;
    }
    // Update is called once per frame
    void LogrosDesbloqueados()
    {
        Transform groupTrasform = contentLogrosDesbloqueados.transform;
        


    }
}
