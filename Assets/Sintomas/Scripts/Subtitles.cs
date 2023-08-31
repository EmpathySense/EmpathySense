using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Subtitles : MonoBehaviour
{
        public TMP_Text textBox;
    void Start()
    {
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence()
    {
        yield return new WaitForSecondsRealtime(2f);//SI NO EMPIEZA DE UNA
        textBox.text = "Esto es la simulación de síntomas de una crisis de pánico,";
        yield return new WaitForSecondsRealtime(3.2f);
        textBox.text = "buscamos darte una idea de lo que se siente tener una ";
        yield return new WaitForSecondsRealtime(2.8f);
        textBox.text = "y que puedas identificar con rapidez cuando una persona esté pasando por esta.";
        yield return new WaitForSecondsRealtime(4.2f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1f); //ACTUALIZAR TIEMPO   
        textBox.text = "El cuerpo tiene una respuesta natural ante el peligro,";
        yield return new WaitForSecondsRealtime(3.3f);
        textBox.text = "preparándolo para defenderse o huir de él.";
        yield return new WaitForSecondsRealtime(3.4f);
        textBox.text = "Una crisis  de pánico se produce cuando se presenta esta respuesta en situaciones donde no hay peligro.";
        yield return new WaitForSecondsRealtime(6.9f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(3f);//ACTUALIZAR TIEMPO 
        textBox.text = "Uno de los síntomas más frecuente es tener una respiración alterada";
         yield return new WaitForSecondsRealtime(4.8f);
        textBox.text = "Es común experimentar respiración rápida y superficial, lo que se conoce como hiperventilación.";
        yield return new WaitForSecondsRealtime(7f);
        textBox.text = "Sintiendo que no puedes respirar profundo, lo que lleva a la sensación de falta de aire.";
        yield return new WaitForSecondsRealtime(4.5f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(2f);//ACTUALIZAR TIEMPO
        textBox.text = "Otro síntoma habitual es el aumento de la frecuencia cardiaca";
        yield return new WaitForSecondsRealtime(4.1f);
        textBox.text = "Durante una crisis de pánico, el sistema nervioso simpático se activa.";
        yield return new WaitForSecondsRealtime(4.5f);
        textBox.text = "Sintiendo que tu corazón late rápidamente y fuerte en tu pecho.";
        yield return new WaitForSecondsRealtime(4.5f);
        textBox.text = "Esta sensación puede ser angustiante y generar más ansiedad.";
        yield return new WaitForSecondsRealtime(3.5f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(2.1f);//ACTUALIZAR TIEMPO
        textBox.text = "Esto puede ir acompañado de náuseas y mareo.";
        yield return new WaitForSecondsRealtime(3.7f);
        textBox.text = " Además puedes experimentar malestar estomacal o sentir vértigo.";
        yield return new WaitForSecondsRealtime(5.5f);
        textBox.text = " Lo que puede aumentar la sensación de descontrol durante la crisis.";
        yield return new WaitForSecondsRealtime(4.7f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(2.2f); //ACTUALIZAR TIEMPO
        textBox.text = "Es frecuente sentir una presión en el pecho";
        yield return new WaitForSecondsRealtime(2.7f);
        textBox.text = "Puedes sentir como si algo pesado estuviera te estuviera aplastando.";
        yield return new WaitForSecondsRealtime(3.5f);
        textBox.text = "Esta sensación puede generar preocupación de tener un problema físico grave,";
        yield return new WaitForSecondsRealtime(4.2f);
        textBox.text = "que no necesariamente se condiciona con la realidad de la situación.";
        yield return new WaitForSecondsRealtime(4.1f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(2.5f);//ACTUALIZAR TIEMPO
        textBox.text = "También en aquellos casos puedes experimentar sensación de desconexión";
        yield return new WaitForSecondsRealtime(4.5f);
        textBox.text = "como si estuvieras observando la realidad desde afuera o como si estuvieras en un sueño.";
        yield return new WaitForSecondsRealtime(4.7f);
        textBox.text = "Puedes sentir que tus pensamientos y emociones están separados de tu cuerpo";
        yield return new WaitForSecondsRealtime(4.6f);
        textBox.text = "o que el mundo a tu alrededor no parece real.";
        yield return new WaitForSecondsRealtime(2.9f);
        textBox.text = "Esto se conoce como despersonalización o desrealización";
        yield return new WaitForSecondsRealtime(4.6f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(3.8f);//ACTUALIZAR TIEMPO
        textBox.text = "Por último, quizás puedas sentir estar a punto de desmayarte";
        yield return new WaitForSecondsRealtime(3.6f);
        textBox.text = "Lo que sería una pérdida temporal de la conciencia";
        yield return new WaitForSecondsRealtime(3f);
        textBox.text = "En esta sensación puedes experimentar visión borrosa,";
        yield return new WaitForSecondsRealtime(3.8f);
        textBox.text = "debilidad generalizada, mareo, sudoración o una palidez notable en la piel.";
        yield return new WaitForSecondsRealtime(5.4f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1.1f);//ACTUALIZAR TIEMPO
        textBox.text = "Debes saber que las crisis de pánico no tienen un tiempo establecido,";
        yield return new WaitForSecondsRealtime(4f);
        textBox.text = "suelen durar de 5 a 20 minutos.";
        yield return new WaitForSecondsRealtime(2.5f);
        textBox.text = "Además no todas las personas tienen los mismos síntomas,";
        yield return new WaitForSecondsRealtime(3.5f);
        textBox.text = "pero los que conocistes son los más frecuentes.";
        yield return new WaitForSecondsRealtime(3.1f);
        textBox.text = " te invitamos a que te informes en la documentación adicional que se encuentra en la página web de nuestra aplicación.";
        yield return new WaitForSecondsRealtime(6.2f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1.1f);//ACTUALIZAR TIEMPO
        textBox.text = "Al haber experimentado una simulación,";
        yield return new WaitForSecondsRealtime(2.5f);
        textBox.text = "ahora tienes una comprensión más profunda de cómo puede sentirse una crisis de pánico.";
        yield return new WaitForSecondsRealtime(5f);
        textBox.text = "Esta comprensión te permite ser una persona más sensible y atenta hacia quienes están pasando por ello. ";
        yield return new WaitForSecondsRealtime(6.3f);
        textBox.text = "";

    }
}
