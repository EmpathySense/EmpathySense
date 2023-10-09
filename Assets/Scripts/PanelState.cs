using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelState : MonoBehaviour
{
    public static PanelState instance;

    // Variables para almacenar el estado que deseas compartir
    public bool activarMenuSIM = false;

    private void Awake()
    {
        // Asegurarse de que solo haya una instancia del objeto GameState en el juego
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
