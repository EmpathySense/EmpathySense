using UnityEngine;
using Viveport;

public class ViveportDemo_Subscription : MonoBehaviour
{
#if UNITY_ANDROID
    private int nWidth = 180, nHeight = 100;
#else
    private int nWidth = 140, nHeight = 40;
#endif
    private static bool bIsReady = false;
    private int nXStart = 10, nYStart = 35;

    static string APP_ID = "76d0898e-8772-49a9-aa55-1ec251a21686";
    private static bool bInit = true;


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

        // IsReady function
        if (GUI.Button(new Rect((nXStart + 2 * (nWidth + 10)), nYStart, nWidth, nHeight), "IsReady", CustButton))
        {
            if (bInit == true)
                Subscription.IsReady(IsReadyHandler);
        }

        // GetUserStatus function
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///    !!!IMPORTANT!!!   Please wait for the IsReadyHandler before calling the GetUserStatus function       ///
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (GUI.Button(new Rect((nXStart + 3 * (nWidth + 10)), nYStart, nWidth, nHeight), "GetUserStatus", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                var userStatus = Subscription.GetUserStatus();
                var isWindowsSubscriber = userStatus.Platforms.Contains(SubscriptionStatus.Platform.Windows) ? "true" : "false";
                var isAndroidSubscriber = userStatus.Platforms.Contains(SubscriptionStatus.Platform.Android) ? "true" : "false";
                var transactionType = "";

                switch (userStatus.Type)
                {
                    case SubscriptionStatus.TransactionType.Unknown:
                        transactionType = "Unknown";
                        break;
                    case SubscriptionStatus.TransactionType.Paid:
                        transactionType = "Paid";
                        break;
                    case SubscriptionStatus.TransactionType.Redeem:
                        transactionType = "Redeem";
                        break;
                    case SubscriptionStatus.TransactionType.FreeTrial:
                        transactionType = "FreeTrial";
                        break;
                    default:
                        transactionType = "Unknown";
                        break;
                }

                Viveport.Core.Logger.Log("User is a Windows subscriber: " +  isWindowsSubscriber + ".  User is a Android subscriber: " + isAndroidSubscriber + ".  transactionType :" + transactionType);

            }
        }
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
#if !UNITY_ANDROID
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
#endif
    }

    private static void IsReadyHandler(int nResult, string message)
    {
        if (nResult == 0)
        {
            Viveport.Core.Logger.Log("Subscription is ready");
            bIsReady = true;
        }
        else
        {
            Viveport.Core.Logger.Log("Subscription IsReadyHandler error: " + nResult + " Message : " + message);
        }
    }
}
