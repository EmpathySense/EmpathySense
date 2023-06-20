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

    private void Start()
    {
        //Se referencia a la configuración que se quiere acceder (el tick para activarlo o desactivarlo en este caso)
        _postProcessVolume = GetComponent<PostProcessVolume>();
        _postProcessVolume.profile.TryGetSettings(out _bloom);
        _postProcessVolume.profile.TryGetSettings(out _depthofField);
        _postProcessVolume.profile.TryGetSettings(out _lensDistortion);

        //falta asignarla a la secuencia, dependiendo como se maneje
        DepthOfFieldOff(true);
        BloomOff(true);
        LensDistortionOff(true);
        

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
        }
        else
        {
            _bloom.active = false;
        }
    }
    public void LensDistortionOff(bool on)
    {
        if (on)
        {
            _lensDistortion.active = true;
        }
        else
        {
            _lensDistortion.active = false;
        }
    }
}
