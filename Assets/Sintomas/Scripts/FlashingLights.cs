using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLights : MonoBehaviour
{

    public float brilloMinimo = 0.3f;
    public float brilloMaximo = 1.5f;
    public float velocidadCambio = 0.5f;

    private LensFlare[] lensFlares;

    public GameObject StreetLight;
    public GameObject StreetLight2;
    public GameObject StreetLight3;
    public GameObject StreetLight4;
    public GameObject StreetLight5;

    // Start is called before the first frame update
    void Start()
    {
        lensFlares = FindObjectsOfType<LensFlare>();
    }

    // Update is called once per frame
    void Update()
    {
        float nuevoBrillo = Mathf.Lerp(brilloMinimo, brilloMaximo, (Mathf.Sin(Time.time * velocidadCambio) + 1f) / 2f);
        float nuevoBrillo2 = Mathf.Lerp(brilloMaximo, brilloMinimo, (Mathf.Sin(Time.time * velocidadCambio) + 1f) / 2f);
        Debug.Log(nuevoBrillo);
        Debug.Log(nuevoBrillo2);
        // Iterar sobre cada objeto Lens Flare y cambiar el valor de Brightness
        //acceder al componente LensFlare y cambiar el valor de Brightness
        StreetLight.GetComponent<LensFlare>().brightness = nuevoBrillo;
        StreetLight2.GetComponent<LensFlare>().brightness = nuevoBrillo;
        StreetLight3.GetComponent<LensFlare>().brightness = nuevoBrillo;
        StreetLight4.GetComponent<LensFlare>().brightness = nuevoBrillo2;
        StreetLight5.GetComponent<LensFlare>().brightness = nuevoBrillo2;
    }
}
