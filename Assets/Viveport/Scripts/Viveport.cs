using AOT;
using LitJson;
using PublicKeyConvert;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using Viveport.Core;

namespace Viveport
{
    public enum Locale
    {
        US = Internal.ELocale.k_ELocaleUS /* 0 */,
        DE = Internal.ELocale.k_ELocaleDE /* 1 */,
        JP = Internal.ELocale.k_ELocaleJP /* 2 */,
        KR = Internal.ELocale.k_ELocaleKR /* 3 */,
        RU = Internal.ELocale.k_ELocaleRU /* 4 */,
        CN = Internal.ELocale.k_ELocaleCN /* 5 */,
        TW = Internal.ELocale.k_ELocaleTW /* 6 */,
        FR = Internal.ELocale.k_ELocaleFR /* 7 */,
    }

    namespace Core
    {
        public class Logger
        {
            private const string LoggerTypeNameUnity = "UnityEngine.Debug";

            private static bool _hasDetected;
            private static bool _usingUnityLog = true;
            private static Type _unityLogType;

            public static void Log(string message)
            {
                if (!_hasDetected || _usingUnityLog)
                {
                    UnityLog(message);
                }
                else
                {
                    ConsoleLog(message);
                }
            }

            private static void ConsoleLog(string message)
            {
                Console.WriteLine(message);
                _hasDetected = true;
            }

            private static void UnityLog(string message)
            {
                try
                {
                    if (_unityLogType == null)
                    {
                        _unityLogType = GetType(LoggerTypeNameUnity);
                    }
                    var methodInfo = _unityLogType.GetMethod("Log", new[] { typeof(string) });
                    methodInfo.Invoke(null, new object[] { message });
                    _usingUnityLog = true;
                }
                catch (Exception)
                {
                    ConsoleLog(message);
                    _usingUnityLog = false;
                }
                _hasDetected = true;
            }

            private static Type GetType(string typeName)
            {
                var type = Type.GetType(typeName);
                if (type != null)
                {
                    return type;
                }
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    type = assembly.GetType(typeName);
                    if (type != null)
                    {
                        return type;
                    }
                }
                return null;
            }
        }
    }

    public delegate void StatusCallback(int nResult);
    public delegate void StatusCallback2(int nResult, string message);
    public delegate void QueryRuntimeModeCallback(int nResult, int emu);

    public class Leaderboard
    {
        public int Rank { get; set; }
        public int Score { get; set; }
        public string UserName { get; set; }
    }

    public class SubscriptionStatus
    {
        public enum Platform
        {
            Windows,
            Android
        }

        public enum TransactionType
        {
            Unknown,
            Paid,
            Redeem,
            FreeTrial
        }

        public List<Platform> Platforms { get; set; }
        public TransactionType Type { get; set; }

        public SubscriptionStatus()
        {
            Platforms = new List<Platform>();
            Type = TransactionType.Unknown;
        }
    }

    public partial class Api
    {
        internal static readonly List<Internal.GetLicenseCallback> InternalGetLicenseCallbacks = new List<Internal.GetLicenseCallback>();
        internal static readonly List<Internal.StatusCallback> InternalStatusCallbacks = new List<Internal.StatusCallback>();
        internal static readonly List<Internal.QueryRuntimeModeCallback> InternalQueryRunTimeCallbacks = new List<Internal.QueryRuntimeModeCallback>();
        internal static readonly List<Internal.StatusCallback2> InternalStatusCallback2s = new List<Internal.StatusCallback2>();
        internal static readonly List<LicenseChecker> InternalLicenseCheckers = new List<LicenseChecker>();

        #region IL2CPP Support
        private static Internal.StatusCallback initIl2cppCallback;
        private static Internal.StatusCallback shutdownIl2cppCallback;
        private static Internal.QueryRuntimeModeCallback queryRuntimeModeIl2cppCallback;
        #endregion

        private static readonly string VERSION = "1.7.3.4";

        private static string _appId = "";
        private static string _appKey = "";

        public static void GetLicense(
                LicenseChecker checker,
                string appId,
                string appKey)
        {
            if (checker == null || string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appKey))
            {
                throw new InvalidOperationException("checker == null || string.IsNullOrEmpty(appId) || string.IsNullOrEmpty(appKey)");
            }

            _appId = appId;
            _appKey = appKey;

#if !UNITY_ANDROID
            InternalLicenseCheckers.Add(checker);
            if (IntPtr.Size == 8)
            {
                Internal.Api.GetLicense_64(GetLicenseHandler, _appId, _appKey);
            }
            else
            {
                Internal.Api.GetLicense(GetLicenseHandler, _appId, _appKey);
            }
            
#elif UNITY_ANDROID
            Internal.Api.GetLicense(checker, _appId, _appKey);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void InitIl2cppCallback(int errorCode) { initIl2cppCallback(errorCode); }

        public static int Init(
                StatusCallback callback,
                string appId)
        {
            if (callback == null || string.IsNullOrEmpty(appId))
            {
                throw new InvalidOperationException("callback == null || string.IsNullOrEmpty(appId)");
            }

            initIl2cppCallback = new Internal.StatusCallback(callback);
            InternalStatusCallbacks.Add(InitIl2cppCallback);
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return Internal.Api.Init_64(InitIl2cppCallback, appId);
            }
            else
            {
                return Internal.Api.Init(InitIl2cppCallback, appId);
            }
#else
            return Internal.Api.Init(InitIl2cppCallback, appId);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void ShutdownIl2cppCallback(int errorCode) { shutdownIl2cppCallback(errorCode); }

        public static int Shutdown(StatusCallback callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            shutdownIl2cppCallback = new Internal.StatusCallback(callback);
            InternalStatusCallbacks.Add(ShutdownIl2cppCallback);
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return Internal.Api.Shutdown_64(ShutdownIl2cppCallback);
            }
            else
            {
                return Internal.Api.Shutdown(ShutdownIl2cppCallback);
            }
#else
            return Internal.Api.Shutdown(ShutdownIl2cppCallback);
#endif
        }

        public static string Version()
        {
            var nativeVersion = "";
#if !UNITY_ANDROID
            try
            {
                if(IntPtr.Size == 8)
                {
                    nativeVersion += Marshal.PtrToStringAnsi(Internal.Api.Version_64());
                }
                else
                {
                    nativeVersion += Marshal.PtrToStringAnsi(Internal.Api.Version());
                }
            }
            catch (Exception)
            {
                Logger.Log("Can not load version from native library");
            }
#else
            nativeVersion = Internal.Api.Version();
#endif
            return "C# version: " + VERSION + ", Native version: " + nativeVersion;
        }

#if !UNITY_ANDROID
        [MonoPInvokeCallback(typeof(Internal.QueryRuntimeModeCallback))]
        private static void QueryRuntimeModeIl2cppCallback(int errorCode, int mode) { queryRuntimeModeIl2cppCallback(errorCode, mode); }

        public static void QueryRuntimeMode(QueryRuntimeModeCallback callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            queryRuntimeModeIl2cppCallback = new Internal.QueryRuntimeModeCallback(callback);
            InternalQueryRunTimeCallbacks.Add(QueryRuntimeModeIl2cppCallback);
            if(IntPtr.Size == 8)
            {
                Internal.Api.QueryRuntimeMode_64(QueryRuntimeModeIl2cppCallback);
            }
            else
            {
                Internal.Api.QueryRuntimeMode(QueryRuntimeModeIl2cppCallback);
            }
        }

        /*
         * Responsed license JSON format:
         * {
         *   "issueTime": 1442301893123, // epoch time in milliseconds, Long
         *   "expirationTime": 1442451893123, // epoch time in milliseconds, Long
         *   "latestVersion": 1001, // versionId, Integer
         *   "updateRequired": true // Boolean
         * }
         */
        [MonoPInvokeCallback(typeof(Internal.GetLicenseCallback))]
        private static void GetLicenseHandler(
                [MarshalAs(UnmanagedType.LPStr)] string message,
                [MarshalAs(UnmanagedType.LPStr)] string signature)
        {
            // Logger.Log("Raw Message: " + message);
            // Logger.Log("Raw Signature: " + signature);

            var isVerified = !string.IsNullOrEmpty(message);
            if (!isVerified)
            {
                for (var i = InternalLicenseCheckers.Count - 1; i >= 0; i--)
                {
                    var checker = InternalLicenseCheckers[i];
                    checker.OnFailure(90003, "License message is empty");
                    InternalLicenseCheckers.Remove(checker);
                }
                return;
            }

            isVerified = !string.IsNullOrEmpty(signature);
            if (!isVerified) // signature is empty - error code mode
            {
                var jsonData = JsonMapper.ToObject(message);
                var errorCode = 99999;
                var errorMessage = "";

                try
                {
                    errorCode = int.Parse((string)jsonData["code"]);
                }
                catch
                {
                    // ignored
                }
                try
                {
                    errorMessage = (string)jsonData["message"];
                }
                catch
                {
                    // ignored
                }

                for (var i = InternalLicenseCheckers.Count - 1; i >= 0; i--)
                {
                    var checker = InternalLicenseCheckers[i];
                    checker.OnFailure(errorCode, errorMessage);
                    InternalLicenseCheckers.Remove(checker);
                }
                return;
            }

            isVerified = VerifyMessage(_appId, _appKey, message, signature);
            if (!isVerified)
            {
                for (var i = InternalLicenseCheckers.Count - 1; i >= 0; i--)
                {
                    var checker = InternalLicenseCheckers[i];
                    checker.OnFailure(90001, "License verification failed");
                    InternalLicenseCheckers.Remove(checker);
                }
                return;
            }

            var decodedLicense = Encoding.UTF8.GetString(
                    Convert.FromBase64String(
                            message.Substring(message.IndexOf("\n", StringComparison.Ordinal) + 1)
                    )
            );
            var jsonData2 = JsonMapper.ToObject(decodedLicense);
            Logger.Log("License: " + decodedLicense);

            var issueTime = -1L;
            var expirationTime = -1L;
            var latestVersion = -1;
            var updateRequired = false;

            try
            {
                issueTime = (long)jsonData2["issueTime"];
            }
            catch
            {
                // ignored
            }
            try
            {
                expirationTime = (long)jsonData2["expirationTime"];
            }
            catch
            {
                // ignored
            }
            try
            {
                latestVersion = (int)jsonData2["latestVersion"];
            }
            catch
            {
                // ignored
            }
            try
            {
                updateRequired = (bool)jsonData2["updateRequired"];
            }
            catch
            {
                // ignored
            }

            for (var i = InternalLicenseCheckers.Count - 1; i >= 0; i--)
            {
                var checker = InternalLicenseCheckers[i];
                checker.OnSuccess(issueTime, expirationTime, latestVersion, updateRequired);
                InternalLicenseCheckers.Remove(checker);
            }
        }

        private static bool VerifyMessage(
                string appId,
                string appKey,
                string message,
                string signature)
        {
            try
            {
                var provider = PEMKeyLoader.CryptoServiceProviderFromPublicKeyInfo(appKey);
                var decodedSignature = Convert.FromBase64String(signature);
                var sha = new SHA1Managed();
                var data = Encoding.UTF8.GetBytes(appId + "\n" + message);

                return provider.VerifyData(data, sha, decodedSignature);
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString());
            }
            return false;
        }
#endif

        public abstract class LicenseChecker
        {
            public abstract void OnSuccess(
                    long issueTime,
                    long expirationTime,
                    int latestVersion,
                    bool updateRequired
            );
            public abstract void OnFailure(
                    int errorCode,
                    string errorMessage
            );
        }
    }

    public partial class User
    {
#region IL2CPP Support
        private static Internal.StatusCallback isReadyIl2cppCallback;
#endregion

#if !UNITY_ANDROID
        private const int MaxIdLength = 256;
        private const int MaxNameLength = 256;
        private const int MaxUrlLength = 512;
#endif

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void IsReadyIl2cppCallback(int errorCode) { isReadyIl2cppCallback(errorCode); }

        public static int IsReady(StatusCallback callback)
		{
			if (callback == null)
			{
				throw new InvalidOperationException("callback == null");
			}

            isReadyIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(IsReadyIl2cppCallback);
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return Internal.User.IsReady_64(IsReadyIl2cppCallback);
            }
            else
            {
                return Internal.User.IsReady(IsReadyIl2cppCallback);
            }
#else
            return Internal.User.IsReady(IsReadyIl2cppCallback);
#endif
        }

        public static string GetUserId()
        {
#if !UNITY_ANDROID
            var userId = new StringBuilder(MaxIdLength);
            if(IntPtr.Size == 8)
            {
                Internal.User.GetUserID_64(userId, MaxIdLength);
            }
            else
            {
                Internal.User.GetUserID(userId, MaxIdLength);
            }
            return userId.ToString();
#else
            return Internal.User.GetUserId().ToString();
#endif
        }

        public static string GetUserName()
        {
#if !UNITY_ANDROID
            var userName = new StringBuilder(MaxNameLength);
            if(IntPtr.Size == 8)
            {
                Internal.User.GetUserName_64(userName, MaxNameLength);
            }
            else
            {
                Internal.User.GetUserName(userName, MaxNameLength);
            }
            return userName.ToString();
#else
            return Internal.User.GetUserName().ToString();
#endif
        }

        public static string GetUserAvatarUrl()
        {
#if !UNITY_ANDROID
            var userAvatarUrl = new StringBuilder(MaxUrlLength);
            if(IntPtr.Size == 8)
            {
                Internal.User.GetUserAvatarUrl_64(userAvatarUrl, MaxUrlLength);
            }
            else
            {
                Internal.User.GetUserAvatarUrl(userAvatarUrl, MaxUrlLength);
            }
            return userAvatarUrl.ToString();
#else
            return Internal.User.GetUserAvatarUrl().ToString();
#endif
        }

    }

    public partial class UserStats
    {
#region IL2CPP Support
        private static Internal.StatusCallback isReadyIl2cppCallback;
        private static Internal.StatusCallback downloadStatsIl2cppCallback;
        private static Internal.StatusCallback uploadStatsIl2cppCallback;
        private static Internal.StatusCallback downloadLeaderboardScoresIl2cppCallback;
        private static Internal.StatusCallback uploadLeaderboardScoreIl2cppCallback;
#endregion

        public enum LeaderBoardRequestType
        {
            GlobalData = Internal.ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal /* 0 */,
            GlobalDataAroundUser = Internal.ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser /* 1 */,
            LocalData = Internal.ELeaderboardDataRequest.k_ELeaderboardDataRequestLocal /* 2 */,
            LocalDataAroundUser = Internal.ELeaderboardDataRequest.k_ELeaderboardDataRequestLocaleAroundUser /* 3 */,
        }

        public enum LeaderBoardTimeRange
        {
            AllTime = Internal.ELeaderboardDataTimeRange.k_ELeaderboardDataScropeAllTime /* 0 */,
            Daily = Internal.ELeaderboardDataTimeRange.k_ELeaderboardDataScropeDaily /* 1 */,
            Weekly = Internal.ELeaderboardDataTimeRange.k_ELeaderboardDataScropeWeekly /* 2 */,
            Monthly = Internal.ELeaderboardDataTimeRange.k_ELeaderboardDataScropeMonthly /* 3 */,
        }

        public enum LeaderBoardSortMethod
        {
            None = Internal.ELeaderboardSortMethod.k_ELeaderboardSortMethodNone /* 0 */,
            Ascending = Internal.ELeaderboardSortMethod.k_ELeaderboardSortMethodAscending /* 1 */,
            Descending = Internal.ELeaderboardSortMethod.k_ELeaderboardSortMethodDescending /* 2 */,
        }

        public enum LeaderBoardDiaplayType
        {
            None = Internal.ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNone /* 0 */,
            Numeric = Internal.ELeaderboardDisplayType.k_ELeaderboardDisplayTypeNumeric /* 1 */,
            TimeSeconds = Internal.ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeSeconds /* 2 */,
            TimeMilliSeconds = Internal.ELeaderboardDisplayType.k_ELeaderboardDisplayTypeTimeMilliSeconds /* 3 */,
        }

        public enum LeaderBoardScoreMethod
        {
            None = Internal.ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodNone /* 0 */,
            KeepBest = Internal.ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest /* 1 */,
            ForceUpdate = Internal.ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodForceUpdate /* 2 */,
        }

        public enum AchievementDisplayAttribute
        {
          Name = Internal.EAchievementDisplayAttribute.k_EAchievementDisplayAttributeName /* 0 */,
          Desc = Internal.EAchievementDisplayAttribute.k_EAchievementDisplayAttributeDesc /* 1 */,
          Hidden = Internal.EAchievementDisplayAttribute.k_EAchievementDisplayAttributeHidden /* 2 */,
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void IsReadyIl2cppCallback(int errorCode) { isReadyIl2cppCallback(errorCode); }

        public static int IsReady(StatusCallback callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            isReadyIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(IsReadyIl2cppCallback);
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return Internal.UserStats.IsReady_64(IsReadyIl2cppCallback);
            }
            else
            {
                return Internal.UserStats.IsReady(IsReadyIl2cppCallback);
            }
#else
            return Internal.UserStats.IsReady(IsReadyIl2cppCallback);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void DownloadStatsIl2cppCallback(int errorCode) { downloadStatsIl2cppCallback(errorCode); }

        public static int DownloadStats(StatusCallback callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            downloadStatsIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(DownloadStatsIl2cppCallback);
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return Internal.UserStats.DownloadStats_64(DownloadStatsIl2cppCallback);
            }
            else
            {
                return Internal.UserStats.DownloadStats(DownloadStatsIl2cppCallback);
            }
#else
            return Internal.UserStats.DownloadStats(DownloadStatsIl2cppCallback);
#endif
        }

        public static int GetStat(string name, int defaultValue)
        {
#if !UNITY_ANDROID
            var result = defaultValue;
            if(IntPtr.Size == 8)
            {
                Internal.UserStats.GetStat_64(name, ref result);
            }
            else
            {
                Internal.UserStats.GetStat(name, ref result);
            }
            return result;
#else
            return Internal.UserStats.GetStat(name, defaultValue);
#endif
        }

        public static float GetStat(string name, float defaultValue)
        {
#if !UNITY_ANDROID
            var result = defaultValue;
            if(IntPtr.Size == 8)
            {
                Internal.UserStats.GetStat_64(name, ref result);
            }
            else
            {
                Internal.UserStats.GetStat(name, ref result);
            }
            return result;
#else
            return Internal.UserStats.GetStat(name, defaultValue);
#endif
        }

        public static void SetStat(string name, int value)
        {
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.UserStats.SetStat_64(name, value);
            }
            else
            {
                Internal.UserStats.SetStat(name, value);
            }
#else
            Internal.UserStats.SetStat(name, value);
#endif
        }

        public static void SetStat(string name, float value)
        {
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.UserStats.SetStat_64(name, value);
            }
            else
            {
                Internal.UserStats.SetStat(name, value);
            }
#else
            Internal.UserStats.SetStat(name, value);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void UploadStatsIl2cppCallback(int errorCode) { uploadStatsIl2cppCallback(errorCode); }

        public static int UploadStats(StatusCallback callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            uploadStatsIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(UploadStatsIl2cppCallback);
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return Internal.UserStats.UploadStats_64(UploadStatsIl2cppCallback);
            }
            else
            {
                return Internal.UserStats.UploadStats(UploadStatsIl2cppCallback);
            }
#else
            return Internal.UserStats.UploadStats(UploadStatsIl2cppCallback);
#endif
        }

        // for Achievements
        public static bool GetAchievement(string pchName)
        {
#if !UNITY_ANDROID
            var nAchieved = 0;
            if(IntPtr.Size == 8)
            {
                Internal.UserStats.GetAchievement_64(pchName, ref nAchieved);
            }
            else
            {
                Internal.UserStats.GetAchievement(pchName, ref nAchieved);
            }
            return nAchieved == 1;
#else
            return Internal.UserStats.GetAchievement(pchName);
#endif
        }

        public static int GetAchievementUnlockTime(string pchName)
        {
#if !UNITY_ANDROID
            var nUnlockTime = 0;
            if(IntPtr.Size == 8)
            {
                Internal.UserStats.GetAchievementUnlockTime_64(pchName, ref nUnlockTime);
            }
            else
            {
                Internal.UserStats.GetAchievementUnlockTime(pchName, ref nUnlockTime);
            }
            return nUnlockTime;
#else
            return Internal.UserStats.GetAchievementUnlockTime(pchName);
#endif
        }

        public static string GetAchievementIcon(string pchName)
        {
#if !UNITY_ANDROID
            //not supported yet
            return "";
#else
            return Internal.UserStats.GetAchievementIcon(pchName);
#endif
        }

        public static string GetAchievementDisplayAttribute(string pchName, AchievementDisplayAttribute attr)
        {
#if !UNITY_ANDROID
            //not supported yet
            return "";
#else
            return Internal.UserStats.GetAchievementDisplayAttribute(pchName, (Internal.EAchievementDisplayAttribute) attr);
#endif
        }

        public static string GetAchievementDisplayAttribute(string pchName, AchievementDisplayAttribute attr, Locale locale)
        {
#if !UNITY_ANDROID
            //not supported yet
            return "";
#else
            return Internal.UserStats.GetAchievementDisplayAttribute(pchName, (Internal.EAchievementDisplayAttribute)attr, (Internal.ELocale)locale);
#endif
        }

        public static int SetAchievement(string pchName)
        {
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return Internal.UserStats.SetAchievement_64(pchName);
            }
            else
            {
                return Internal.UserStats.SetAchievement(pchName);
            }
#else
            return Internal.UserStats.SetAchievement(pchName);
#endif
        }
        
        public static int ClearAchievement(string pchName)
        {
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return Internal.UserStats.ClearAchievement_64(pchName);
            }
            else
            {
                return Internal.UserStats.ClearAchievement(pchName);
            }
#else
            return Internal.UserStats.ClearAchievement(pchName);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void DownloadLeaderboardScoresIl2cppCallback(int errorCode) { downloadLeaderboardScoresIl2cppCallback(errorCode); }

        // for Leaderboards
        public static int DownloadLeaderboardScores(
                StatusCallback callback,
                string pchLeaderboardName,
                LeaderBoardRequestType eLeaderboardDataRequest,
                LeaderBoardTimeRange eLeaderboardDataTimeRange,
                int nRangeStart,
                int nRangeEnd)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            downloadLeaderboardScoresIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(DownloadLeaderboardScoresIl2cppCallback);
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return Internal.UserStats.DownloadLeaderboardScores_64(
                    DownloadLeaderboardScoresIl2cppCallback,
                    pchLeaderboardName,
                    (Internal.ELeaderboardDataRequest)eLeaderboardDataRequest,
                    (Internal.ELeaderboardDataTimeRange)eLeaderboardDataTimeRange,
                    nRangeStart,
                    nRangeEnd
                );
            }
            else
            {
                return Internal.UserStats.DownloadLeaderboardScores(
                    DownloadLeaderboardScoresIl2cppCallback,
                    pchLeaderboardName,
                    (Internal.ELeaderboardDataRequest)eLeaderboardDataRequest,
                    (Internal.ELeaderboardDataTimeRange)eLeaderboardDataTimeRange,
                    nRangeStart,
                    nRangeEnd
                );
            }
#else
            return Internal.UserStats.DownloadLeaderboardScores(
                DownloadLeaderboardScoresIl2cppCallback,
                pchLeaderboardName,
                (Internal.ELeaderboardDataRequest)eLeaderboardDataRequest,
                (Internal.ELeaderboardDataTimeRange)eLeaderboardDataTimeRange,
                nRangeStart,
                nRangeEnd
            );
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void UploadLeaderboardScoreIl2cppCallback(int errorCode) { uploadLeaderboardScoreIl2cppCallback(errorCode); }

        public static int UploadLeaderboardScore(
                StatusCallback callback,
                string pchLeaderboardName,
                int nScore)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            uploadLeaderboardScoreIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(UploadLeaderboardScoreIl2cppCallback);
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return Internal.UserStats.UploadLeaderboardScore_64(UploadLeaderboardScoreIl2cppCallback, pchLeaderboardName, nScore);
            }
            else
            {
                return Internal.UserStats.UploadLeaderboardScore(UploadLeaderboardScoreIl2cppCallback, pchLeaderboardName, nScore);
            }
#else
            return Internal.UserStats.UploadLeaderboardScore(UploadLeaderboardScoreIl2cppCallback, pchLeaderboardName, nScore);
#endif
        }

        public static Leaderboard GetLeaderboardScore(int index)
        {
#if !UNITY_ANDROID
            Internal.LeaderboardEntry_t pLeaderboardEntry;
            pLeaderboardEntry.m_nGlobalRank = 0;
            pLeaderboardEntry.m_nScore = 0;
            pLeaderboardEntry.m_pUserName = "";
            if(IntPtr.Size == 8)
            {
                Internal.UserStats.GetLeaderboardScore_64(index, ref pLeaderboardEntry);
            }
            else
            {
                Internal.UserStats.GetLeaderboardScore(index, ref pLeaderboardEntry);
            }
 
            return new Leaderboard
            {
                Rank = pLeaderboardEntry.m_nGlobalRank,
                Score = pLeaderboardEntry.m_nScore,
                UserName = pLeaderboardEntry.m_pUserName
            };

#else
            Leaderboard pLeaderboardEntry;
            pLeaderboardEntry = Internal.UserStats.GetLeaderboardScore(index);
            return pLeaderboardEntry;
#endif
        }

        public static int GetLeaderboardScoreCount()
        {
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return Internal.UserStats.GetLeaderboardScoreCount_64();
            }
            else
            {
                return Internal.UserStats.GetLeaderboardScoreCount();
            }
#else 
            return Internal.UserStats.GetLeaderboardScoreCount();
#endif
        }

        public static LeaderBoardSortMethod GetLeaderboardSortMethod()
        {
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return (LeaderBoardSortMethod)Internal.UserStats.GetLeaderboardSortMethod_64();
            }
            else
            {
                return (LeaderBoardSortMethod)Internal.UserStats.GetLeaderboardSortMethod();
            }
#else
            return (LeaderBoardSortMethod)Internal.UserStats.GetLeaderboardSortMethod();
#endif
        }

        public static LeaderBoardDiaplayType GetLeaderboardDisplayType()
        {
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                return (LeaderBoardDiaplayType)Internal.UserStats.GetLeaderboardDisplayType_64();
            }
            else
            {
                return (LeaderBoardDiaplayType)Internal.UserStats.GetLeaderboardDisplayType();
            }
#else
            return (LeaderBoardDiaplayType)Internal.UserStats.GetLeaderboardDisplayType();
#endif
        }
    }

    public partial class ArcadeLeaderboard
    {
#if !UNITY_ANDROID
#region IL2CPP Support
        private static Internal.StatusCallback isReadyIl2cppCallback;
        private static Internal.StatusCallback downloadLeaderboardScoresIl2cppCallback;
        private static Internal.StatusCallback uploadLeaderboardScoreIl2cppCallback;
#endregion

        public enum LeaderboardTimeRange
        {
            AllTime = Internal.ELeaderboardDataTimeRange.k_ELeaderboardDataScropeAllTime /* 0 */,
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void IsReadyIl2cppCallback(int errorCode) { isReadyIl2cppCallback(errorCode); }

        public static void IsReady(StatusCallback callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            isReadyIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(IsReadyIl2cppCallback);

            if(IntPtr.Size == 8)
            {
                Internal.ArcadeLeaderboard.IsReady_64(IsReadyIl2cppCallback);
            }
            else
            {
                Internal.ArcadeLeaderboard.IsReady(IsReadyIl2cppCallback);
            }
            
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void DownloadLeaderboardScoresIl2cppCallback(int errorCode) { downloadLeaderboardScoresIl2cppCallback(errorCode); }

        public static void DownloadLeaderboardScores(
                StatusCallback callback,
                string pchLeaderboardName,
                LeaderboardTimeRange eLeaderboardDataTimeRange,
                int nCount)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            downloadLeaderboardScoresIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(DownloadLeaderboardScoresIl2cppCallback);
            eLeaderboardDataTimeRange = LeaderboardTimeRange.AllTime;

            if(IntPtr.Size == 8)
            {
                Internal.ArcadeLeaderboard.DownloadLeaderboardScores_64(
                    DownloadLeaderboardScoresIl2cppCallback,
                    pchLeaderboardName,
                    (Internal.ELeaderboardDataTimeRange)eLeaderboardDataTimeRange,
                    nCount
                );
            }
            else
            {
                Internal.ArcadeLeaderboard.DownloadLeaderboardScores(
                    DownloadLeaderboardScoresIl2cppCallback,
                    pchLeaderboardName,
                    (Internal.ELeaderboardDataTimeRange)eLeaderboardDataTimeRange,
                    nCount
                );
            }
            
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void UploadLeaderboardScoreIl2cppCallback(int errorCode) { uploadLeaderboardScoreIl2cppCallback(errorCode); }

        public static void UploadLeaderboardScore(
                StatusCallback callback,
                string pchLeaderboardName,
                string pchUserName,
                int nScore)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            uploadLeaderboardScoreIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(UploadLeaderboardScoreIl2cppCallback);

            if(IntPtr.Size == 8)
            {
                Internal.ArcadeLeaderboard.UploadLeaderboardScore_64(
                    UploadLeaderboardScoreIl2cppCallback,
                    pchLeaderboardName,
                    pchUserName,
                    nScore
                );
            }
            else
            {
                Internal.ArcadeLeaderboard.UploadLeaderboardScore(
                    UploadLeaderboardScoreIl2cppCallback,
                    pchLeaderboardName,
                    pchUserName,
                    nScore
                );
            }

        }

        public static Leaderboard GetLeaderboardScore(int index)
        {
            Internal.LeaderboardEntry_t pLeaderboardEntry;
            pLeaderboardEntry.m_nGlobalRank = 0;
            pLeaderboardEntry.m_nScore = 0;
            pLeaderboardEntry.m_pUserName = "";
            if(IntPtr.Size == 8)
            {
                Internal.ArcadeLeaderboard.GetLeaderboardScore_64(index, ref pLeaderboardEntry);
            }
            else
            {
                Internal.ArcadeLeaderboard.GetLeaderboardScore(index, ref pLeaderboardEntry);
            }
            return new Leaderboard
            {
                Rank = pLeaderboardEntry.m_nGlobalRank,
                Score = pLeaderboardEntry.m_nScore,
                UserName = pLeaderboardEntry.m_pUserName
            };
        }

        public static int GetLeaderboardScoreCount()
        {
            if(IntPtr.Size == 8)
            {
                return Internal.ArcadeLeaderboard.GetLeaderboardScoreCount_64();
            }
            else
            {
                return Internal.ArcadeLeaderboard.GetLeaderboardScoreCount();
            }
        }

        public static int GetLeaderboardUserRank()
        {
            if(IntPtr.Size == 8)
            {
                return Internal.ArcadeLeaderboard.GetLeaderboardUserRank_64();
            }
            else
            {
                return Internal.ArcadeLeaderboard.GetLeaderboardUserRank();
            }
        }

        public static int GetLeaderboardUserScore()
        {
            if(IntPtr.Size == 8)
            {
                return Internal.ArcadeLeaderboard.GetLeaderboardUserScore_64();
            }
            else
            {
                return Internal.ArcadeLeaderboard.GetLeaderboardUserScore();
            }
        }
#endif
    }

    public partial class IAPurchase
    {
#region IL2CPP Support
        private static Internal.IAPurchaseCallback isReadyIl2cppCallback;
        private static Internal.IAPurchaseCallback request01Il2cppCallback;
        private static Internal.IAPurchaseCallback request02Il2cppCallback;
        private static Internal.IAPurchaseCallback purchaseIl2cppCallback;
        private static Internal.IAPurchaseCallback query01Il2cppCallback;
        private static Internal.IAPurchaseCallback query02Il2cppCallback;
        private static Internal.IAPurchaseCallback getBalanceIl2cppCallback;
        private static Internal.IAPurchaseCallback requestSubscriptionIl2cppCallback;
        private static Internal.IAPurchaseCallback requestSubscriptionWithPlanIDIl2cppCallback;
        private static Internal.IAPurchaseCallback subscribeIl2cppCallback;
        private static Internal.IAPurchaseCallback querySubscriptionIl2cppCallback;
        private static Internal.IAPurchaseCallback querySubscriptionListIl2cppCallback;
        private static Internal.IAPurchaseCallback cancelSubscriptionIl2cppCallback;
#endregion

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void IsReadyIl2cppCallback(int errorCode, string message) { isReadyIl2cppCallback(errorCode, message); }

        public static void IsReady(IAPurchaseListener listener, string pchAppKey)
        {
            isReadyIl2cppCallback = new IAPHandler(listener).getIsReadyHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.IsReady_64(IsReadyIl2cppCallback, pchAppKey);
            }
            else
            {
                Internal.IAPurchase.IsReady(IsReadyIl2cppCallback, pchAppKey);
            }
#else
            Internal.IAPurchase.IsReady(IsReadyIl2cppCallback, pchAppKey);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void Request01Il2cppCallback(int errorCode, string message) { request01Il2cppCallback(errorCode, message); }

        public static void Request(IAPurchaseListener listener, string pchPrice)
        {
            request01Il2cppCallback = new IAPHandler(listener).getRequestHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.Request_64(Request01Il2cppCallback, pchPrice);
            }
            else
            {
                Internal.IAPurchase.Request(Request01Il2cppCallback, pchPrice);
            }
#else
            Internal.IAPurchase.Request(Request01Il2cppCallback, pchPrice);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void Request02Il2cppCallback(int errorCode, string message) { request02Il2cppCallback(errorCode, message); }

        public static void Request(IAPurchaseListener listener, string pchPrice, string pchUserData)
        {
            request02Il2cppCallback = new IAPHandler(listener).getRequestHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.Request_64(Request02Il2cppCallback, pchPrice, pchUserData);
            }
            else
            {
                Internal.IAPurchase.Request(Request02Il2cppCallback, pchPrice, pchUserData);
            }
#else
            Internal.IAPurchase.Request(Request02Il2cppCallback, pchPrice, pchUserData);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void PurchaseIl2cppCallback(int errorCode, string message) { purchaseIl2cppCallback(errorCode, message); }

        public static void Purchase(IAPurchaseListener listener, string pchPurchaseId)
        {
            purchaseIl2cppCallback = new IAPHandler(listener).getPurchaseHandler();
#if !UNITY_ANDROID
            if (IntPtr.Size == 8)
            {
                Internal.IAPurchase.Purchase_64(PurchaseIl2cppCallback, pchPurchaseId);
            }
            else
            {
                Internal.IAPurchase.Purchase(PurchaseIl2cppCallback, pchPurchaseId);
            }
#else
            Internal.IAPurchase.Purchase(PurchaseIl2cppCallback, pchPurchaseId);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void Query01Il2cppCallback(int errorCode, string message) { query01Il2cppCallback(errorCode, message); }

        public static void Query(IAPurchaseListener listener, string pchPurchaseId)
        {
            query01Il2cppCallback = new IAPHandler(listener).getQueryHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.Query_64(Query01Il2cppCallback, pchPurchaseId);
            }
            else
            {
                Internal.IAPurchase.Query(Query01Il2cppCallback, pchPurchaseId);
            }
#else
            Internal.IAPurchase.Query(Query01Il2cppCallback, pchPurchaseId);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void Query02Il2cppCallback(int errorCode, string message) { query02Il2cppCallback(errorCode, message); }

        public static void Query(IAPurchaseListener listener)
        {
            query02Il2cppCallback = new IAPHandler(listener).getQueryListHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.Query_64(Query02Il2cppCallback);
            }
            else
            {
                Internal.IAPurchase.Query(Query02Il2cppCallback);
            }
#else
            Internal.IAPurchase.Query(Query02Il2cppCallback);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void GetBalanceIl2cppCallback(int errorCode, string message) { getBalanceIl2cppCallback(errorCode, message); }

        public static void GetBalance(IAPurchaseListener listener)
        {
            getBalanceIl2cppCallback = new IAPHandler(listener).getBalanceHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.GetBalance_64(GetBalanceIl2cppCallback);
            }
            else
            {
                Internal.IAPurchase.GetBalance(GetBalanceIl2cppCallback);
            }
#else
            Internal.IAPurchase.GetBalance(GetBalanceIl2cppCallback);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void RequestSubscriptionIl2cppCallback(int errorCode, string message) { requestSubscriptionIl2cppCallback(errorCode, message); }

        public static void RequestSubscription(
                IAPurchaseListener listener,
                string pchPrice,
                string pchFreeTrialType,
                int nFreeTrialValue,
                string pchChargePeriodType,
                int nChargePeriodValue,
                int nNumberOfChargePeriod,
                string pchPlanId)
        {
            requestSubscriptionIl2cppCallback = new IAPHandler(listener).getRequestSubscriptionHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.RequestSubscription_64(
                    RequestSubscriptionIl2cppCallback,
                    pchPrice,
                    pchFreeTrialType,
                    nFreeTrialValue,
                    pchChargePeriodType,
                    nChargePeriodValue,
                    nNumberOfChargePeriod,
                    pchPlanId
                );
            }
            else
            {
                Internal.IAPurchase.RequestSubscription(
                    RequestSubscriptionIl2cppCallback,
                    pchPrice,
                    pchFreeTrialType,
                    nFreeTrialValue,
                    pchChargePeriodType,
                    nChargePeriodValue,
                    nNumberOfChargePeriod,
                    pchPlanId
                );
            }
#else
            Internal.IAPurchase.RequestSubscription(
                RequestSubscriptionIl2cppCallback,
                pchPrice,
                pchFreeTrialType,
                nFreeTrialValue,
                pchChargePeriodType,
                nChargePeriodValue,
                nNumberOfChargePeriod,
                pchPlanId
            );
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void RequestSubscriptionWithPlanIDIl2cppCallback(int errorCode, string message) { requestSubscriptionWithPlanIDIl2cppCallback(errorCode, message); }

        public static void RequestSubscriptionWithPlanID(IAPurchaseListener listener, string pchPlanId)
        {
            requestSubscriptionWithPlanIDIl2cppCallback = new IAPHandler(listener).getRequestSubscriptionWithPlanIDHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.RequestSubscriptionWithPlanID_64(RequestSubscriptionWithPlanIDIl2cppCallback, pchPlanId);
            }
            else
            {
                Internal.IAPurchase.RequestSubscriptionWithPlanID(RequestSubscriptionWithPlanIDIl2cppCallback, pchPlanId);
            }
#else
            Internal.IAPurchase.RequestSubscriptionWithPlanID(RequestSubscriptionWithPlanIDIl2cppCallback, pchPlanId);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void SubscribeIl2cppCallback(int errorCode, string message) { subscribeIl2cppCallback(errorCode, message); }

        public static void Subscribe(IAPurchaseListener listener, string pchSubscriptionId)
        {
            subscribeIl2cppCallback = new IAPHandler(listener).getSubscribeHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.Subscribe_64(SubscribeIl2cppCallback, pchSubscriptionId);
            }
            else
            {
                Internal.IAPurchase.Subscribe(SubscribeIl2cppCallback, pchSubscriptionId);
            }
#else
            Internal.IAPurchase.Subscribe(SubscribeIl2cppCallback, pchSubscriptionId);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void QuerySubscriptionIl2cppCallback(int errorCode, string message) { querySubscriptionIl2cppCallback(errorCode, message); }

        public static void QuerySubscription(IAPurchaseListener listener, string pchSubscriptionId)
        {
            querySubscriptionIl2cppCallback = new IAPHandler(listener).getQuerySubscriptionHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.QuerySubscription_64(QuerySubscriptionIl2cppCallback, pchSubscriptionId);
            }
            else
            {
                Internal.IAPurchase.QuerySubscription(QuerySubscriptionIl2cppCallback, pchSubscriptionId);
            }
#else
            Internal.IAPurchase.QuerySubscription(QuerySubscriptionIl2cppCallback, pchSubscriptionId);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void QuerySubscriptionListIl2cppCallback(int errorCode, string message) { querySubscriptionListIl2cppCallback(errorCode, message); }

        public static void QuerySubscriptionList(IAPurchaseListener listener)
        {
            querySubscriptionListIl2cppCallback = new IAPHandler(listener).getQuerySubscriptionListHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.QuerySubscriptionList_64(QuerySubscriptionListIl2cppCallback);
            }
            else
            {
                Internal.IAPurchase.QuerySubscriptionList(QuerySubscriptionListIl2cppCallback);
            }
#else
            Internal.IAPurchase.QuerySubscriptionList(QuerySubscriptionListIl2cppCallback);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.IAPurchaseCallback))]
        private static void CancelSubscriptionIl2cppCallback(int errorCode, string message) { cancelSubscriptionIl2cppCallback(errorCode, message); }

        public static void CancelSubscription(IAPurchaseListener listener, string pchSubscriptionId)
        {
            cancelSubscriptionIl2cppCallback = new IAPHandler(listener).getCancelSubscriptionHandler();
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.IAPurchase.CancelSubscription_64(CancelSubscriptionIl2cppCallback, pchSubscriptionId);
            }
            else
            {
                Internal.IAPurchase.CancelSubscription(CancelSubscriptionIl2cppCallback, pchSubscriptionId);
            }
#else
            Internal.IAPurchase.CancelSubscription(CancelSubscriptionIl2cppCallback, pchSubscriptionId);
#endif
        }

        private partial class IAPHandler : BaseHandler
        {
            static IAPurchaseListener listener;

            public IAPHandler(IAPurchaseListener cb)
            {
                listener = cb;
            }

#region IsReady

            public Internal.IAPurchaseCallback getIsReadyHandler()
            {
                return IsReadyHandler;
            }

            /*
             * TODO
             * 
             * Responsed JSON format:
             * {
             *   "statusCode": 500,     // status code, Integer
             *   "currencyName": "",     // user's setting currencyName
             *   "message": "",         // error message information, String
             * }
             * 
             */
            protected override void IsReadyHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[IsReadyHandler] message=" + message);
                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                string currencyName = "";
                string errMessage = "";
                if (code == 0)
                {
                    try
                    {
                        statusCode = (int)jsonData["statusCode"];
                        errMessage = (string)jsonData["message"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[IsReadyHandler] statusCode, message ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[IsReadyHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                    if (statusCode == 0)
                    {
                        try
                        {
                            currencyName = (string)jsonData["currencyName"];
                        }
                        catch (Exception ex)
                        {
                            Viveport.Core.Logger.Log("[IsReadyHandler] currencyName ex=" + ex);
                        }
                        Viveport.Core.Logger.Log("[IsReadyHandler] currencyName=" + currencyName);
                    }
                }

                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0)
                        {
                            listener.OnSuccess(currencyName);
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }
                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
            }

#endregion IsReady
#region Request

            public Internal.IAPurchaseCallback getRequestHandler()
            {
                return RequestHandler;
            }

            /*
             * TODO
             * 
             * Responsed JSON format:
             * {
             *   "statusCode": 500,     // status code, Integer
             *   "purchase_id": "",     // specific purchase id, String
             *   "message": "",         // error message information, String
             * }
             * 
             */
            protected override void RequestHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[RequestHandler] message=" + message);

                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                string purchaseId = "";
                string errMessage = "";

                if (code == 0)
                {
                    try
                    {
                        statusCode = (int)jsonData["statusCode"];
                        errMessage = (string)jsonData["message"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[RequestHandler] statusCode, message ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[RequestHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                    if (statusCode == 0)
                    {
                        try
                        {
                            purchaseId = (string)jsonData["purchase_id"];
                        }
                        catch (Exception ex)
                        {
                            Viveport.Core.Logger.Log("[RequestHandler] purchase_id ex=" + ex);
                        }
                        Viveport.Core.Logger.Log("[RequestHandler] purchaseId =" + purchaseId);
                    }
                }
                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0)
                        {
                            listener.OnRequestSuccess(purchaseId);
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }
                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
            }

#endregion Request

#region Purchase

            public Internal.IAPurchaseCallback getPurchaseHandler()
            {
                return PurchaseHandler;
            }

            /*
             * TODO
             * 
             * Responsed JSON format:
             * {
             *   "statusCode": 500,     // status code, Integer
             *   "purchase_id": "",     // specific purchase id, String
             *   "paid_timestamp": 0,   // paid_timestamp in milli seconds, Long,
             *   "message": "",         // error message information, String
             * }
             * 
             */
            protected override void PurchaseHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[PurchaseHandler] message=" + message);

                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                string purchaseId = "";
                string errMessage = "";
                long paid_timestamp = 0L;

                if (code == 0)
                {
                    try
                    {
                        statusCode = (int)jsonData["statusCode"];
                        errMessage = (string)jsonData["message"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[PurchaseHandler] statusCode, message ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[PurchaseHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                    if (statusCode == 0)
                    {
                        try
                        {
                            purchaseId = (string)jsonData["purchase_id"];
                            paid_timestamp = (long)jsonData["paid_timestamp"];
                        }
                        catch (Exception ex)
                        {
                            Viveport.Core.Logger.Log("[PurchaseHandler] purchase_id,paid_timestamp ex=" + ex);
                        }
                        Viveport.Core.Logger.Log("[PurchaseHandler] purchaseId =" + purchaseId + ",paid_timestamp=" + paid_timestamp);
                    }
                }
                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0)
                        {
                            listener.OnPurchaseSuccess(purchaseId);
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }
                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
            }

#endregion Purchase

#region Query

            public Internal.IAPurchaseCallback getQueryHandler()
            {
                return QueryHandler;
            }


            /*
             * TODO
             * 
             * Responsed JSON format:
             * 
             * {
             *   "order_id": "",                // , String
             *   "purchase_id": "string",       // , String
             *   "status": "string",            // , String
             *   "app_id": "string",            // , String
             *   "price": "string",             // , String
             *   "item_list": [
             *     {
             *       "item_id": "string",           // , String
             *       "quantity": 0,                 // , Integer
             *       "subtotal_price": "string",    // , String
             *       "category": "string",          // , String
             *       "description": "string"        // , String
             *     }
             *   ],
             *   "currency": "string",          // , String
             *   "paid_timestamp": 0,           // epoch time in milliseconds, Long
             *   "user_data": "string"          // , String
             * }
             * 
             */
            protected override void QueryHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[QueryHandler] message=" + message);
                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                string purchaseId = "";
                string errMessage = "";
                string order_id = "";
                string status = "";
                string price = "";
                string currency = "";
                long paid_timestamp = 0L;

                if (code == 0)
                {
                    try
                    {
                        statusCode = (int)jsonData["statusCode"];
                        errMessage = (string)jsonData["message"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[QueryHandler] statusCode, message ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[QueryHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                    if (statusCode == 0)
                    {
                        try
                        {
                            purchaseId = (string)jsonData["purchase_id"];
                            order_id = (string)jsonData["order_id"];
                            status = (string)jsonData["status"];
                            price = (string)jsonData["price"];
                            currency = (string)jsonData["currency"];
                            paid_timestamp = (long)jsonData["paid_timestamp"];
                        }
                        catch (Exception ex)
                        {
                            Viveport.Core.Logger.Log("[QueryHandler] purchase_id, order_id ex=" + ex);
                        }
                        Viveport.Core.Logger.Log("[QueryHandler] status =" + status + ",price=" + price + ",currency=" + currency);
                        Viveport.Core.Logger.Log("[QueryHandler] purchaseId =" + purchaseId + ",order_id=" + order_id + ",paid_timestamp=" + paid_timestamp);
                    }
                }
                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0)
                        {
                            QueryResponse response = new QueryResponse();
                            response.purchase_id = purchaseId;
                            response.order_id = order_id;
                            response.price = price;
                            response.currency = currency;
                            response.paid_timestamp = paid_timestamp;
                            response.status = status;
                            listener.OnQuerySuccess(response);
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }

                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }

                /*
                if (listener != null)
                {
                    if (code == 0)
                    {
                        //string sampleText = "{\"order_id\":\"response_order_id_000\",\"purchase_id\":null,\"status\":\"response_status_000\",\"price\":null,\"item_list\":null,\"currency\":null,\"paid_timestamp\":0,\"user_data\":null}";
                        QueryResponse response = JsonMapper.ToObject<QueryResponse>(message);
                        if (response != null && string.IsNullOrEmpty(response.message))
                        {
                            listener.OnQuerySuccess(response);
                        }
                        else
                        {
                            int statusCode = 999;
                            if (response != null && !string.IsNullOrEmpty(response.code))
                            {
                                // TODO code shoud be Integer
                                string[] codes = response.code.Split('.');
                                if (codes != null && codes.Length > 0)
                                    statusCode = Int32.Parse(codes[0]);
                            }

                            string errMessage = (response != null) ? response.message : "";
                            listener.OnFailure(statusCode, errMessage);
                        }
                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
                */
            }

#endregion Query

#region QueryList

            public Internal.IAPurchaseCallback getQueryListHandler()
            {
                return QueryListHandler;
            }

            protected override void QueryListHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[QueryListHandler] message=" + message);
                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                int total = 0;
                int from = 0;
                int to = 0;
                List<QueryResponse2> purchaseList = new List<QueryResponse2>();
                string errMessage = "";

                if (code == 0)
                {

                    try
                    {
                        statusCode = (int)jsonData["statusCode"];
                        errMessage = (string)jsonData["message"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[QueryListHandler] statusCode, message ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[QueryListHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                    if (statusCode == 0)
                    {
                        try
                        {
                            JsonData purchaseData = JsonMapper.ToObject(errMessage);
                            total = (int)purchaseData["total"];
                            from = (int)purchaseData["from"];
                            to = (int)purchaseData["to"];
                            JsonData purchases = (JsonData)purchaseData["purchases"];
                            bool isArray = purchases.IsArray;
                            foreach (JsonData jd in purchases)
                            {
                                QueryResponse2 q = new QueryResponse2();
                                var dic = (jd as IDictionary);
                                q.app_id = dic.Contains("app_id") ? (string)jd["app_id"] : "";
                                q.currency = dic.Contains("currency") ? (string)jd["currency"] : "";
                                q.purchase_id = dic.Contains("purchase_id") ? (string)jd["purchase_id"] : "";
                                q.order_id = dic.Contains("order_id") ? (string)jd["order_id"] : "";
                                q.price = dic.Contains("price") ? (string)jd["price"] : "";
                                q.user_data = dic.Contains("user_data") ? (string)jd["user_data"] : "";
                                if (dic.Contains("paid_timestamp"))
                                {
                                    if (jd["paid_timestamp"].IsLong)
                                    {
                                        q.paid_timestamp = (long)jd["paid_timestamp"];
                                    }
                                    else if (jd["paid_timestamp"].IsInt)
                                    {
                                        q.paid_timestamp = (int)jd["paid_timestamp"];
                                    }
                                }
                                purchaseList.Add(q);
                            }
                        }
                        catch (Exception ex)
                        {
                            Viveport.Core.Logger.Log("[QueryListHandler] purchase_id, order_id ex=" + ex);
                        }
                    }
                }
                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0)
                        {
                            QueryListResponse response = new QueryListResponse();
                            response.total = total;
                            response.from = from;
                            response.to = to;
                            response.purchaseList = purchaseList;
                            listener.OnQuerySuccess(response);
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }

                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
            }

#endregion QueryList

#region GetBalance

            public Internal.IAPurchaseCallback getBalanceHandler()
            {
                return BalanceHandler;
            }
            /*
             * TODO
             * 
             * Responsed JSON format:
             * {
             *   "statusCode": 500,     // status code, Integer
             *   "currencyName": "USD",     // currency name, String
             *   "balance": "100",     // balance, String
             *   "message": "",         // error message information, String
             * }
             * 
             */
            protected override void BalanceHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[BalanceHandler] code=" + code + ",message= " + message);
                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                string currencyName = "";
                string balance = "";
                string errMessage = "";

                if (code == 0)
                {
                    try
                    {
                        statusCode = (int)jsonData["statusCode"];
                        errMessage = (string)jsonData["message"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[BalanceHandler] statusCode, message ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[BalanceHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                    if (statusCode == 0)
                    {
                        try
                        {
                            currencyName = (string)jsonData["currencyName"];
                            balance = (string)jsonData["balance"];
                        }
                        catch (Exception ex)
                        {
                            Viveport.Core.Logger.Log("[BalanceHandler] currencyName, balance ex=" + ex);
                        }
                        Viveport.Core.Logger.Log("[BalanceHandler] currencyName=" + currencyName + ",balance=" + balance);
                    }
                }

                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0)
                        {
                            listener.OnBalanceSuccess(balance);
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }
                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
            }
#endregion GetBalance
#region RequestSubscription

            public Internal.IAPurchaseCallback getRequestSubscriptionHandler()
            {
                return RequestSubscriptionHandler;
            }

            /*
             * 
             * Responsed JSON format:
             * 
             *  {
             *      "statusCode",  0,
             *      "subscription_id": "subscription_id_string",
             *      "message", "success"
             *  }
             * 
             */
            protected override void RequestSubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[RequestSubscriptionHandler] message=" + message);
                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                string subscription_id = "";
                string errMessage = "";

                try
                {
                    statusCode = (int)jsonData["statusCode"];
                    errMessage = (string)jsonData["message"];
                }
                catch (Exception ex)
                {
                    Viveport.Core.Logger.Log("[RequestSubscriptionHandler] statusCode, message ex=" + ex);
                }
                Viveport.Core.Logger.Log("[RequestSubscriptionHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                if (statusCode == 0)
                {
                    try
                    {
                        subscription_id = (string)jsonData["subscription_id"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[RequestSubscriptionHandler] subscription_id ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[RequestSubscriptionHandler] subscription_id =" + subscription_id);
                }
                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0)
                        {
                            listener.OnRequestSubscriptionSuccess(subscription_id);
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }
                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
            }

#endregion RequestSubscription
#region RequestSubscriptionWithPlanID

            public Internal.IAPurchaseCallback getRequestSubscriptionWithPlanIDHandler()
            {
                return RequestSubscriptionWithPlanIDHandler;
            }

            /*
             * 
             * Responsed JSON format:
             * 
             *  {
             *      "statusCode",  0,
             *      "subscription_id": "subscription_id_string",
             *      "message", "success"
             *  }
             * 
             */
            protected override void RequestSubscriptionWithPlanIDHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[RequestSubscriptionWithPlanIDHandler] message=" + message);
                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                string subscription_id = "";
                string errMessage = "";

                try
                {
                    statusCode = (int)jsonData["statusCode"];
                    errMessage = (string)jsonData["message"];
                }
                catch (Exception ex)
                {
                    Viveport.Core.Logger.Log("[RequestSubscriptionWithPlanIDHandler] statusCode, message ex=" + ex);
                }
                Viveport.Core.Logger.Log("[RequestSubscriptionWithPlanIDHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                if (statusCode == 0)
                {
                    try
                    {
                        subscription_id = (string)jsonData["subscription_id"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[RequestSubscriptionWithPlanIDHandler] subscription_id ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[RequestSubscriptionWithPlanIDHandler] subscription_id =" + subscription_id);
                }
                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0)
                        {
                            listener.OnRequestSubscriptionWithPlanIDSuccess(subscription_id);
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }
                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
            }

#endregion RequestSubscriptionWithPlanID
#region Subscribe

            public Internal.IAPurchaseCallback getSubscribeHandler()
            {
                return SubscribeHandler;
            }

            /*
             * TODO
             * 
             * Responsed JSON format:
             * 
             *  {
             *      "statusCode",  0,
             *      "subscription_id": "subscription_id_string",
             *      "subscribed_timestamp": 0,
             *      "plan_id": "string",
             *      "message", "success"
             *  }
             * 
             */
            protected override void SubscribeHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[SubscribeHandler] message=" + message);
                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                string subscription_id = "";
                string errMessage = "";
                string plan_id = "";
                long subscribed_timestamp = 0L;

                try
                {
                    statusCode = (int)jsonData["statusCode"];
                    errMessage = (string)jsonData["message"];
                }
                catch (Exception ex)
                {
                    Viveport.Core.Logger.Log("[SubscribeHandler] statusCode, message ex=" + ex);
                }
                Viveport.Core.Logger.Log("[SubscribeHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                if (statusCode == 0)
                {
                    try
                    {
                        subscription_id = (string)jsonData["subscription_id"];
                        plan_id = (string)jsonData["plan_id"];
                        subscribed_timestamp = (long)jsonData["subscribed_timestamp"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[SubscribeHandler] subscription_id, plan_id ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[SubscribeHandler] subscription_id =" + subscription_id + ", plan_id=" + plan_id + ", timestamp=" + subscribed_timestamp.ToString());
                }
                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0)
                        {
                            listener.OnSubscribeSuccess(subscription_id);
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }
                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
            }

#endregion Subscribe

#region QuerySubscription

            public Internal.IAPurchaseCallback getQuerySubscriptionHandler()
            {
                return QuerySubscriptionHandler;
            }

            /*
            {
              "statusCode": 0,
              "message": "success",
              "subscriptions": [
                {
                  "app_id": "app001",
                  "order_id": "dev001",
                  "subscription_id": "s001",
                  "price": "10",
                  "currency": "USD",
                  "subscribed_timestamp": 1486905703000,
                  "free_trial_period": {"time_type":"month", "value":1},
                  "charge_period": {"time_type":"month", "value":1},
                  "number_of_charge_period": 3,
                  "plan_id": "01",
                  "status": "ACTIVE",
                  "status_detail": {"date_next_charge":1486905703000,
                  "transactions":[{"create_time":1486905703000, "payment_method":"wallet", "status":"finish"}],
                  "cancel_reason":"none"}
                }
              ]
            }
            */

            protected override void QuerySubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[QuerySubscriptionHandler] message=" + message);
                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                string errMessage = "";
                List<Subscription> subscriptions = null;

                if (code == 0)
                {
                    try
                    {
                        statusCode = (int)jsonData["statusCode"];
                        errMessage = (string)jsonData["message"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[QuerySubscriptionHandler] statusCode, message ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[QuerySubscriptionHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                    if (statusCode == 0)
                    {
                        try
                        {
                            QuerySubscritionResponse querySubscritionResponse = JsonMapper.ToObject<QuerySubscritionResponse>(message);
                            subscriptions = querySubscritionResponse.subscriptions;
                        }
                        catch (Exception ex)
                        {
                            Viveport.Core.Logger.Log("[QuerySubscriptionHandler] ex =" + ex);
                        }
                    }
                }
                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0 && subscriptions != null && subscriptions.Count > 0)
                        {
                            listener.OnQuerySubscriptionSuccess(subscriptions.ToArray());
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }
                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
            }

#endregion QuerySubscription

#region QuerySubscriptionList

            public Internal.IAPurchaseCallback getQuerySubscriptionListHandler()
            {
                return QuerySubscriptionListHandler;
            }

            /*
            {
              "statusCode": 0,
              "message": "success",
              "subscriptions": [
                {
                  "app_id": "app001",
                  "order_id": "dev001",
                  "subscription_id": "s001",
                  "price": "10",
                  "currency": "USD",
                  "subscribed_timestamp": 1486905703000,
                  "free_trial_period": {"time_type":"month", "value":1},
                  "charge_period": {"time_type":"month", "value":1},
                  "number_of_charge_period": 3,
                  "plan_id": "01",
                  "status": "ACTIVE",
                  "status_detail": {"date_next_charge":1486905703000,
                  "transactions":[{"create_time":1486905703000, "payment_method":"wallet", "status":"finish"}],
                  "cancel_reason":"none"}
                }
              ]
            }
            */

            protected override void QuerySubscriptionListHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[QuerySubscriptionListHandler] message=" + message);
                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                string errMessage = "";
                List<Subscription> subscriptions = null;

                if (code == 0)
                {
                    try
                    {
                        statusCode = (int)jsonData["statusCode"];
                        errMessage = (string)jsonData["message"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[QuerySubscriptionListHandler] statusCode, message ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[QuerySubscriptionListHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                    if (statusCode == 0)
                    {
                        try
                        {
                            QuerySubscritionResponse querySubscritionResponse = JsonMapper.ToObject<QuerySubscritionResponse>(message);
                            subscriptions = querySubscritionResponse.subscriptions;
                        }
                        catch (Exception ex)
                        {
                            Viveport.Core.Logger.Log("[QuerySubscriptionListHandler] ex =" + ex);
                        }
                    }
                }
                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0 && subscriptions != null && subscriptions.Count > 0)
                        {
                            listener.OnQuerySubscriptionListSuccess(subscriptions.ToArray());
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }
                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
            }
#endregion QuerySubscriptionList

#region CancelSubscription

            public Internal.IAPurchaseCallback getCancelSubscriptionHandler()
            {
                return CancelSubscriptionHandler;
            }

            /*
             * Responsed JSON format:
             * {
             *   "statusCode": 500,     // status code, Integer
             *   "message": "",         // error message information, String
             * }
            */

            protected override void CancelSubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
            {
                Viveport.Core.Logger.Log("[CancelSubscriptionHandler] message=" + message);
                JsonData jsonData = JsonMapper.ToObject(message);
                int statusCode = -1;
                bool isCanceled = false;
                string errMessage = "";

                if (code == 0)
                {
                    try
                    {
                        statusCode = (int)jsonData["statusCode"];
                        errMessage = (string)jsonData["message"];
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[CancelSubscriptionHandler] statusCode, message ex=" + ex);
                    }
                    Viveport.Core.Logger.Log("[CancelSubscriptionHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                    if (statusCode == 0)
                    {
                        isCanceled = true;
                        Viveport.Core.Logger.Log("[CancelSubscriptionHandler] isCanceled = " + isCanceled);
                    }
                }
                if (listener != null)
                {
                    if (code == 0)
                    {
                        // TODO The actual success judgement.
                        if (statusCode == 0)
                        {
                            listener.OnCancelSubscriptionSuccess(isCanceled);
                        }
                        else
                        {
                            listener.OnFailure(statusCode, errMessage);
                        }
                    }
                    else
                    {
                        listener.OnFailure(code, message);
                    }
                }
            }

#endregion CancelSubscription
        }

        private abstract partial class BaseHandler
        {
            protected abstract void IsReadyHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            protected abstract void RequestHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            protected abstract void PurchaseHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            protected abstract void QueryHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            protected abstract void QueryListHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            protected abstract void BalanceHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            protected abstract void RequestSubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            protected abstract void RequestSubscriptionWithPlanIDHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            protected abstract void SubscribeHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            protected abstract void QuerySubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            protected abstract void QuerySubscriptionListHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            protected abstract void CancelSubscriptionHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
        }

        public partial class IAPurchaseListener
        {
            public virtual void OnSuccess(string pchCurrencyName) { }
            public virtual void OnRequestSuccess(string pchPurchaseId) { }
            public virtual void OnPurchaseSuccess(string pchPurchaseId) { }
            public virtual void OnQuerySuccess(QueryResponse response) { }
            public virtual void OnQuerySuccess(QueryListResponse response) { }
            public virtual void OnBalanceSuccess(string pchBalance) { }
            public virtual void OnFailure(int nCode, string pchMessage) { }
            public virtual void OnRequestSubscriptionSuccess(string pchSubscriptionId) { }
            public virtual void OnRequestSubscriptionWithPlanIDSuccess(string pchSubscriptionId) { }
            public virtual void OnSubscribeSuccess(string pchSubscriptionId) { }
            public virtual void OnQuerySubscriptionSuccess(Subscription[] subscriptionlist) { }
            public virtual void OnQuerySubscriptionListSuccess(Subscription[] subscriptionlist) { }
            public virtual void OnCancelSubscriptionSuccess(bool bCanceled) { }
        }

        public class QueryResponse
        {
            public string order_id { get; set; }
            public string purchase_id { get; set; }
            public string status { get; set; }//the value of status is "created" or "processing" or "success" or "failure" or "expired"
            public string price { get; set; }
            public string currency { get; set; }
            public long paid_timestamp { get; set; }
        }

        public class QueryResponse2
        {
            public string order_id { get; set; }
            public string app_id { get; set; }
            public string purchase_id { get; set; }
            public string user_data { get; set; }
            public string price { get; set; }
            public string currency { get; set; }
            public long paid_timestamp { get; set; }
        }

        public class QueryListResponse
        {
            public int total { get; set; }
            public int from { get; set; }
            public int to { get; set; }
            public List<QueryResponse2> purchaseList;
        }

        public class StatusDetailTransaction
        {
            public long create_time { get; set; }
            public string payment_method { get; set; }
            public string status { get; set; }//paymentFailed/pendingWebhook/finish
        }

        public class StatusDetail
        {
            public long date_next_charge { get; set; }
            public StatusDetailTransaction[] transactions { get; set; }
            public string cancel_reason { get; set; }
        }

        public class TimePeriod
        {
            public string time_type { get; set; }
            public int value { get; set; }
        }

        public class Subscription
        {
            public string app_id { get; set; }
            public string order_id { get; set; }
            public string subscription_id { get; set; }
            public string price { get; set; }
            public string currency { get; set; }
            public long subscribed_timestamp { get; set; }
            public TimePeriod free_trial_period { get; set; }
            public TimePeriod charge_period { get; set; }
            public int number_of_charge_period { get; set; }
            public string plan_id { get; set; }
            public string plan_name { get; set; }
            //the value of status is "created" or "processing" or "failure" or "expired" or "ACTIVE" or "NON_RENEWING" or "CANCELED"
            public string status { get; set; }
            public StatusDetail status_detail { get; set; }
        }

        public class QuerySubscritionResponse
        {
            public int statusCode { get; set; }
            public string message { get; set; }
            public List<Subscription> subscriptions { get; set; }
        }
    }

    public partial class DLC
    {
#if !UNITY_ANDROID
#region IL2CPP Support
        private static Internal.StatusCallback isDlcReadyIl2cppCallback;
#endregion

        private const int AppIdLength = 37;

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void IsDlcReadyIl2cppCallback(int errorCode) { isDlcReadyIl2cppCallback(errorCode); }

        public static int IsDlcReady(StatusCallback callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            isDlcReadyIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(IsDlcReadyIl2cppCallback);
            if(IntPtr.Size == 8)
            {
                return Internal.DLC.IsReady_64(IsDlcReadyIl2cppCallback);
            }
            else
            {
                return Internal.DLC.IsReady(IsDlcReadyIl2cppCallback);
            }
        }

		public static int GetCount()
		{
            if(IntPtr.Size == 8)
            {
                return Internal.DLC.GetCount_64();
            }
            else
            {
                return Internal.DLC.GetCount();
            }
		}

		public static bool GetIsAvailable(int index, out string appId, out bool isAvailable)
        {
            bool isInRange = false;

            var appIdSB = new StringBuilder(AppIdLength);
            if(IntPtr.Size == 8)
            {
                isInRange = Internal.DLC.GetIsAvailable_64(index, appIdSB, out isAvailable);
            }
            else
            {
                isInRange = Internal.DLC.GetIsAvailable(index, appIdSB, out isAvailable);
            }
            appId = appIdSB.ToString();

            return isInRange;
        }

        public static int IsSubscribed()
        {
            if(IntPtr.Size == 8)
            {
                return Internal.DLC.IsSubscribed_64();
            }
            else
            {
                return Internal.DLC.IsSubscribed();
            }
        }
#endif
    }

    public partial class Subscription
    {
#region IL2CPP Support
        private static Internal.StatusCallback2 isReadyIl2cppCallback;
#endregion

        [MonoPInvokeCallback(typeof(Internal.StatusCallback2))]
        private static void IsReadyIl2cppCallback(int errorCode, string message) { isReadyIl2cppCallback(errorCode, message); }
        public static void IsReady(StatusCallback2 callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            isReadyIl2cppCallback = new Internal.StatusCallback2(callback);
            Api.InternalStatusCallback2s.Add(IsReadyIl2cppCallback);

            var internalCallback = new Internal.StatusCallback2(callback);
#if !UNITY_ANDROID       
            if (IntPtr.Size == 8)
            {
                Internal.Subscription.IsReady_64(IsReadyIl2cppCallback);
            }
            else
            {
                Internal.Subscription.IsReady(IsReadyIl2cppCallback);
            }
#else
            Internal.Subscription.IsReady(IsReadyIl2cppCallback);
#endif
        }

        public static SubscriptionStatus GetUserStatus()
        {
            var status = new SubscriptionStatus();
#if !UNITY_ANDROID 
            if (IntPtr.Size == 8)
            {
                if (Internal.Subscription.IsWindowsSubscriber_64())
                {
                    status.Platforms.Add(SubscriptionStatus.Platform.Windows);
                }
                if (Internal.Subscription.IsAndroidSubscriber_64())
                {
                    status.Platforms.Add(SubscriptionStatus.Platform.Android);
                }

                switch (Internal.Subscription.GetTransactionType_64())
                {
                    case Internal.ESubscriptionTransactionType.UNKNOWN:
                        status.Type = SubscriptionStatus.TransactionType.Unknown;
                        break;
                    case Internal.ESubscriptionTransactionType.PAID:
                        status.Type = SubscriptionStatus.TransactionType.Paid;
                        break;
                    case Internal.ESubscriptionTransactionType.REDEEM:
                        status.Type = SubscriptionStatus.TransactionType.Redeem;
                        break;
                    case Internal.ESubscriptionTransactionType.FREEE_TRIAL:
                        status.Type = SubscriptionStatus.TransactionType.FreeTrial;
                        break;
                    default:
                        status.Type = SubscriptionStatus.TransactionType.Unknown;
                        break;
                }
            }
            else
            {
                if (Internal.Subscription.IsWindowsSubscriber())
                {
                    status.Platforms.Add(SubscriptionStatus.Platform.Windows);
                }
                if (Internal.Subscription.IsAndroidSubscriber())
                {
                    status.Platforms.Add(SubscriptionStatus.Platform.Android);
                }

                switch (Internal.Subscription.GetTransactionType())
                {
                    case Internal.ESubscriptionTransactionType.UNKNOWN:
                        status.Type = SubscriptionStatus.TransactionType.Unknown;
                        break;
                    case Internal.ESubscriptionTransactionType.PAID:
                        status.Type = SubscriptionStatus.TransactionType.Paid;
                        break;
                    case Internal.ESubscriptionTransactionType.REDEEM:
                        status.Type = SubscriptionStatus.TransactionType.Redeem;
                        break;
                    case Internal.ESubscriptionTransactionType.FREEE_TRIAL:
                        status.Type = SubscriptionStatus.TransactionType.FreeTrial;
                        break;
                    default:
                        status.Type = SubscriptionStatus.TransactionType.Unknown;
                        break;
                }
            }
#else
            if (Internal.Subscription.IsWindowsSubscriber())
            {
                status.Platforms.Add(SubscriptionStatus.Platform.Windows);
            }
            if (Internal.Subscription.IsAndroidSubscriber())
            {
                status.Platforms.Add(SubscriptionStatus.Platform.Android);
            }
            switch (Internal.Subscription.GetTransactionType())
            {
                case Internal.ESubscriptionTransactionType.UNKNOWN:
                    status.Type = SubscriptionStatus.TransactionType.Unknown;
                    break;
                case Internal.ESubscriptionTransactionType.PAID:
                    status.Type = SubscriptionStatus.TransactionType.Paid;
                    break;
                case Internal.ESubscriptionTransactionType.REDEEM:
                    status.Type = SubscriptionStatus.TransactionType.Redeem;
                    break;
                case Internal.ESubscriptionTransactionType.FREEE_TRIAL:
                    status.Type = SubscriptionStatus.TransactionType.FreeTrial;
                    break;
                default:
                    status.Type = SubscriptionStatus.TransactionType.Unknown;
                    break;
            }
#endif

            return status;
        }
    }


    public partial class Deeplink
    {

        private const int MaxIdLength = 256;
        private static Internal.StatusCallback isReadyIl2cppCallback;
        private static Internal.StatusCallback2 goToAppIl2cppCallback;
        private static Internal.StatusCallback2 goToAppWithBranchNameIl2cppCallback;
        private static Internal.StatusCallback2 goToStoreIl2cppCallback;
        private static Internal.StatusCallback2 goToAppOrGoToStoreIl2cppCallback;

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void IsReadyIl2cppCallback(int errorCode) { isReadyIl2cppCallback(errorCode); }

        public static void IsReady(StatusCallback callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }
            isReadyIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(IsReadyIl2cppCallback);

#if !UNITY_ANDROID
            if (IntPtr.Size == 8)
            {
                Internal.Deeplink.IsReady_64(IsReadyIl2cppCallback);
            }
            else
            {
                Internal.Deeplink.IsReady(IsReadyIl2cppCallback);
            }
#else
            Internal.Deeplink.IsReady(IsReadyIl2cppCallback);
#endif
        }

#if !UNITY_ANDROID
        [MonoPInvokeCallback(typeof(Internal.StatusCallback2))]
        private static void GoToAppIl2cppCallback(int errorCode, string message) { goToAppIl2cppCallback(errorCode, message); }

        public static void GoToApp(
                StatusCallback2 callback, 
                string viveportId, 
                string launchData)
        {
            if (callback == null || string.IsNullOrEmpty(viveportId))
            {
                throw new InvalidOperationException("callback == null || string.IsNullOrEmpty(viveportId)");
            }

            goToAppIl2cppCallback = new Internal.StatusCallback2(callback);
            Api.InternalStatusCallback2s.Add(GoToAppIl2cppCallback);

            if (IntPtr.Size == 8)
            {
                Internal.Deeplink.GoToApp_64(GoToAppIl2cppCallback, viveportId, launchData);
            }
            else
            {
                Internal.Deeplink.GoToApp(GoToAppIl2cppCallback, viveportId, launchData);
            }
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback2))]
        private static void GoToAppWithBranchNameIl2cppCallback(int errorCode, string message) { goToAppWithBranchNameIl2cppCallback(errorCode, message); }

        public static void GoToApp(
                StatusCallback2 callback,
                string viveportId,
                string launchData,
                string branchName)
        {
            if (callback == null || string.IsNullOrEmpty(viveportId))
            {
                throw new InvalidOperationException("callback == null || string.IsNullOrEmpty(viveportId)");
            }

            goToAppWithBranchNameIl2cppCallback = new Internal.StatusCallback2(callback);
            Api.InternalStatusCallback2s.Add(GoToAppWithBranchNameIl2cppCallback);

            if (IntPtr.Size == 8)
            {
                Internal.Deeplink.GoToApp_64(GoToAppWithBranchNameIl2cppCallback, viveportId, launchData, branchName);
            }
            else
            {
                Internal.Deeplink.GoToApp(GoToAppWithBranchNameIl2cppCallback, viveportId, launchData, branchName);
            }
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback2))]
        private static void GoToStoreIl2cppCallback(int errorCode, string message) { goToStoreIl2cppCallback(errorCode, message); }

        public static void GoToStore(
                StatusCallback2 callback, 
                string viveportId = "")
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null || string.IsNullOrEmpty(viveportId)");
            }

            goToStoreIl2cppCallback = new Internal.StatusCallback2(callback);
            Api.InternalStatusCallback2s.Add(GoToStoreIl2cppCallback);

            if (IntPtr.Size == 8)
            {
                Internal.Deeplink.GoToStore_64(GoToStoreIl2cppCallback, viveportId);
            }
            else
            {
                Internal.Deeplink.GoToStore(GoToStoreIl2cppCallback, viveportId);
            }
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback2))]
        private static void GoToAppOrGoToStoreIl2cppCallback(int errorCode, string message) { goToAppOrGoToStoreIl2cppCallback(errorCode, message); }

        public static void GoToAppOrGoToStore(
                StatusCallback2 callback,
                string viveportId,
                string launchData)
        {
            if (callback == null || string.IsNullOrEmpty(viveportId))
            {
                throw new InvalidOperationException("callback == null || string.IsNullOrEmpty(viveportId)");
            }

            goToAppOrGoToStoreIl2cppCallback = new Internal.StatusCallback2(callback);
            Api.InternalStatusCallback2s.Add(GoToAppOrGoToStoreIl2cppCallback);

            if (IntPtr.Size == 8)
            {
                Internal.Deeplink.GoToAppOrGoToStore_64(GoToAppOrGoToStoreIl2cppCallback, viveportId, launchData);
            }
            else
            {
                Internal.Deeplink.GoToAppOrGoToStore(GoToAppOrGoToStoreIl2cppCallback, viveportId, launchData);
            }
        }

        public static string GetAppLaunchData()
        {
#if !UNITY_ANDROID
            var userId = new StringBuilder(MaxIdLength);
            if (IntPtr.Size == 8)
            {
                Internal.Deeplink.GetAppLaunchData_64(userId, MaxIdLength);
            }
            else
            {
                Internal.Deeplink.GetAppLaunchData(userId, MaxIdLength);
            }
            return userId.ToString();
#else
            return Internal.Deeplink.GetAppLaunchData();
#endif
        }
#endif

#if UNITY_ANDROID
        public static void GoToApp(
                DeeplinkChecker checker,
                string appId,
                string launchData)
        {
            if (checker == null || string.IsNullOrEmpty(appId))
            {
                throw new InvalidOperationException("checker == null || string.IsNullOrEmpty(appId)");
            }

            Internal.Deeplink.GoToApp(checker, appId, launchData);
        }

        public static void GoToApp(
                DeeplinkChecker checker,
                string appId,
                string launchData,
                string branchName)
        {
            if (checker == null || string.IsNullOrEmpty(appId))
            {
                throw new InvalidOperationException("checker == null || string.IsNullOrEmpty(appId)");
            }

            Internal.Deeplink.GoToApp(checker, appId, launchData);
        }

        public static void GoToStore(
            DeeplinkChecker checker,
                string appId = "")
        {
            if (checker == null)
            {
                throw new InvalidOperationException("checker == null || string.IsNullOrEmpty(appId)");
            }

            Internal.Deeplink.GoToStore(checker, appId);
        }

        public static void GoToAppOrGoToStore(
                DeeplinkChecker checker,
                string appId,
                string launchData)
        {
            if (checker == null || string.IsNullOrEmpty(appId))
            {
                throw new InvalidOperationException("checker == null || string.IsNullOrEmpty(appId)");
            }

            Internal.Deeplink.GoToAppOrGoToStore(checker, appId, launchData);
        }

        public static string GetAppLaunchData()
        {
            return Internal.Deeplink.GetAppLaunchData();
        }

        public abstract class DeeplinkChecker
        {
            public abstract void OnSuccess();
            public abstract void OnFailure(
                    int errorCode,
                    string errorMessage
            );
        }
#endif
    }


    class Token
    {
#region IL2CPP Support
        private static Internal.StatusCallback isReadyIl2cppCallback;
        private static Internal.StatusCallback2 getSessionTokenIl2cppCallback;
#endregion

        [MonoPInvokeCallback(typeof(Internal.StatusCallback))]
        private static void IsReadyIl2cppCallback(int errorCode) { isReadyIl2cppCallback(errorCode); }

        public static void IsReady(StatusCallback callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            isReadyIl2cppCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(IsReadyIl2cppCallback);
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.Token.IsReady_64(IsReadyIl2cppCallback);
            }
            else
            {
                Internal.Token.IsReady(IsReadyIl2cppCallback);
            }
#else
            Internal.Token.IsReady(IsReadyIl2cppCallback);
#endif            
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback2))]
        private static void GetSessionTokenIl2cppCallback(int errorCode, string message) { getSessionTokenIl2cppCallback(errorCode, message); }

        public static void GetSessionToken(StatusCallback2 callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            getSessionTokenIl2cppCallback = new Internal.StatusCallback2(callback);
            Api.InternalStatusCallback2s.Add(GetSessionTokenIl2cppCallback);
#if !UNITY_ANDROID
            if(IntPtr.Size == 8)
            {
                Internal.Token.GetSessionToken_64(GetSessionTokenIl2cppCallback);
            }
            else
            {
                Internal.Token.GetSessionToken(GetSessionTokenIl2cppCallback);
            }
#else
               Internal.Token.GetSessionToken(GetSessionTokenIl2cppCallback);
#endif        
        }
    }

    class Avatar
    {
#region IL2CPP Support
        private static Internal.StatusCallback2 downloadAvatarIl2cppCallback;
        private static Internal.StatusCallback2 getAvatarListIl2cppCallback;
#endregion

        [MonoPInvokeCallback(typeof(Internal.StatusCallback2))]
        private static void DownloadAvatarIl2cppCallback(int errorCode, string message) { downloadAvatarIl2cppCallback(errorCode, message); }

        public static void DownloadAvatar(StatusCallback2 callback, int id)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            downloadAvatarIl2cppCallback = new Internal.StatusCallback2(callback);
            Api.InternalStatusCallback2s.Add(downloadAvatarIl2cppCallback);

#if !UNITY_ANDROID
#else
            Internal.Avatar.DownloadAvatar(DownloadAvatarIl2cppCallback, id);
#endif
        }

        public static void DownloadAvatar(StatusCallback2 callback, string url)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            downloadAvatarIl2cppCallback = new Internal.StatusCallback2(callback);
            Api.InternalStatusCallback2s.Add(downloadAvatarIl2cppCallback);

#if !UNITY_ANDROID
#else
            Internal.Avatar.DownloadAvatar(DownloadAvatarIl2cppCallback, url);
#endif
        }

        [MonoPInvokeCallback(typeof(Internal.StatusCallback2))]
        private static void GetAvatarListIl2cppCallback(int errorCode, string message) { getAvatarListIl2cppCallback(errorCode, message); }

        public static void GetAvatarList(StatusCallback2 callback)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            getAvatarListIl2cppCallback = new Internal.StatusCallback2(callback);
            Api.InternalStatusCallback2s.Add(getAvatarListIl2cppCallback);

#if !UNITY_ANDROID
#else
            Internal.Avatar.GetAvatarList(GetAvatarListIl2cppCallback);
#endif
        }

        public static List<AvatarData> ParseAvatarList(string json)
        {
            var avatarList = new List<AvatarData>();

            try
            {
                var jsonData = JsonMapper.ToObject(json);

                if (jsonData != null)
                {
                    foreach (JsonData avatar in jsonData["avatars"])
                    {
                        var avatarDictionary = avatar as IDictionary;

                        long updateTimeUtcInSec = 0;

                        if (avatarDictionary.Contains("updateTimeUtcInSec"))
                        {
                            if (avatar["updateTimeUtcInSec"].IsLong)
                                updateTimeUtcInSec = (long)avatar["updateTimeUtcInSec"];
                            else if (avatar["updateTimeUtcInSec"].IsInt)
                                updateTimeUtcInSec = (int)avatar["updateTimeUtcInSec"];
                        }

                        long createTimeUtcInMilli = 0;

                        if (avatarDictionary.Contains("createTimeUtcInMilli"))
                        {
                            if (avatar["createTimeUtcInMilli"].IsLong)
                                createTimeUtcInMilli = (long)avatar["createTimeUtcInMilli"];
                            else if (avatar["createTimeUtcInMilli"].IsInt)
                                createTimeUtcInMilli = (int)avatar["createTimeUtcInMilli"];
                        }

                        avatarList.Add(new AvatarData()
                        {
                            Id = avatarDictionary.Contains("id") ? (int)avatar["id"] : -1,
                            ViveportId = avatarDictionary.Contains("viveportId") ? (string)avatar["viveportId"] : null,
                            DataType = avatarDictionary.Contains("dataType") ? (DataType)(int)avatar["dataType"] : DataType.Unknown,
                            Data = avatarDictionary.Contains("data") ? (string)avatar["data"] : null,
                            MetaData = avatarDictionary.Contains("metaData") ? (string)avatar["metaData"] : null,
                            S3KeyBin = avatarDictionary.Contains("s3KeyBin") ? (string)avatar["s3KeyBin"] : null,
                            S3KeySnapshot = avatarDictionary.Contains("s3KeySnapshot") ? (string)avatar["s3KeySnapshot"] : null,
                            S3KeyHeadIcon = avatarDictionary.Contains("s3KeyHeadIcon") ? (string)avatar["s3KeyHeadIcon"] : null,
                            S3KeyVrmBin = avatarDictionary.Contains("s3KeyVrmBin") ? (string)avatar["s3KeyVrmBin"] : null,
                            BinaryDataUrl = avatarDictionary.Contains("binaryDataUrl") ? (string)avatar["binaryDataUrl"] : null,
                            VrmBinaryDataUrl = avatarDictionary.Contains("vrmBinaryDataUrl") ? (string)avatar["vrmBinaryDataUrl"] : null,
                            SnapshotDataUrl = avatarDictionary.Contains("snapshotDataUrl") ? (string)avatar["snapshotDataUrl"] : null,
                            HeadIconDataUrl = avatarDictionary.Contains("headIconDataUrl") ? (string)avatar["headIconDataUrl"] : null,
                            UpdateTimeUtcInSec = updateTimeUtcInSec,
                            CreateTimeUtcInMilli = createTimeUtcInMilli,
                            UpperBodyNft = avatarDictionary.Contains("upperBodyNft") ? (string)avatar["upperBodyNft"] : null,
                            LowerBodyNft = avatarDictionary.Contains("lowerBodyNft") ? (string)avatar["lowerBodyNft"] : null,
                            FootNft = avatarDictionary.Contains("footNft") ? (string)avatar["footNft"] : null,
                            IsEncrypted = avatarDictionary.Contains("isEncrypted") ? (bool)avatar["isEncrypted"] : false,
                            IsCurrent = avatarDictionary.Contains("isCurrent") ? (bool)avatar["isCurrent"] : false
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Viveport.Core.Logger.Log("[Avatar ParseAvatarList] exception=" + ex);
            }

            return avatarList;
        }

        public class AvatarData
        {
            public int Id = -1;
            public string ViveportId;
            public DataType DataType = DataType.Unknown;
            public string Data;
            public string MetaData;
            public string S3KeyBin;
            public string S3KeySnapshot;
            public string S3KeyHeadIcon;
            public string S3KeyVrmBin;
            public string BinaryDataUrl;
            public string VrmBinaryDataUrl;
            public string SnapshotDataUrl;
            public string HeadIconDataUrl;
            public long UpdateTimeUtcInSec;
            public long CreateTimeUtcInMilli;
            public string UpperBodyNft;
            public string LowerBodyNft;
            public string FootNft;
            public bool IsEncrypted;
            public bool IsCurrent;
        }

        public enum DataType
        {
            Unknown = -1,
            Sytlized,
            Realistic,
            HandMadeAvatarHead,
            HandMadeAvatarFullBody,
            VRM,
            NFTFullBody,
            GLB,
            Cartoon
        }
    }

    namespace Arcade
    {
        partial class Session
        {
#if !UNITY_ANDROID
#region IL2CPP Support
            private static Internal.Arcade.SessionCallback isReadyIl2cppCallback;
            private static Internal.Arcade.SessionCallback startIl2cppCallback;
            private static Internal.Arcade.SessionCallback stopIl2cppCallback;
#endregion

            [MonoPInvokeCallback(typeof(Internal.Arcade.SessionCallback))]
            private static void IsReadyIl2cppCallback(int errorCode, string message) { isReadyIl2cppCallback(errorCode, message); }

            public static void IsReady(SessionListener listener)
            {
                isReadyIl2cppCallback = new SessionHandler(listener).getIsReadyHandler();
                if(IntPtr.Size == 8)
                {
                    Internal.Arcade.Session.IsReady_64(IsReadyIl2cppCallback);
                }
                else
                {
                    Internal.Arcade.Session.IsReady(IsReadyIl2cppCallback);
                }
            }

            [MonoPInvokeCallback(typeof(Internal.Arcade.SessionCallback))]
            private static void StartIl2cppCallback(int errorCode, string message) { startIl2cppCallback(errorCode, message); }

            public static void Start(SessionListener listener)
            {
                startIl2cppCallback = new SessionHandler(listener).getStartHandler();
                if(IntPtr.Size == 8)
                {
                    Internal.Arcade.Session.Start_64(StartIl2cppCallback);
                }
                else
                {
                    Internal.Arcade.Session.Start(StartIl2cppCallback);
                }
            }

            [MonoPInvokeCallback(typeof(Internal.Arcade.SessionCallback))]
            private static void StopIl2cppCallback(int errorCode, string message) { stopIl2cppCallback(errorCode, message); }

            public static void Stop(SessionListener listener)
            {
                stopIl2cppCallback = new SessionHandler(listener).getStopHandler();
                if(IntPtr.Size == 8)
                {
                    Internal.Arcade.Session.Stop_64(StopIl2cppCallback);
                }
                else
                {
                    Internal.Arcade.Session.Stop(StopIl2cppCallback);
                }
            }

            partial class SessionHandler : BaseHandler
            {
                static SessionListener listener;

                public SessionHandler(SessionListener cb)
                {
                    listener = cb;
                }

#region IsReady
                public Internal.Arcade.SessionCallback getIsReadyHandler()
                {
                    return IsReadyHandler;
                }

                /*
                 * 
                 * Responsed JSON format:
                 * {
                 *   "statusCode": 0, // status code, Integer, 0 is success
                 *   "appID": "",     // app ID that is passed by Api.Init(), String
                 *   "message": "",   // error message information
                 * }
                 * 
                 */
                protected override void IsReadyHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
                {
                    //code is 0(ipc successful) or 20001(Functions are not supported) 
                    Viveport.Core.Logger.Log("[Session IsReadyHandler] message=" + message + ",code=" + code);
                    JsonData jsonData = null;
                    try
                    {
                        jsonData = JsonMapper.ToObject(message);
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[Session IsReadyHandler] exception="+ex);
                    }
                    int statusCode = -1;
                    string errMessage = "";
                    string appID = "";

                    if (code == 0 && jsonData != null)
                    {
                        try
                        {
                            statusCode = (int)jsonData["statusCode"];
                            errMessage = (string)jsonData["message"];
                        }
                        catch (Exception ex)
                        {
                            Viveport.Core.Logger.Log("[IsReadyHandler] statusCode, message ex=" + ex);
                        }
                        Viveport.Core.Logger.Log("[IsReadyHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                        if (statusCode == 0)
                        {
                            try
                            {
                                appID = (string)jsonData["appID"];
                            }
                            catch (Exception ex)
                            {
                                Viveport.Core.Logger.Log("[IsReadyHandler] appID ex=" + ex);
                            }
                            Viveport.Core.Logger.Log("[IsReadyHandler] appID=" + appID);
                        }
                    }

                    if (listener != null)
                    {
                        if (code == 0)
                        {
                            // TODO The actual success judgement.
                            if (statusCode == 0)
                            {
                                listener.OnSuccess(appID);
                            }
                            else
                            {
                                listener.OnFailure(statusCode, errMessage);
                            }
                        }
                        else
                        {
                            listener.OnFailure(code, message);
                        }
                    }
                }
#endregion IsReady

#region Start
                public Internal.Arcade.SessionCallback getStartHandler()
                {
                    return StartHandler;
                }

                /*
                 * 
                 * Responsed JSON format:
                 * {
                 *   "statusCode": 0, // status code, Integer, 0 is success
                 *   "appID": "",     // app ID that is passed by Api.Init(), String
                 *   "Guid": "",      // Guid that is generated on every session to represent a unique session, if succes, a valid and unique Guid, if error, an empty Guid 
                 *   "message": "",   // error message information
                 * }
                 * 
                 */
                protected override void StartHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
                {
                    //code is 0(ipc successful) or 20001(Functions are not supported) 
                    Viveport.Core.Logger.Log("[Session StartHandler] message=" + message + ",code=" + code);
                    JsonData jsonData = null;
                    try
                    {
                        jsonData = JsonMapper.ToObject(message);
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[Session StartHandler] exception=" + ex);
                    }
                    int statusCode = -1;
                    string errMessage = "";
                    string appID = "";
                    string Guid = "";

                    if (code == 0 && jsonData != null)
                    {
                        try
                        {
                            statusCode = (int)jsonData["statusCode"];
                            errMessage = (string)jsonData["message"];
                        }
                        catch (Exception ex)
                        {
                            Viveport.Core.Logger.Log("[StartHandler] statusCode, message ex=" + ex);
                        }
                        Viveport.Core.Logger.Log("[StartHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                        if (statusCode == 0)
                        {
                            try
                            {
                                appID = (string)jsonData["appID"];
                                Guid = (string)jsonData["Guid"];
                            }
                            catch (Exception ex)
                            {
                                Viveport.Core.Logger.Log("[StartHandler] appID, Guid ex=" + ex);
                            }
                            Viveport.Core.Logger.Log("[StartHandler] appID=" + appID + ",Guid=" + Guid);
                        }
                    }

                    if (listener != null)
                    {
                        if (code == 0)
                        {
                            // TODO The actual success judgement.
                            if (statusCode == 0)
                            {
                                listener.OnStartSuccess(appID, Guid);
                            }
                            else
                            {
                                listener.OnFailure(statusCode, errMessage);
                            }
                        }
                        else
                        {
                            listener.OnFailure(code, message);
                        }
                    }
                }
#endregion Start

#region Stop
                public Internal.Arcade.SessionCallback getStopHandler()
                {
                    return StopHandler;
                }

                /*
                 * 
                 * Responsed JSON format:
                 * {
                 *   "statusCode": 0, // status code, Integer, 0 is success
                 *   "appID": "",     // app ID that is passed by Api.Init(), String
                 *   "Guid": "",      // Guid that is the same as the Guid received by the callback of Start(), if succes, a valid and unique Guid, if error, an empty Guid 
                 *   "message": "",   // error message information
                 * }
                 * 
                 */
                protected override void StopHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message)
                {
                    //code is 0(ipc successful) or 20001(Functions are not supported) 
                    Viveport.Core.Logger.Log("[Session StopHandler] message=" + message + ",code=" + code);
                    JsonData jsonData = null;
                    try
                    {
                        jsonData = JsonMapper.ToObject(message);
                    }
                    catch (Exception ex)
                    {
                        Viveport.Core.Logger.Log("[Session StopHandler] exception=" + ex);
                    }
                    int statusCode = -1;
                    string errMessage = "";
                    string appID = "";
                    string Guid = "";

                    if (code == 0 && jsonData != null)
                    {
                        try
                        {
                            statusCode = (int)jsonData["statusCode"];
                            errMessage = (string)jsonData["message"];
                        }
                        catch (Exception ex)
                        {
                            Viveport.Core.Logger.Log("[StopHandler] statusCode, message ex=" + ex);
                        }
                        Viveport.Core.Logger.Log("[StopHandler] statusCode =" + statusCode + ",errMessage=" + errMessage);
                        if (statusCode == 0)
                        {
                            try
                            {
                                appID = (string)jsonData["appID"];
                                Guid = (string)jsonData["Guid"];
                            }
                            catch (Exception ex)
                            {
                                Viveport.Core.Logger.Log("[StopHandler] appID, Guid ex=" + ex);
                            }
                            Viveport.Core.Logger.Log("[StopHandler] appID=" + appID + ",Guid=" + Guid);
                        }
                    }

                    if (listener != null)
                    {
                        if (code == 0)
                        {
                            // TODO The actual success judgement.
                            if (statusCode == 0)
                            {
                                listener.OnStopSuccess(appID, Guid);
                            }
                            else
                            {
                                listener.OnFailure(statusCode, errMessage);
                            }
                        }
                        else
                        {
                            listener.OnFailure(code, message);
                        }
                    }
                }
#endregion Stop
            }

            abstract partial class BaseHandler
            {
                protected abstract void IsReadyHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
                protected abstract void StartHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
                protected abstract void StopHandler(int code, [MarshalAs(UnmanagedType.LPStr)] string message);
            }

            public partial class SessionListener
            {
                public virtual void OnSuccess(string pchAppID) { }
                public virtual void OnStartSuccess(string pchAppID, string pchGuid) { }
                public virtual void OnStopSuccess(string pchAppID, string pchGuid) { }
                public virtual void OnFailure(int nCode, string pchMessage) { }
            }
#endif
        }
    }
}
