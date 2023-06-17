using UnityEngine;
using System.Collections;
using Viveport;

public class ViveportPreviewDemo_DLC : MonoBehaviour
{
#if UNITY_ANDROID
    private int nWidth = 180, nHeight = 100;
#else
    private int nWidth = 140, nHeight = 40;
#endif
    private int nXStart = 10, nYStart = 35;

    static string APP_ID = "76d0898e-8772-49a9-aa55-1ec251a21686";
    private static bool bInit = true, bIsReady = false, isDLCAvailable = false;
    private static int dlcCount = -1;
    private int dlcIndex = 0;

    // Use this for initialization
    void Start()
    {
        Api.Init(InitStatusHandler, APP_ID);
    }

    void OnGUI()
    {
        GUIStyle CustButton = new GUIStyle("button");
#if UNITY_ANDROID
        CustButton.fontSize = 23;
#endif
        if (bInit == false)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.gray;

        // Init function
        if (GUI.Button(new Rect(nXStart, nYStart, nWidth, nHeight), "Init", CustButton))
        {
            if (bInit == false)
                Api.Init(InitStatusHandler, APP_ID);
        }

        if (bInit == true)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.grey;

        // Shutdown function
        if (GUI.Button(new Rect((nXStart + 1 * (nWidth + 10)), nYStart, nWidth, nHeight), "Shutdown", CustButton))
        {
            if (bInit == true)
                Api.Shutdown(ShutdownHandler);
        }

#if !UNITY_ANDROID
        // IsDLCReady function
        if (GUI.Button(new Rect((nXStart + 2 * (nWidth + 10)), nYStart, nWidth, nHeight), "IsDLCReady", CustButton))
        {
            if (bInit == true)
                DLC.IsDlcReady(IsDLCReadyHandler);
        }

        // GetDLCCount function
        if (GUI.Button(new Rect((nXStart + 3 * (nWidth + 10)), nYStart, nWidth, nHeight), "GetDLCCount", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                dlcCount = DLC.GetCount();
                Viveport.Core.Logger.Log("DLC count: " + dlcCount);
            }
        }

        // GetIsAvailable function
        if (GUI.Button(new Rect((nXStart + 4 * (nWidth + 10)), nYStart, nWidth, nHeight), "GetDLCDataByIndex", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                bool isInRange = DLC.GetIsAvailable(dlcIndex, out APP_ID, out isDLCAvailable);
                if (isInRange)
                {
                    Viveport.Core.Logger.Log("Is DLC available: " + isDLCAvailable);
                    Viveport.Core.Logger.Log("DLC APP ID: " + APP_ID);
                }
            }
        }
#endif
    }

    private static void InitStatusHandler(int nResult)
    {
        if (nResult == 0)
        {
            bInit = true;
            bIsReady = false;
            Viveport.Core.Logger.Log("InitStatusHandler is successful");
        }
        else
        {
            // Init error, close your app and make sure your app ID is correct or not.
            bInit = false;
            Viveport.Core.Logger.Log("InitStatusHandler error : " + nResult);
        }
    }

    private static void ShutdownHandler(int nResult)
    {
        if (nResult == 0)
        {
            bInit = false;
            bIsReady = false;
            Viveport.Core.Logger.Log("ShutdownHandler is successful");
        }
        else
        {
            Viveport.Core.Logger.Log("ShutdownHandler error: " + nResult);
        }
    }

    private static void IsDLCReadyHandler(int nResult)
    {
        if (nResult == 0)
        {
            Viveport.Core.Logger.Log("DLC is ready");
            bIsReady = true;
        }
        else
        {
            Viveport.Core.Logger.Log("IsDLCReadyHandler error: " + nResult);
        }
    }
}