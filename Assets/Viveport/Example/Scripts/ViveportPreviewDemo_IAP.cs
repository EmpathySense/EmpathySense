using System;
using UnityEngine;
using Viveport;

public class ViveportPreviewDemo_IAP : MonoBehaviour
{
#if UNITY_ANDROID
    private int nWidth = 240, nHeight = 120;
#else
    private int nWidth = 80, nHeight = 40;
#endif
    private int nXStart = 10, nYStart = 35;

    static string IAP_APP_TEST_ID = "57bde8be-0eb4-4324-8243-3a482adcb1c3";
    static string IAP_APP_TEST_KEY = "KiZMLHQUTHqhXZEXMb8zImxHoTzBZjeM";

    private Result mListener;
    private static bool bIsDuplicatedSubscription = false;

    // Use this for initialization
    void Start()
    {
        mListener = new Result();
        Api.Init(InitStatusHandler, IAP_APP_TEST_ID);
#if !UNITY_ANDROID
        Viveport.Core.Logger.Log("Version: " + Api.Version());
        Viveport.Core.Logger.Log("UserId: " + User.GetUserId());
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnGUI()
    {
        //******************************************************
        //*                  IAP sample code
        //*****************************************************
#if UNITY_ANDROID
        GUIStyle customButton = new GUIStyle("button");
        customButton.fontSize = 24;
        if (GUI.Button(new Rect(nXStart, nYStart, nWidth, nHeight), "IsReady", customButton))
        {
            Viveport.Core.Logger.Log("IsReady");
            IAPurchase.IsReady(mListener, IAP_APP_TEST_KEY);
        }

        if (GUI.Button(new Rect(nXStart, nYStart + 1 * nWidth + 10, nWidth, nHeight), "Request", customButton))
        {
            Viveport.Core.Logger.Log("Request");
            //add virtual items into cache
            mListener.mItem.items = new string[3];
            mListener.mItem.items[0] = "sword";
            mListener.mItem.items[1] = "knife";
            mListener.mItem.items[2] = "medicine";
            IAPurchase.Request(mListener, "1");
        }

        if (GUI.Button(new Rect(nXStart, nYStart + 2 * nWidth + 20, nWidth, nHeight), "Purchase", customButton))
        {
            Viveport.Core.Logger.Log("Purchase mListener.mItem.ticket=" + mListener.mItem.ticket);
            IAPurchase.Purchase(mListener, mListener.mItem.ticket);
        }

        if (GUI.Button(new Rect(nXStart, nYStart + 3 * nWidth + 30, nWidth, nHeight), "Query", customButton))
        {
            Viveport.Core.Logger.Log("Query");
            IAPurchase.Query(mListener, mListener.mItem.ticket);
        }

        if (GUI.Button(new Rect(nXStart, nYStart + 4 * nWidth + 40, nWidth, nHeight), "GetBalance", customButton))
        {
            Viveport.Core.Logger.Log("GetBalance");
            IAPurchase.GetBalance(mListener);
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 1 * nWidth + 10, nWidth + 70, nHeight), "RequestSubscription", customButton))
        {
            Viveport.Core.Logger.Log("RequestSubscription");
            IAPurchase.RequestSubscription(mListener, "1", "month", 1, "day", 2, 3, "pID");
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 2 * nWidth + 20, nWidth + 120, nHeight), "RequestSubscriptionWithPlanID", customButton))
        {
            Viveport.Core.Logger.Log("RequestSubscriptionWithPlanID");
            IAPurchase.RequestSubscriptionWithPlanID(mListener, "pID");
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 3 * nWidth + 30, nWidth, nHeight), "Subscribe", customButton))
        {
            Viveport.Core.Logger.Log("Subscribe bIsDuplicatedSubscription=" + bIsDuplicatedSubscription);
            IAPurchase.Subscribe(mListener, mListener.mItem.subscription_ticket);
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 4 * nWidth + 40, nWidth + 50, nHeight), "QuerySubscription", customButton))
        {
            Viveport.Core.Logger.Log("QuerySubscription");
            bIsDuplicatedSubscription = false;
            IAPurchase.QuerySubscription(mListener, mListener.mItem.subscription_ticket);
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 5 * nWidth + 50, nWidth + 50, nHeight), "CancelSubscription", customButton))
        {
            Viveport.Core.Logger.Log("CancelSubscription");
            IAPurchase.CancelSubscription(mListener, mListener.mItem.subscription_ticket);
        }
        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 6 * nWidth + 60, nWidth + 60, nHeight), "QueryAllSubscription", customButton))
        {
            Viveport.Core.Logger.Log("QueryAllSubscription");
            bIsDuplicatedSubscription = false;
            IAPurchase.QuerySubscription(mListener, null);
        }
#else
        if (GUI.Button(new Rect(nXStart, nYStart, nWidth, nHeight), "IsReady"))
        {
            Viveport.Core.Logger.Log("IsReady");
            IAPurchase.IsReady(mListener, IAP_APP_TEST_KEY);
        }

        if (GUI.Button(new Rect(nXStart, nYStart + 1 * nWidth + 10, nWidth, nHeight), "Request"))
        {
            Viveport.Core.Logger.Log("Request");
            //add virtual items into cache
            mListener.mItem.items = new string[3];
            mListener.mItem.items[0] = "sword";
            mListener.mItem.items[1] = "knife";
            mListener.mItem.items[2] = "medicine";
            IAPurchase.Request(mListener, "1");
        }

        if (GUI.Button(new Rect(nXStart, nYStart + 2 * nWidth + 20, nWidth, nHeight), "Purchase"))
        {
            Viveport.Core.Logger.Log("Purchase mListener.mItem.ticket=" + mListener.mItem.ticket);
            IAPurchase.Purchase(mListener, mListener.mItem.ticket);
        }

        if (GUI.Button(new Rect(nXStart, nYStart + 3 * nWidth + 30, nWidth, nHeight), "Query"))
        {
            Viveport.Core.Logger.Log("Query");
            IAPurchase.Query(mListener, mListener.mItem.ticket);
        }

        if (GUI.Button(new Rect(nXStart, nYStart + 4 * nWidth + 40, nWidth, nHeight), "GetBalance"))
        {
            Viveport.Core.Logger.Log("GetBalance");
            IAPurchase.GetBalance(mListener);
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 1 * nWidth + 10, nWidth + 70, nHeight), "RequestSubscription"))
        {
            Viveport.Core.Logger.Log("RequestSubscription");
            IAPurchase.RequestSubscription(mListener, "1", "month", 1, "day", 2, 3, "pID");
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 2 * nWidth + 20, nWidth + 120, nHeight), "RequestSubscriptionWithPlanID"))
        {
            Viveport.Core.Logger.Log("RequestSubscriptionWithPlanID");
            IAPurchase.RequestSubscriptionWithPlanID(mListener, "pID");
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 3 * nWidth + 30, nWidth, nHeight), "Subscribe"))
        {
            Viveport.Core.Logger.Log("Subscribe");
            IAPurchase.Subscribe(mListener, mListener.mItem.subscription_ticket);
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 4 * nWidth + 40, nWidth + 50, nHeight), "QuerySubscription"))
        {
            Viveport.Core.Logger.Log("QuerySubscription");
            IAPurchase.QuerySubscription(mListener, mListener.mItem.subscription_ticket);
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 5 * nWidth + 50, nWidth + 50, nHeight), "CancelSubscription"))
        {
            Viveport.Core.Logger.Log("CancelSubscription");
            IAPurchase.CancelSubscription(mListener, mListener.mItem.subscription_ticket);
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 6 * nWidth + 60, nWidth + 60, nHeight), "QueryAllSubscription"))
        {
            Viveport.Core.Logger.Log("QueryAllSubscription");
            IAPurchase.QuerySubscription(mListener, null);
        }
#endif


    }

    private static void InitStatusHandler(int nResult)
    {
        Viveport.Core.Logger.Log("InitStatusHandler: " + nResult);
    }
    //a sample class which store purchase id and puchased items
    public class Item
    {
        public string ticket = "test_id";
        public string[] items;
        public string subscription_ticket = "unity_test_subscriptionId";
    }
    //Declare class which extends IAPurchase.IAPurchaseListener and implement callback to get the response of APIs
    //You can make this class for your own customization, for the example here, we store necessary purchase information into cache
    //You can store it in db or use any other cache mechanism
    class Result : IAPurchase.IAPurchaseListener
    {
        public Item mItem = new Item();
        public override void OnSuccess(string pchCurrencyName)
        {
            Viveport.Core.Logger.Log("[OnSuccess] pchCurrencyName=" + pchCurrencyName);
        }

        public override void OnRequestSuccess(string pchPurchaseId)
        {
            mItem.ticket = pchPurchaseId;
            Viveport.Core.Logger.Log("[OnRequestSuccess] pchPurchaseId=" + pchPurchaseId + ",mItem.ticket=" + mItem.ticket);
        }

        public override void OnPurchaseSuccess(string pchPurchaseId)
        {
            Viveport.Core.Logger.Log("[OnPurchaseSuccess] pchPurchaseId=" + pchPurchaseId);
            if (mItem.ticket == pchPurchaseId)//if stored id equals the purchase id which is returned by OnPurchaseSuccess(), give the virtual items to user
            {
                Viveport.Core.Logger.Log("[OnPurchaseSuccess] give items to user");
                //to implement: give virtual items to user
            }
        }

        public override void OnQuerySuccess(IAPurchase.QueryResponse response)
        {
            //when status equals "success", then this purchase is valid, you can deliver virtual items to user
            Viveport.Core.Logger.Log("[OnQuerySuccess] purchaseId=" + response.purchase_id + ",status=" + response.status);
        }
        public override void OnBalanceSuccess(string pchBalance)
        {
            Viveport.Core.Logger.Log("[OnBalanceSuccess] pchBalance=" + pchBalance);
        }

        public override void OnRequestSubscriptionSuccess(string pchSubscriptionId)
        {
            mItem.subscription_ticket = pchSubscriptionId;
            Viveport.Core.Logger.Log("[OnRequestSubscriptionSuccess] pchSubscriptionId=" + pchSubscriptionId + ",mItem.subscription_ticket=" + mItem.subscription_ticket);
        }

        public override void OnRequestSubscriptionWithPlanIDSuccess(string pchSubscriptionId)
        {
            mItem.subscription_ticket = pchSubscriptionId;
            Viveport.Core.Logger.Log("[OnRequestSubscriptionWithPlanIDSuccess] pchSubscriptionId=" + pchSubscriptionId + ",mItem.subscription_ticket=" + mItem.subscription_ticket);
        }

        public override void OnSubscribeSuccess(string pchSubscriptionId)
        {
            Viveport.Core.Logger.Log("[OnSubscribeSuccess] pchSubscriptionId=" + pchSubscriptionId);
            if (mItem.subscription_ticket == pchSubscriptionId)
            {
                Viveport.Core.Logger.Log("[OnSubscribeSuccess] give virtual items to user");
                //to implement: give virtual items to user
            }
        }

        public override void OnQuerySubscriptionSuccess(IAPurchase.Subscription[] subscriptionlist)
        {
            int size = subscriptionlist.Length;
            Viveport.Core.Logger.Log("[OnQuerySubscriptionSuccess] subscriptionlist size =" + size);
            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    //when status equals "ACTIVE", then this subscription is valid, you can deliver virtual items to user
                    Viveport.Core.Logger.Log("[OnQuerySubscriptionSuccess] subscriptionlist[" + i + "].status =" + subscriptionlist[i].status +
                        ", subscriptionlist[" + i + "].plan_id = " + subscriptionlist[i].plan_id);
                    if (subscriptionlist[i].plan_id == "pID" && subscriptionlist[i].status == "ACTIVE")
                    {
                        bIsDuplicatedSubscription = true;
                    }
                }
            }
        }

        public override void OnCancelSubscriptionSuccess(bool bCanceled)
        {
            Viveport.Core.Logger.Log("[OnCancelSubscriptionSuccess] bCanceled=" + bCanceled);
        }

        public override void OnFailure(int nCode, string pchMessage)
        {
            Viveport.Core.Logger.Log("[OnFailed] " + nCode + ", " + pchMessage);
        }
    }
}

