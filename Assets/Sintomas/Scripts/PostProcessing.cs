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
    private ChromaticAberration _chromaticAberration;


    public float maxIntensityChromatic;
    private bool flagChromatic = false;
    public float transitionDurationChromatic;
    private float timerChromatic;
    private float timerChromaticOff = 0f;

   /*  private bool flagChromaticOff = false;
    public float timerChromaticOff; */



    public float maxIntensityBloom;
    private bool flagBloom = false;
    public float transitionDurationBloom;
    private float timerBloom;
    private float timerBloomOff = 0f;


    public float maxFocalLengthDepthOfField;
    private bool flagDepthOfField = false;
    public float transitionDurationDepthOfField;
    private float timerDepthOfField;
    private float timerDepthOfFieldOff = 0f;

    private void Start()
    {
        //Se referencia a la configuración que se quiere acceder (el tick para activarlo o desactivarlo en este caso)
        _postProcessVolume = GetComponent<PostProcessVolume>();
        _postProcessVolume.profile.TryGetSettings(out _bloom);
        _postProcessVolume.profile.TryGetSettings(out _depthofField);
        _postProcessVolume.profile.TryGetSettings(out _chromaticAberration);

        //falta asignarla a la secuencia, dependiendo como se maneje
        /* DepthOfFieldOff(true);
        BloomOff(true);
        LensDistortionOff(true); */
        

    }
    
    private void Update()
    {
        //Flag para entrar a la secuencia de transición
        if(flagChromatic)
        {
            if(timerChromatic < transitionDurationChromatic) 
            {
                //Se usa la animation curve para asignar los valores a la intensidad del efecto, la curva está en el objeto asignado
                timerChromatic += Time.deltaTime;
                float t = Mathf.Clamp01(timerChromatic / transitionDurationChromatic);
                float intensity = maxIntensityChromatic * t;
            
                _chromaticAberration.intensity.value = intensity;
            }
            else
            {
                timerChromaticOff += Time.deltaTime;
                float t = Mathf.Clamp01(timerChromaticOff / transitionDurationChromatic);
                float intensity = maxIntensityChromatic * (1.0f - t);
                _chromaticAberration.intensity.value = intensity;
            }
        }

        if(flagBloom)
        {   
            if(timerBloom < transitionDurationBloom) 
            {
                Debug.Log("deberia cambiar intensity");
                timerBloom += Time.deltaTime;
                float t = Mathf.Clamp01(timerBloom / transitionDurationBloom);
                float intensity = maxIntensityBloom * t;
            
                _bloom.intensity.value = intensity;
            }
            else
            {
                timerBloomOff += Time.deltaTime;
                float t = Mathf.Clamp01(timerBloomOff / transitionDurationBloom);
                float intensity = maxIntensityBloom * (1.0f - t);
                _bloom.intensity.value = intensity;
                Debug.Log(intensity);
            }
        }

        if(flagDepthOfField)
        {
            if(timerDepthOfField < transitionDurationDepthOfField) 
            {
                timerDepthOfField += Time.deltaTime;
                float t = Mathf.Clamp01(timerDepthOfField / transitionDurationDepthOfField);
                float intensity = maxFocalLengthDepthOfField * t;
            
                _depthofField.focalLength.value = intensity;
            }
            else
            {
                timerDepthOfFieldOff += Time.deltaTime;
                float t = Mathf.Clamp01(timerDepthOfFieldOff / transitionDurationDepthOfField);
                float intensity = maxFocalLengthDepthOfField * (1.0f - t);
                _depthofField.focalLength.value = intensity;
            }
        }



    }

    //Funciones que prenden y apagan la configuración
    public void DepthOfFieldOff(bool on)
    {
        if (on)
        {
            _depthofField.active = true;
            float transitionDurationDepthOfField = 10f;
            float timerDepthOfField = 0f;
            flagDepthOfField = true;   
        }
        else
        {
            flagDepthOfField = false;
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

    public void ChromaticAberrationOff(bool on)
    {
        if (on)
        {
            _chromaticAberration.active = true;
            float transitionDurationChromatic = 10f;
            float timerChromatic = 0f;
            flagChromatic = true;
        }
        else
        {
            flagChromatic = false;
            _chromaticAberration.active = false;
        }
    }
}
