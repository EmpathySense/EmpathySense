using UnityEngine;
using System.Collections;

namespace Viveport.Internal
{
#if UNITY_ANDROID
    internal partial class Api
    {
        internal static int Init(StatusCallback callback, string pchAppKey)
        {
            return Android.GetJavaAPI().CallStatic<int>("init", Android.CurrentContext, new AndroidPluginCallback(callback), pchAppKey);
        }

        internal static int Shutdown(StatusCallback callback)
        {
            return Android.GetJavaAPI().CallStatic<int>("shutdown", new AndroidPluginCallback(callback));
        }

        internal static string Version()
        {
            return Android.GetJavaAPI().CallStatic<string>("version");
        }

        internal static void GetLicense(Viveport.Api.LicenseChecker checker, string appId, string appKey)
        {
            Android.GetJavaAPI().CallStatic("getLicense", Android.CurrentContext, new AndroidLicenseChecker(checker), appId, appKey);
        }

        internal class AndroidLicenseChecker : UnityEngine.AndroidJavaProxy
        {
            private Viveport.Api.LicenseChecker checker;

            internal AndroidLicenseChecker(Viveport.Api.LicenseChecker checker) : base("com.htc.viveport.Api$LicenseChecker")
            {
                this.checker = checker;
            }

            internal void onSuccess(long issueTime, long expirationTime, int latestVersion, bool updateRequired)
            {
                Core.Logger.Log("AndroidLicenseChecker onSuccess: " + latestVersion + ":" + updateRequired);
                if (checker != null)
                    checker.OnSuccess(issueTime, expirationTime, latestVersion, updateRequired);
            }

            internal void onFailure(int errorCode, string errorMessage)
            {
                Core.Logger.Log("AndroidLicenseChecker onFailure: " + errorCode + ":" + errorMessage);
                if (checker != null)
                    checker.OnFailure(errorCode, errorMessage);
            }
        }
    }

    internal partial class User
    {
		internal static int IsReady(StatusCallback callback)
		{
			return Android.GetJavaUser().CallStatic<int>("isReady", Android.CurrentContext, new AndroidPluginCallback(callback));
		}

        internal static string GetUserId()
        {
            return Android.GetJavaUser().CallStatic<string>("getUserID", Android.CurrentContext);
        }

        internal static string GetUserName()
        {
            return Android.GetJavaUser().CallStatic<string>("getUserName", Android.CurrentContext);
        }

        internal static string GetUserAvatarUrl()
        {
            return Android.GetJavaUser().CallStatic<string>("getUserAvatar", Android.CurrentContext);
        }
    }

    internal partial class UserStats
    {
        internal static int IsReady(StatusCallback callback)
        {
            return Android.GetJavaUserStats().CallStatic<int>("isReady", Android.CurrentContext, new AndroidPluginCallback(callback));
        }

        // Stats
        internal static int DownloadStats(StatusCallback callback)
        {
            return Android.GetJavaUserStats().CallStatic<int>("downloadStats", Android.CurrentContext, new AndroidPluginCallback(callback));
        }

        internal static int UploadStats(StatusCallback callback)
        {
            return Android.GetJavaUserStats().CallStatic<int>("uploadStats", Android.CurrentContext, new AndroidPluginCallback(callback));
        }

        internal static int SetStat(string pchName, int nData)
        {
            return Android.GetJavaUserStats().CallStatic<int>("setStats", pchName, nData);
        }

        internal static int SetStat(string pchName, float fData)
        {
            return Android.GetJavaUserStats().CallStatic<int>("setStats", pchName, fData);
        }

        internal static int GetStat(string pchName, int nData)
        {
            return Android.GetJavaUserStats().CallStatic<int>("getStats", pchName, nData);
        }

        internal static float GetStat(string pchName, float fData)
        {
            return Android.GetJavaUserStats().CallStatic<float>("getStats", pchName, fData);
        }

        internal static bool GetAchievement(string pchName)
        {
            return Android.GetJavaUserStats().CallStatic<bool>("getAchievement", pchName);
        }

        internal static int GetAchievementUnlockTime(string pchName)
        {
            return Android.GetJavaUserStats().CallStatic<int>("getAchievementUnlockTime", pchName);
        }

        internal static int SetAchievement(string pchName)
        {
            return Android.GetJavaUserStats().CallStatic<int>("setAchievement", pchName);
        }

        internal static int ClearAchievement(string pchName)
        {
            return Android.GetJavaUserStats().CallStatic<int>("clearAchievement", pchName);
        }

        internal static string GetAchievementDisplayAttribute(string pchName, EAchievementDisplayAttribute attr)
        {
            return Android.GetJavaUserStats().CallStatic<string>("getAchievementDisplayAttribute", pchName, (int)attr);
        }

        internal static string GetAchievementDisplayAttribute(string pchName, EAchievementDisplayAttribute attr, ELocale locale)
        {
            return Android.GetJavaUserStats().CallStatic<string>("getAchievementDisplayAttribute", pchName, (int)attr, (int)locale);
        }

        internal static string GetAchievementIcon(string pchName)
        {
            return Android.GetJavaUserStats().CallStatic<string>("getAchievementIcon", pchName);
        }

        // Leaderboard
        internal static int DownloadLeaderboardScores(StatusCallback callback, string pchLeaderboardName, ELeaderboardDataRequest nDataRequest, ELeaderboardDataTimeRange nTimeRange, int nRangeStart, int nRangeEnd)
        {
            int nRequestValue = (int)nDataRequest;
            int nTimeValue = (int)nTimeRange;
            return Android.GetJavaUserStats().CallStatic<int>("getLeaderboard", Android.CurrentContext, new AndroidPluginCallback(callback), pchLeaderboardName, nRangeStart, nRangeEnd, nTimeValue, nRequestValue);
        }

        internal static int UploadLeaderboardScore(StatusCallback callback, string pchLeaderboardName, int nScores)
        {
            return Android.GetJavaUserStats().CallStatic<int>("setLeaderboard", Android.CurrentContext, new AndroidPluginCallback(callback), pchLeaderboardName, nScores);
        }

        internal static Leaderboard GetLeaderboardScore(int nIndex)
        {
            Leaderboard pLeaderboardEntry = new Leaderboard();
            pLeaderboardEntry.UserName = Android.GetJavaUserStats().CallStatic<string>("getLeaderboardNameData", nIndex);
            pLeaderboardEntry.Rank = Android.GetJavaUserStats().CallStatic<int>("getLeaderboardRankData", nIndex);
            pLeaderboardEntry.Score = Android.GetJavaUserStats().CallStatic<int>("getLeaderboardScoreData", nIndex);
            return pLeaderboardEntry;
        }

        internal static int GetLeaderboardScoreCount()
        {
            return Android.GetJavaUserStats().CallStatic<int>("getLeaderboardScoreCount");
        }

        internal static ELeaderboardSortMethod GetLeaderboardSortMethod()
        {
            return (ELeaderboardSortMethod)Android.GetJavaUserStats().CallStatic<int>("getLeaderboardSortType");
        }

        internal static ELeaderboardDisplayType GetLeaderboardDisplayType()
        {
            return (ELeaderboardDisplayType)Android.GetJavaUserStats().CallStatic<int>("getLeaderboardDisplayType");
        }
    }

    internal partial class Deeplink
    {
        internal static void IsReady(StatusCallback callback)
        {
            Android.GetDeeplink().CallStatic("isReady", new AndroidPluginCallback(callback));
        }

        internal static void GoToApp(Viveport.Deeplink.DeeplinkChecker checker, string appId, string launchData)
        {
            Android.GetDeeplink().CallStatic("GoToApp", Android.CurrentContext, new AndroidDeeplinkChecker(checker), appId, launchData);
        }

        internal static void GoToStore(Viveport.Deeplink.DeeplinkChecker checker, string appId)
        {
            Android.GetDeeplink().CallStatic("GoToStore", Android.CurrentContext, new AndroidDeeplinkChecker(checker), appId);
        }

        internal static void GoToAppOrGoToStore(Viveport.Deeplink.DeeplinkChecker checker, string appId, string launchData)
        {
            Android.GetDeeplink().CallStatic("GoToAppOrGoToStore", Android.CurrentContext, new AndroidDeeplinkChecker(checker), appId, launchData);
        }

        internal static string GetAppLaunchData()
        {
            return Android.GetDeeplink().CallStatic<string>("GetAppLaunchData", Android.CurrentContext);
        }

        internal class AndroidDeeplinkChecker : UnityEngine.AndroidJavaProxy
        {
            private Viveport.Deeplink.DeeplinkChecker checker;

            internal AndroidDeeplinkChecker(Viveport.Deeplink.DeeplinkChecker checker) : base("com.htc.viveport.Api$StatusCallback2")
            {
                this.checker = checker;
            }

            internal void onSuccess()
            {
                Core.Logger.Log("AndroidDeeplinkChecker onSuccess");
                if (checker != null)
                    checker.OnSuccess();
            }

            internal void onFailure(int errorCode, string errorMessage)
            {
                Core.Logger.Log("AndroidDeeplinkChecker onFailure: " + errorCode + ":" + errorMessage);
                if (checker != null)
                    checker.OnFailure(errorCode, errorMessage);
            }
        }
    }

    internal partial class IAPurchase
    {
        internal static void IsReady(IAPurchaseCallback callback, string pchAppKey)
        {
            Android.GetJavaIAPurchase().CallStatic("isReady", Android.CurrentContext, new AndroidPluginCallback(callback), pchAppKey);
        }
        internal static void Request(IAPurchaseCallback callback, string pchPrice)
        {
            Android.GetJavaIAPurchase().CallStatic("request", Android.CurrentContext, new AndroidPluginCallback(callback), pchPrice);
        }
        internal static void Request(IAPurchaseCallback callback, string pchPrice, string pchUserData)
        {
            Android.GetJavaIAPurchase().CallStatic("request", Android.CurrentContext, new AndroidPluginCallback(callback), pchPrice, pchUserData);
        }
        internal static void Purchase(IAPurchaseCallback callback, string pchPurchaseId)
        {
            Android.GetJavaIAPurchase().CallStatic("makePurchase", Android.CurrentContext, new AndroidPluginCallback(callback), pchPurchaseId);
        }
        internal static void Query(IAPurchaseCallback callback, string pchPurchaseId)
        {
            Android.GetJavaIAPurchase().CallStatic("query", Android.CurrentContext, new AndroidPluginCallback(callback), pchPurchaseId);
        }
        internal static void Query(IAPurchaseCallback callback)
        {
            Android.GetJavaIAPurchase().CallStatic("query", Android.CurrentContext, new AndroidPluginCallback(callback));
        }
        internal static void GetBalance(IAPurchaseCallback callback)
        {
            Android.GetJavaIAPurchase().CallStatic("getBalance", Android.CurrentContext, new AndroidPluginCallback(callback));
        }
        internal static void RequestSubscription(IAPurchaseCallback callback, string pchPrice, string pchFreeTrialType, int nFreeTrialValue,
            string pchChargePeriodType, int nChargePeriodValue, int nNumberOfChargePeriod, string pchPlanId)
        {
            Android.GetJavaIAPurchase().CallStatic("requestSubscription", Android.CurrentContext, new AndroidPluginCallback(callback),
                pchPrice, pchFreeTrialType, nFreeTrialValue, pchChargePeriodType, nChargePeriodValue, nNumberOfChargePeriod, pchPlanId);
        }
        internal static void RequestSubscriptionWithPlanID(IAPurchaseCallback callback, string pchPlanId)
        {
            Android.GetJavaIAPurchase().CallStatic("requestSubscriptionWithPlanId", Android.CurrentContext, new AndroidPluginCallback(callback), pchPlanId);
        }
        internal static void Subscribe(IAPurchaseCallback callback, string pchSubscriptionId)
        {
            Android.GetJavaIAPurchase().CallStatic("subscribe", Android.CurrentContext, new AndroidPluginCallback(callback), pchSubscriptionId);
        }
        internal static void QuerySubscription(IAPurchaseCallback callback, string pchSubscriptionId)
        {
            Android.GetJavaIAPurchase().CallStatic("querySubscription", Android.CurrentContext, new AndroidPluginCallback(callback), pchSubscriptionId);
        }
        internal static void QuerySubscriptionList(IAPurchaseCallback callback)
        {
            Android.GetJavaIAPurchase().CallStatic("querySubscriptionList", Android.CurrentContext, new AndroidPluginCallback(callback));
        }
        internal static void CancelSubscription(IAPurchaseCallback callback, string pchSubscriptionId)
        {
            Android.GetJavaIAPurchase().CallStatic("cancelSubscription", Android.CurrentContext, new AndroidPluginCallback(callback), pchSubscriptionId);
        }
    }
    
    internal partial class Subscription
    {
        internal static int IsReady(StatusCallback2 callback)
        {
            Android.GetJavaSubscription().CallStatic("isReady", Android.CurrentContext, new AndroidPluginCallback(callback));
            return 0;
        }

        internal static bool IsWindowsSubscriber()
        {
            return Android.GetJavaSubscription().CallStatic<bool>("isWindowsSubscriber");
        }

        internal static bool IsAndroidSubscriber()
        {
            return Android.GetJavaSubscription().CallStatic<bool>("isAndroidSubscriber");
        }

        internal static ESubscriptionTransactionType GetTransactionType()
        {
            int curTransactionType = Android.GetJavaSubscription().CallStatic<AndroidJavaObject>("getTransactionType").Call<int>("getValue");

            ESubscriptionTransactionType curType = ESubscriptionTransactionType.UNKNOWN;

            switch (curTransactionType)
            {
                case 0:
                    curType = ESubscriptionTransactionType.UNKNOWN;
                    break;
                case 1:
                    curType = ESubscriptionTransactionType.PAID;
                    break;
                case 2:
                    curType = ESubscriptionTransactionType.REDEEM;
                    break;
                case 3:
                    curType = ESubscriptionTransactionType.FREEE_TRIAL;
                    break;
            }
            return curType;
        }
 
    }

    internal partial class Token
    {
        internal static int IsReady(StatusCallback IsReadyCallback)
        {
            Android.GetJavaSessionToken().CallStatic("isReady", Android.CurrentContext, new AndroidPluginCallback(IsReadyCallback));
            return 0;
        }

        internal static int GetSessionToken(StatusCallback2 GetSessionTokenCallback)
        {
            Android.GetJavaSessionToken().CallStatic("getSessionToken", Android.CurrentContext, new AndroidPluginCallback(GetSessionTokenCallback));
            return 0;
        }
    }

    internal partial class Avatar
    {
        internal static int DownloadAvatar(StatusCallback2 callback, int id)
        {
            Android.GetJavaAvatar().CallStatic("downloadAvatar", Android.CurrentContext, new AndroidPluginCallback(callback), id);
            return 0;
        }

        internal static int DownloadAvatar(StatusCallback2 callback, string url)
        {
            Android.GetJavaAvatar().CallStatic("downloadAvatar", Android.CurrentContext, new AndroidPluginCallback(callback), url);
            return 0;
        }

        internal static int GetAvatarList(StatusCallback2 callback)
        {
            Android.GetJavaAvatar().CallStatic("getAvatarList", Android.CurrentContext, new AndroidPluginCallback(callback));
            return 0;
        }
    }

    internal partial class Android
    {
        private static UnityEngine.AndroidJavaObject _api;
        private static UnityEngine.AndroidJavaObject _iAPurchase;
        private static UnityEngine.AndroidJavaClass _unityPlayer;
        private static UnityEngine.AndroidJavaObject _user;
        private static UnityEngine.AndroidJavaObject _userStats;
        private static UnityEngine.AndroidJavaObject _subscription;
        private static UnityEngine.AndroidJavaObject _sessionToken;
        private static UnityEngine.AndroidJavaObject _deeplink;
        private static UnityEngine.AndroidJavaObject _avatar;

        internal static UnityEngine.AndroidJavaObject GetJavaAPI()
        {
            if (_api == null)
            {
                _api = new UnityEngine.AndroidJavaObject("com.htc.viveport.Api");
            }
            return _api;
        }

        internal static UnityEngine.AndroidJavaObject GetJavaUser()
        {
            if (_user == null)
            {
                _user = new UnityEngine.AndroidJavaObject("com.htc.viveport.User");
            }
            return _user;
        }
        internal static UnityEngine.AndroidJavaObject GetJavaUserStats()
        {
            if (_userStats == null)
            {
                _userStats = new UnityEngine.AndroidJavaObject("com.htc.viveport.UserStats");
            }
            return _userStats;
        }

        internal static UnityEngine.AndroidJavaObject GetJavaIAPurchase()
        {
            if (_iAPurchase == null)
            {
                _iAPurchase = new UnityEngine.AndroidJavaObject("com.htc.viveport.IAPurchase");
            }
            return _iAPurchase;
        }
        
        internal static UnityEngine.AndroidJavaObject GetJavaSubscription()
        {
            if (_subscription == null)
            {
                _subscription = new UnityEngine.AndroidJavaObject("com.htc.viveport.Subscription");
            }
            return _subscription;
        }

        internal static UnityEngine.AndroidJavaObject GetJavaSessionToken()
        {
            if (_sessionToken == null)
            {
                _sessionToken = new UnityEngine.AndroidJavaObject("com.htc.viveport.SessionToken");
            }
            return _sessionToken;
        }

        internal static UnityEngine.AndroidJavaObject GetDeeplink()
        {
            if (_deeplink == null)
            {
                _deeplink = new UnityEngine.AndroidJavaObject("com.htc.viveport.Deeplink");
            }
            return _deeplink;
        }

        internal static UnityEngine.AndroidJavaObject GetJavaAvatar()
        {
            if (_avatar== null)
            {
                _avatar = new UnityEngine.AndroidJavaObject("com.htc.viveport.Avatar");
            }
            return _avatar;
        }
        
        internal static UnityEngine.AndroidJavaClass UnityPlayer
        {
            get
            {
                if (_unityPlayer == null)
                {
                    _unityPlayer = new UnityEngine.AndroidJavaClass("com.unity3d.player.UnityPlayer");
                }

                return _unityPlayer;
            }
        }
        internal static UnityEngine.AndroidJavaObject CurrentActivity
        {
            get
            {
                return UnityPlayer.GetStatic<UnityEngine.AndroidJavaObject>("currentActivity");
            }
        }

        internal static UnityEngine.AndroidJavaObject CurrentContext
        {
            get
            {
                return CurrentActivity;
            }
        }
    }

    internal class AndroidPluginCallback : UnityEngine.AndroidJavaProxy
    {
        private IAPurchaseCallback callback;
        private StatusCallback statusCallback;
        private StatusCallback2 statusCallback2;

        internal AndroidPluginCallback(IAPurchaseCallback callback) : base("com.htc.viveport.Api$StatusCallback")
        {
            this.callback = callback;
        }

        internal AndroidPluginCallback(StatusCallback callback) : base("com.htc.viveport.Api$StatusCallback")
        {
            this.statusCallback = callback;
        }

        internal AndroidPluginCallback(StatusCallback2 callback) : base("com.htc.viveport.Api$StatusCallback")
        {
            this.statusCallback2 = callback;
        }

        internal void onResult(int statusCode, string result)
        {
            Core.Logger.Log("ENTER callback onSuccess: " + statusCode + ":" + result);
            if (callback != null)
            {
                callback(statusCode, result);
            }
            if (statusCallback != null)
            {
                statusCallback(statusCode);
            }
            if (statusCallback2 != null)
            {
                statusCallback2(statusCode, result);
            }
        }
    }
#endif
}
