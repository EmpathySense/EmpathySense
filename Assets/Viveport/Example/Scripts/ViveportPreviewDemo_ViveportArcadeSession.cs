using System;
using UnityEngine;
using Viveport;
using Viveport.Arcade;

public class ViveportPreviewDemo_ViveportArcadeSession : MonoBehaviour
{
    private int nWidth = 120, nHeight = 40;
    private int nXStart = 10, nYStart = 35;

    static string VIVEPORT_ARCADE_APP_TEST_ID = "app_test_id";
#if UNITY_STANDALONE_WIN
    private Result mListener;
#endif

    // Use this for initialization
    void Start()
    {
        Api.Init(InitStatusHandler, VIVEPORT_ARCADE_APP_TEST_ID);
#if UNITY_STANDALONE_WIN
        Viveport.Core.Logger.Log("Version: " + Api.Version());
        mListener = new Result();
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    private static void InitStatusHandler(int nResult)
    {
        Viveport.Core.Logger.Log("InitStatusHandler: " + nResult);
    }

#if UNITY_STANDALONE_WIN
    void OnGUI()
    {
        //******************************************************
        //*        Viveport Arcade Session sample code
        //******************************************************

        if (GUI.Button(new Rect(nXStart, nYStart, nWidth, nHeight), "Session IsReady"))
        {
            Viveport.Core.Logger.Log("Session IsReady");
            Session.IsReady(mListener);
        }

        if (GUI.Button(new Rect(nXStart, nYStart + 1 * nWidth + 10, nWidth, nHeight), "Session Start"))
        {
            Viveport.Core.Logger.Log("Session Start");
            Session.Start(mListener);
        }

        if (GUI.Button(new Rect(nXStart, nYStart + 2 * nWidth + 20, nWidth, nHeight), "Session Stop"))
        {
            Viveport.Core.Logger.Log("Session Stop");
            Session.Stop(mListener);
        }
    }
    //Declare class which extends Session.SessionListener and implement callback to get the response of APIs
    //You can make this class for your own customization, for the example here, we return appID, Guid if success
    //You may store Guid for any purpose
    class Result : Session.SessionListener
    {
        public override void OnSuccess(string pchAppID)
        {
            Viveport.Core.Logger.Log("[Session OnSuccess] pchAppID=" + pchAppID);
        }

        public override void OnStartSuccess(string pchAppID, string pchGuid)
        {
            Viveport.Core.Logger.Log("[Session OnStartSuccess] pchAppID=" + pchAppID + ",pchGuid=" + pchGuid);
        }

        public override void OnStopSuccess(string pchAppID, string pchGuid)
        {
            Viveport.Core.Logger.Log("[Session OnStopSuccess] pchAppID=" + pchAppID + ",pchGuid=" + pchGuid);
        }

        public override void OnFailure(int nCode, string pchMessage)
        {
            Viveport.Core.Logger.Log("[Session OnFailed] nCode=" + nCode + ",pchMessage=" + pchMessage);
            //If you get failure callback, you need to do some error handling in your code, for example here,
            //we pause the content. You can also show any warning message to inform user. Please do not start a round
            //if you get failure callback of IsReady() and Start().
            Time.timeScale = 0;
        }
    }
#endif
}
