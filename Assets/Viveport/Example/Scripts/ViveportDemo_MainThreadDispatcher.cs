using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Viveport;

public class ViveportDemo_MainThreadDispatcher : MonoBehaviour
{
    // If you change the text of UGUI (or anything relate to GameObject), you should call that line from main thread.
    public Text uiText;
    static Text myApiResultText;

    private readonly static string appId = "Your APP ID";
    private readonly static string apiKey = "Your API KEY";

    void Start()
    {
        myApiResultText = uiText;
        Api.Init(InitCallback, appId);
    }

    void InitCallback(int errorCode)
    {
        if (errorCode == 0) // If Init success.
        {
            // You can uncomment this line. This line will "NOT" work fine.
            //IAPurchase.IsReady(new ThisCallbackWillNotWorkFine(), apiKey);

            // This line will work fine.
            IAPurchase.IsReady(new ThisCallbackWillWorkFine_01(), apiKey);

            // This line will also work fine.
            //IAPurchase.IsReady(new ThisCallbackWillWorkFine_02(), apiKey);
        }
    }

    class ThisCallbackWillNotWorkFine : IAPurchase.IAPurchaseListener
    {
        public override void OnSuccess(string pchCurrencyName)
        {
            // Error: should only be called from main thread
            myApiResultText.text = string.Format("The Currency is: {0}", pchCurrencyName);
        }
    }

    class ThisCallbackWillWorkFine_01 : IAPurchase.IAPurchaseListener
    {
        public override void OnSuccess(string pchCurrencyName)
        {
            Action action = () => { myApiResultText.text = string.Format("The Currency is: {0}", pchCurrencyName); };

            // Main thread dispatcher will let the action be called from main thread.
            MainThreadDispatcher.Instance().Enqueue(action);
        }
    }

    class ThisCallbackWillWorkFine_02 : IAPurchase.IAPurchaseListener
    {
        public override void OnSuccess(string pchCurrencyName)
        {
            MainThreadDispatcher.Instance().Enqueue(ShowResult(pchCurrencyName));
        }

        IEnumerator ShowResult(string pchCurrencyName)
        {
            myApiResultText.text = string.Format("The Currency is: {0}", pchCurrencyName);
            yield return null;
        }
    }
}
