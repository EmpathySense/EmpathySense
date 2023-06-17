using System;
using UnityEngine;
using UnityEngine.UI;
using Viveport;


public class ViveportDemo_IAP : MonoBehaviour
{
#if UNITY_ANDROID
    private int nWidth = 240, nHeight = 120;
#else
    private int nWidth = 80, nHeight = 40;
#endif
    private int nXStart = 10, nYStart = 40;

    static string IAP_APP_TEST_ID = "app_VIVEPORT_ID" ; // replace it with your VIVEPORT ID
    static string IAP_APP_TEST_KEY = "app_API_Key" ; // replace it with your IAP - API Key
    private Result mListener;
    private static bool bIsDuplicatedSubscription = false;
    private static bool bInit_Done = false, bIsReady_Done = false, bShutdown_Done = false;
    private Text winText;

    // Use this for initialization
    void Start()
    {
        Viveport.Core.Logger.Log("mListener: " + mListener);
        mListener = new Result();
        Viveport.Core.Logger.Log("mListener end: " + mListener); 

#if UNITY_ANDROID
        Api.Init(InitStatusHandler, IAP_APP_TEST_ID);
#endif
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
        GUIStyle CustButtonStyle = new GUIStyle("button");
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

        if (GUI.Button(new Rect(nXStart + nWidth + 350 , nYStart + 1 * nWidth + 10, nWidth + 70, nHeight), "RequestWithUserData", customButton))
        {
            Viveport.Core.Logger.Log("Request");
            IAPurchase.Request(mListener, "1", "Knife");
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

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 6 * nWidth + 50, nWidth + 50, nHeight),"QueryPurchaseList", customButton))
        {
           Viveport.Core.Logger.Log("QueryPurchaseList");
           IAPurchase.Query(mListener);
        }

        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 7 * nWidth + 40, nWidth + 50, nHeight), "QuerySubscriptionList", customButton))
        {
            Viveport.Core.Logger.Log("QuerySubscriptionList");
            IAPurchase.QuerySubscriptionList(mListener);
        }

#else
        GUI.Label(new Rect(15, 5, 100, 20), "TOP API");
        GUI.Label(new Rect(15, 65, 800, 20), "==========================================================================================================================================");
        GUI.Label(new Rect(15, 80, 100, 20), "IAP - Purchase");
        GUI.Label(new Rect(15, 145, 800, 20), "==========================================================================================================================================");
        GUI.Label(new Rect(15, 165, 200, 20), "IAP - Purchase With User Data");
        GUI.Label(new Rect(15, 235, 800, 20), "==========================================================================================================================================");
        GUI.Label(new Rect(15, 260, 120, 20), "IAP - Subscription");
        GUI.Label(new Rect(15, 380, 800, 20), "==========================================================================================================================================");
        GUI.Label(new Rect(15, 400, 120, 20), "IAP - Optional API");

        if ((bInit_Done == false && bIsReady_Done == false) || bShutdown_Done == true)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.gray;

        if (GUI.Button(new Rect(nXStart, nYStart - 15 , nWidth, nHeight), "1.Init", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("Init");
            Api.Init(InitStatusHandler, IAP_APP_TEST_ID);
        }

        if (bInit_Done == true && bIsReady_Done ==false)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.gray;

        if (GUI.Button(new Rect(nXStart + nWidth + 15 , nYStart - 15 , nWidth, nHeight), "2.IsReady", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("IsReady");
            IAPurchase.IsReady(mListener, IAP_APP_TEST_KEY);
        }

        if (bInit_Done == false || bShutdown_Done == true)
            GUI.contentColor = Color.grey;
        else
            GUI.contentColor = Color.white;

        if (GUI.Button(new Rect(nXStart + 2 * nWidth + 30, nYStart - 15 , nWidth + 20, nHeight), "3.Shutdown", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("Shutdown");
            Api.Shutdown(ShutdownStatusHandler);
        }

        if (bInit_Done == true && bIsReady_Done == true && bShutdown_Done == false)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.grey;

        // *******************************   IAP Purchase  ******************************//

        if (GUI.Button(new Rect(nXStart, nYStart + 60 , nWidth + 20 , nHeight), "1.1.Request", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("Request");
            //add virtual items into cache
            mListener.mItem.items = new string[3];
            mListener.mItem.items[0] = "sword";
            mListener.mItem.items[1] = "knife";
            mListener.mItem.items[2] = "medicine";
            IAPurchase.Request(mListener, "1");
        }

        if (GUI.Button(new Rect(nXStart + 1 * nWidth + 40, nYStart + 60 , nWidth + 20, nHeight), "1.2.Purchase", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("Purchase mListener.mItem.ticket=" + mListener.mItem.ticket);
            IAPurchase.Purchase(mListener, mListener.mItem.ticket);
        }

        if (GUI.Button(new Rect(nXStart + 2 * nWidth + 80, nYStart + 60 , nWidth + 20, nHeight), "2.Query", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("Query");
            IAPurchase.Query(mListener, mListener.mItem.ticket);
        }

        // ************************   IAP Purchase With User Data  ******************************//

        if (bInit_Done == true && bIsReady_Done == true && bShutdown_Done == false)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.grey;

        if (GUI.Button(new Rect(nXStart, nYStart + nHeight + 110, nWidth + 90, nHeight), "1.1.RequestWithUserData", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("Request");
            IAPurchase.Request(mListener, "1", "Knife");
        }

        if (GUI.Button(new Rect(nXStart + 1 * nWidth + 105, nYStart + nHeight + 110 , nWidth + 20, nHeight), "1.2.Purchase", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("Purchase mListener.mItem.ticket=" + mListener.mItem.ticket);
            IAPurchase.Purchase(mListener, mListener.mItem.ticket);
        }

        if (GUI.Button(new Rect(nXStart + 2 * nWidth + 140, nYStart + nHeight + 110, nWidth + 80, nHeight), "2.QueryPurchaseList", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("QueryPurchaseList");
            IAPurchase.Query(mListener);
        }

        // ************************   IAP Subscription  ******************************//

        if (bInit_Done == true && bIsReady_Done == true && bShutdown_Done == false)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.grey;

        if (GUI.Button(new Rect(nXStart, nYStart + 2 * nHeight + 165, nWidth + 160 , nHeight), "1.1-1.RequestSubscription                 ", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("RequestSubscription");
            IAPurchase.RequestSubscription(mListener, "1", "month", 1, "day", 2, 3, "pID");
        }

        if (GUI.Button(new Rect(nXStart, nYStart + 3 * nHeight + 175, nWidth + 160, nHeight), "1.1-2.RequestSubscriptionWithPlanID", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("RequestSubscriptionWithPlanID");
            IAPurchase.RequestSubscriptionWithPlanID(mListener, "pID");
        }

        if (GUI.Button(new Rect(nXStart + 1 * nWidth + 180 , nYStart + 2 * nHeight + 190, nWidth + 20 , nHeight), "1.2.Subscribe", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("Subscribe");
            //Before you call Subscribe(), you should call IAPurchase.QuerySubscription(mListener, null) first to get user's current subscriptions
            //When you find your current subscription is in user's current subscriptions(plan id is identical), and this subscription's status is ACTIVE, 
            //You should prompt a dialog to remind user that he has subscribed this plan to avoid duplicated subscription
            IAPurchase.Subscribe(mListener, mListener.mItem.subscription_ticket);
        }

        if (GUI.Button(new Rect(nXStart + 2 * nWidth + 218, nYStart + 2 * nHeight + 165, nWidth + 90, nHeight), "2.1.QuerySubscription", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("QuerySubscription");
            IAPurchase.QuerySubscription(mListener, mListener.mItem.subscription_ticket);
        }

        if (GUI.Button(new Rect(nXStart + 2 * nWidth + 218, nYStart + 3 * nHeight + 175, nWidth + 105, nHeight), "2.2.QuerySubscriptionList", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("QuerySubscriptionList");
            IAPurchase.QuerySubscriptionList(mListener);
        }

        if (GUI.Button(new Rect(nXStart + 3 * nWidth + 345, nYStart + 2 * nHeight + 190, nWidth + 80, nHeight), "3.CancelSubscription", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("CancelSubscription");
            IAPurchase.CancelSubscription(mListener, mListener.mItem.subscription_ticket);
        }

        // ************************   IAP Optional API  ******************************//
        if  (bInit_Done == true && bIsReady_Done == true && bShutdown_Done == false)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.gray;

        if (GUI.Button(new Rect(nXStart, nYStart + 2 * nHeight + 305 , nWidth + 20, nHeight), "GetBalance", CustButtonStyle))
        {
            Viveport.Core.Logger.Log("GetBalance");
            IAPurchase.GetBalance(mListener);
        }

#endif


    }

    private static void InitStatusHandler(int nResult)
    {
        bInit_Done = true;
        bShutdown_Done = false;
        Viveport.Core.Logger.Log("InitStatusHandler: " + nResult);
    }

    private static void ShutdownStatusHandler(int nResult)
    {
        bShutdown_Done = true;
        bInit_Done = false;
        bIsReady_Done = false;
        Viveport.Core.Logger.Log("ShutdownStatusHandler: " + nResult);
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
            bIsReady_Done = true;
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

        public override void OnQuerySuccess(IAPurchase.QueryListResponse response)
        {
            //return all purchases which status equal to "success"
            Viveport.Core.Logger.Log("[OnQueryListSuccess] total=" + response.total + ", from=" + response.from + ", to=" + response.to);
            foreach (IAPurchase.QueryResponse2 qr in response.purchaseList)
            {
                Viveport.Core.Logger.Log("purchase_id=" + qr.purchase_id + ", user_data=" + qr.user_data + ", price=" + qr.price + ", currency=" + qr.currency +
                    ", paid_timestamp=" + qr.paid_timestamp);              
            }
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

        public override void OnQuerySubscriptionListSuccess(IAPurchase.Subscription[] subscriptionlist)
        {
            int size = subscriptionlist.Length;
            Viveport.Core.Logger.Log("[OnQuerySubscriptionListSuccess] subscriptionlist size =" + size);
            if (size > 0)
            {
                for (int i = 0; i < size; i++)
                {
                    //when status equals "ACTIVE", then this subscription is valid, you can deliver virtual items to user
                    Viveport.Core.Logger.Log("[OnQuerySubscriptionListSuccess] subscriptionlist[" + i + "].status =" + subscriptionlist[i].status +
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

