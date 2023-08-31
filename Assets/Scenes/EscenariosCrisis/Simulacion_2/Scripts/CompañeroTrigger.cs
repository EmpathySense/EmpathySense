using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CompañeroTrigger : MonoBehaviour

{
    public Animator characterAnimator; // Arrastra el componente Animator del personaje aquí
    public Button TriggerButton; // Arrastra el componente Button del botón aquí

    public void Start()
    {
        // Asigna el método ChangeAnimationTrigger al evento OnClick del botón
        TriggerButton.onClick.AddListener(ChangeAnimationTrigger);
    }


    public void ChangeAnimationTrigger()
    {
        // Activa el trigger para cambiar la animación
        characterAnimator.SetTrigger("QuietTrigger");
    }
}
