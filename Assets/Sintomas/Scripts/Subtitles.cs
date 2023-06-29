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
        yield return new WaitForSecondsRealtime(1.4f);
        textBox.text = "Esta es la simulacion de síntomas de una crisis de pánico";
        yield return new WaitForSecondsRealtime(2.9f);
        textBox.text = "buscamos darte una idea de lo que se siente tener una";
        yield return new WaitForSecondsRealtime(2.8f);
        textBox.text = "y que puedas identificar con rapidez cuando una persona esté pasando por esta.";
        yield return new WaitForSecondsRealtime(4.2f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1f);
        textBox.text = "El cuerpo tiene una respuesta natural ante el estrés o el peligro,";
        yield return new WaitForSecondsRealtime(3f);
        textBox.text = "preparándolo para hacerle frente o huir de él.";
        yield return new WaitForSecondsRealtime(3f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(0.7f);
        textBox.text = "Un ataque de pánico se produce cuando se presenta esta respuesta en situaciones donde no hay peligro.";
        yield return new WaitForSecondsRealtime(4.6f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(3f);
        textBox.text = "Uno de los síntomas más frecuente es la irregularidad en la respiración.";
         yield return new WaitForSecondsRealtime(4f);
        textBox.text = "Es común experimentar respiración rápida y superficial, lo que se conoce como hiperventilación.";
        yield return new WaitForSecondsRealtime(7f);
        textBox.text = "Además, es posible sentir que no puedes respirar lo suficientemente profundo,";
        yield return new WaitForSecondsRealtime(5f);
        textBox.text = " lo que lleva a la sensación de falta de aire.";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(2.5f);
        textBox.text = "Otro síntoma habitual es el aumento de la frecuencia cardiaca";
        yield return new WaitForSecondsRealtime(3.6f);
        textBox.text = "Durante una crisis de pánico, el cuerpo puede experimentar una respuesta de 'lucha o huida'";
        yield return new WaitForSecondsRealtime(6f);
        textBox.text = "en la que el sistema nervioso simpático se activa y aumenta la frecuencia cardíaca.";
        yield return new WaitForSecondsRealtime(4f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1.2f);
        textBox.text = "Puedes sentir que tu corazón late rápidamente y fuertemente en tu pecho.";
        yield return new WaitForSecondsRealtime(4f);
        textBox.text = "Esta sensación puede ser aterradora y puede generar más ansiedad.";
        yield return new WaitForSecondsRealtime(4f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(2.3f);
        textBox.text = "Estos pueden ir acompañados por un sentimiento de náuseas y mareo.";
        yield return new WaitForSecondsRealtime(4.5f);
        textBox.text = " El estrés y la ansiedad intensa pueden afectar el sistema gastrointestinal,";
        yield return new WaitForSecondsRealtime(4.5f);
        textBox.text = "donde estaría el origen de este síntoma.";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1.3f);
        textBox.text = "Puedes experimentar una sensación de malestar estomacal o sentir vértigo.";
        yield return new WaitForSecondsRealtime(4.4f);
        textBox.text = " Lo que puede aumentar la sensación de miedo y descontrol durante la crisis.";
        yield return new WaitForSecondsRealtime(3.8f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1.5f);
        textBox.text = "Es frecuente sentir una presión en el pecho";
        yield return new WaitForSecondsRealtime(2.7f);
        textBox.text = "Puedes sentir como si algo pesado estuviera sobre el o como si te estuvieran aplastando.";
        yield return new WaitForSecondsRealtime(4.7f);
        textBox.text = "Esta sensación puede ser angustiante";
        yield return new WaitForSecondsRealtime(2.9f);
        textBox.text = "y puede generar preocupación de tener un problema físico grave,";
        yield return new WaitForSecondsRealtime(3.1f);
        textBox.text = "que no necesariamente se condiciona con la realidad de la situación.";
        yield return new WaitForSecondsRealtime(3.2f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1.3f);
        textBox.text = "También es posible experimentar despersonalización y desrealización.";
        yield return new WaitForSecondsRealtime(5.2f);
        textBox.text = "Sensación de desconexión o separación de sí mismo,";
        yield return new WaitForSecondsRealtime(3.4f);
        textBox.text = "como si estuvieras observando la realidad desde afuera o como si estuvieras en un sueño.";
        yield return new WaitForSecondsRealtime(4.8f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1f);
        textBox.text = "puedes sentir que tus pensamientos y emociones están separados de tu cuerpo";
        yield return new WaitForSecondsRealtime(4.2f);
        textBox.text = "o que el mundo a tu alrededor no parece real.";
        yield return new WaitForSecondsRealtime(3f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1.5f);
        textBox.text = "Por último, puedes sentir que estás a punto de desmayarte.";
        yield return new WaitForSecondsRealtime(4f);
        textBox.text = "Lo que sería una pérdida temporal de la conciencia";
        yield return new WaitForSecondsRealtime(2.8f);
        textBox.text = "y la capacidad de mantenerse en pie";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text = "ocurre debido a la disminución del flujo sanguíneo al cerebro.";
        yield return new WaitForSecondsRealtime(3.5f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1.1f);
        textBox.text = "En esta sensación de estar a punto de desmayarte";
        yield return new WaitForSecondsRealtime(3.2f);
        textBox.text = "puedes experimentar visión borrosa, debilidad generalizada,";
        yield return new WaitForSecondsRealtime(4.1f);
        textBox.text = "mareo, sudoración o una palidez notable en la piel.";
        yield return new WaitForSecondsRealtime(4.5f);
        textBox.text = "";
        yield return new WaitForSecondsRealtime(1f);
        textBox.text = "Los ataques de pánico suelen durar de 5 a 20 minutos,";
        yield return new WaitForSecondsRealtime(3.8f);
        textBox.text = "y no todas las personas sufren de los mismos síntomas a la hora de tener una crisis de pánico,";
        yield return new WaitForSecondsRealtime(4.7f);
        textBox.text = "pero estos son de los más frecuentes.";
        yield return new WaitForSecondsRealtime(2f);
        textBox.text = " te invitamos a que te informes en la documentación adicional que se encuentra en la página web de nuestra aplicación.";
        yield return new WaitForSecondsRealtime(5.2f);
        textBox.text = "";

    }
}
