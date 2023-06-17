using System;
using System.Runtime.InteropServices;

namespace Viveport.Internal
{
#if !UNITY_ANDROID
    internal partial class UserStats
    {
        [DllImport("viveport_api", EntryPoint = "IViveportUserStats_GetAchievementDisplayAttribute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GetAchievementDisplayAttribute(string pchName, string pchKey);
    }

    internal partial class Ads
    {
        [DllImport("viveport_api", EntryPoint = "IViveportAds_AddToFavorite", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int AddToFavorite(StatusCallback callback, string pchADId, string pchPosId, string pchGameId);
    }

#endif
}
