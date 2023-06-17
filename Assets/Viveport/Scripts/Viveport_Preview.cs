using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Viveport.Core;

using LitJson;
using System.Collections;

namespace Viveport
{
    partial class UserStats
    {
#if !UNITY_ANDROID
        public static string GetAchievementDisplayAttribute(string pchName, string pchKey)
        {
            string nativeVersion = "";
            IntPtr ptr = Internal.UserStats.GetAchievementDisplayAttribute(pchName, pchKey);
            nativeVersion += Marshal.PtrToStringAnsi(ptr);

            return nativeVersion;
        }
#endif
    }

    partial class Ads
    {
#if !UNITY_ANDROID
        public static int AddToFavorite(StatusCallback callback, string pchADId, string pchPosId, string pchGameId)
        {
            if (callback == null)
            {
                throw new InvalidOperationException("callback == null");
            }

            Internal.StatusCallback internalCallback = new Internal.StatusCallback(callback);
            Api.InternalStatusCallbacks.Add(internalCallback);

            return Internal.Ads.AddToFavorite(internalCallback, pchADId, pchPosId, pchGameId);
        }
#endif
    }

}
