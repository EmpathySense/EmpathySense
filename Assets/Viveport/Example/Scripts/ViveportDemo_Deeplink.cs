using Viveport;
using UnityEngine;

public class ViveportDemo_Deeplink : MonoBehaviour {

    // Get a VIVEPORT ID and VIVEPORT Key from the VIVEPORT Developer Console. Please refer to here:
    // https://developer.viveport.com/documents/sdk/en/viveport_sdk/definition/get_viveportid.html

    static string VIVEPORT_ID = "VIVEPORT ID of the content";           // replace with developer VIVEPORT ID
    static string VIVEPORT_KEY = "VIVEPORT Key of the content";         // replace with developer VIVEPORT Key

    private string GoToApp_Viveport_ID = "VIVEPORT ID of target APP";
    private string GoToStore_Viveport_ID = "VIVEPORT ID of target APP";
    private string LaunchData = "Start_Content";
    private string LaunchBranchName = "PROD"; // PROD or BETA
    private const int SUCCESS = 0;
    private static bool bInitComplete = false;

    void Awake()
    {
        MainThreadDispatcher mainThreadDispatcher = GameObject.FindObjectOfType<MainThreadDispatcher>();
        if (!mainThreadDispatcher)
        {
            GameObject main = new GameObject();
            main.AddComponent<MainThreadDispatcher>();
            main.name = "MainThreadDispatcher";
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(100, 20, 600, 600),
            " Show Message in Console log \n" +
            " KeyDown \" Q \" Use GoToApp \n" +
            " KeyDown \" W \" Use GoToApp with BranchName \n" +
            " KeyDown \" E \" Use GoToStore \n" +
            " KeyDown \" R \" Use GoToAppOrGoToStore \n" +
            " KeyDown \" A \" Use GetAppLaunchData \n");
    }

    void Start()
    {
        Api.Init(InitStatusHandler, VIVEPORT_ID);       // initialize VIVEPORT platform
    }

    void OnDestroy()
    {
        Api.Shutdown(ShutdownHandler);
    }

    private static void InitStatusHandler(int nResult)          // The callback of Api.init()
    {
        if (nResult == SUCCESS)
        {
            Debug.Log("VIVEPORT init pass");
            Viveport.Deeplink.IsReady(IsReadyHandler);
        }
        else
        {
            Debug.Log("VIVEPORT init fail");
            Application.Quit();                                 // the response of Api.Init() is fail                                      
        }
    }

    private static void IsReadyHandler(int nResult)
    {
        if (nResult == SUCCESS)
        {
            Debug.Log("VIVEPORT Deeplink.IsReady pass");
            bInitComplete = true;
        }
        else
        {
            Debug.Log("VIVEPORT Deeplink.IsReady fail");
            Application.Quit();                                // the response of Deeplink.IsReady() is fail
        }
    }

    private static void ShutdownHandler(int nResult)
    {
#if !UNITY_ANDROID
        if (nResult == SUCCESS)
        {
            bInitComplete = false;
            Viveport.Core.Logger.Log("ShutdownHandler is successful");
        }
        else
        {
            Viveport.Core.Logger.Log("ShutdownHandler error: " + nResult);
        }
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (bInitComplete)
            {
#if !UNITY_ANDROID
                Deeplink.GoToApp(GoToAppHandler, GoToApp_Viveport_ID, LaunchData);
#else
                Deeplink.GoToApp(new GotoAppDeeplinkChecker(), GoToApp_Viveport_ID, LaunchData);
#endif
            }

        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (bInitComplete)
            {
#if !UNITY_ANDROID
                Deeplink.GoToApp(GoToAppHandler, GoToApp_Viveport_ID, LaunchData, LaunchBranchName);
#else
                Deeplink.GoToApp(new GotoAppDeeplinkChecker(), GoToApp_Viveport_ID, LaunchData, LaunchBranchName);
#endif
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (bInitComplete)
            {
#if !UNITY_ANDROID
                Deeplink.GoToStore(GoToStoreHandler, GoToStore_Viveport_ID);
#else
                Deeplink.GoToStore(new GotoStoreDeeplinkChecker(), GoToStore_Viveport_ID);
#endif
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (bInitComplete)
            {
#if !UNITY_ANDROID
                Deeplink.GoToAppOrGoToStore(GoToAppOrGoToStoreHandler, GoToStore_Viveport_ID, LaunchData);
#else
                Deeplink.GoToAppOrGoToStore(new GoToAppOrGoToStoreDeeplinkChecker(), GoToStore_Viveport_ID, LaunchData);
#endif
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (bInitComplete)
            {
                var launchDataString = Deeplink.GetAppLaunchData();
                Debug.Log(launchDataString);
            }
        }
    }

#if !UNITY_ANDROID
    private static void GoToAppHandler(int errorCode, string message)
    {
        if (errorCode == SUCCESS)
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GoToApp is successful");
            });
        }
        else
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GoToApp errorCode : " + errorCode + " ErrorMessage : " + message);
            });
        }
    }

    private static void GoToStoreHandler(int errorCode, string message)
    {
        if (errorCode == SUCCESS)
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GoToStore is successful");
            });
        }
        else
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GoToStore errorCode : " + errorCode + " ErrorMessage : " + message);
            });
        }
    }

    private static void GoToAppOrGoToStoreHandler(int errorCode, string message)
    {
        if (errorCode == SUCCESS)
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GoToAppOrGoToStore is successful");
            });
        }
        else
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GoToAppOrGoToStore errorCode : " + errorCode + " ErrorMessage : " + message);
            });
        }
    }
#endif

#if UNITY_ANDROID
    class GotoAppDeeplinkChecker : Deeplink.DeeplinkChecker
    {
        public override void OnSuccess()
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GotoApp is successful");
            });
        }

        public override void OnFailure(int errorCode, string errorMessage)
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GotoApp errorCode : " + errorCode + " ErrorMessage : " + errorMessage);
            });
        }
    }

    class GotoStoreDeeplinkChecker : Deeplink.DeeplinkChecker
    {
        public override void OnSuccess()
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GotoStore is successful");
            });
        }

        public override void OnFailure(int errorCode, string errorMessage)
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GotoStore errorCode : " + errorCode + " ErrorMessage : " + errorMessage);
            });
        }
    }

    class GoToAppOrGoToStoreDeeplinkChecker : Deeplink.DeeplinkChecker
    {
        public override void OnSuccess()
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GoToAppOrGoToStore is successful");
            });
        }

        public override void OnFailure(int errorCode, string errorMessage)
        {
            MainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("GoToAppOrGoToStore errorCode : " + errorCode + " ErrorMessage : " + errorMessage);
            });
        }
    }
#endif
}
