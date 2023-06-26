using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessing : MonoBehaviour
{
    private PostProcessVolume _postProcessVolume;
    private Bloom _bloom;
    private DepthOfField _depthofField;
    private LensDistortion _lensDistortion;


    private bool flagLens = false;
    public AnimationCurve intensityCurveLens;
    public float transitionDurationLens;
    public float timerLens;


    public AnimationCurve intensityCurveBloom;
    private bool flagBloom = false;
    public float transitionDurationBloom;
    public float timerBloom;

    private void Start()
    {
        //Se referencia a la configuración que se quiere acceder (el tick para activarlo o desactivarlo en este caso)
        _postProcessVolume = GetComponent<PostProcessVolume>();
        _postProcessVolume.profile.TryGetSettings(out _bloom);
        _postProcessVolume.profile.TryGetSettings(out _depthofField);
        _postProcessVolume.profile.TryGetSettings(out _lensDistortion);

        //falta asignarla a la secuencia, dependiendo como se maneje
        /* DepthOfFieldOff(true);
        BloomOff(true);
        LensDistortionOff(true); */
        

    }
    
    private void Update()
    {
        //Flag para entrar a la secuencia de transición
        if(flagLens)
        {
            if(timerLens < transitionDurationLens) 
            {
                //Se usa la animation curve para asignar los valores a la intensidad del efecto, la curva está en el objeto asignado
                timerLens += Time.deltaTime;
                float t = Mathf.Clamp01(timerLens / transitionDurationLens);
                float intensity = intensityCurveLens.Evaluate(t);
            
                _lensDistortion.intensity.value = intensity;
            }
        }

        if(flagBloom)
        {   
            if(timerBloom < transitionDurationBloom) 
            {
                Debug.Log("deberia cambiar intensity");
                timerBloom += Time.deltaTime;
                float t = Mathf.Clamp01(timerBloom / transitionDurationBloom);
                float intensity = intensityCurveBloom.Evaluate(t);
            
                _bloom.intensity.value = intensity;
            }
        }



    }

    //Funciones que prenden y apagan la configuración
    public void DepthOfFieldOff(bool on)
    {
        if (on)
        {
            _depthofField.active = true;
            
        }
        else
        {
            _depthofField.active = false;
        }
    }
    public void BloomOff(bool on)
    {
        if (on)
        {
            _bloom.active = true;
            float transitionDurationBloom = 10f;
            float timerBloom = 0f;
            flagBloom = true;
        }
        else
        {
            flagBloom = false;
            _bloom.active = false;
        }
    }

    public void LensDistortionOff(bool on)
    {
        if (on)
        {
            _lensDistortion.active = true;
            float transitionDurationLens = 10f;
            float timerLens = 0f;
            flagLens = true;
        }
        else
        {
            flagLens = false;
            _lensDistortion.active = false;
        }
    }
}
