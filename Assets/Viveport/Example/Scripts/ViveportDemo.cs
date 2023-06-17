using UnityEngine;
using System.Collections;
using Viveport;

public class ViveportDemo : MonoBehaviour
{

    private int nInitValue = 0, nResult = 0;
#if UNITY_ANDROID
    private int nWidth = 150, nHeight = 100;
#else
    private int nWidth = 110, nHeight = 40;
#endif
    private int nXStart = 10, nYStart = 35;
    private string stringToEdit = "ID_Stat1";
    private string StatsCount = "80";
    private string achivToEdit = "ID_Achievement1";
    private string leaderboardToEdit = "ID_Leaderboard1";
    private string leaderboardUserName = "Karl";
    private string leaderboardScore = "1000";
    private static bool bInit = true, bIsReady = false, bUserProfileIsReady = false, bArcadeIsReady = false, bTokenIsReady = false;

    private static string msgBuffer = "";

    static string APP_ID = "bd67b286-aafc-449d-8896-bb7e9b351876";
    static string APP_KEY = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDFypCg0OHf"
                          + "BC+VZLSWPbNSgDo9qg/yQORDwGy1rKIboMj3IXn4Zy6h6bgn"
                          + "8kiMY7VI0lPwIj9lijT3ZxkzuTsI5GsK//Y1bqeTol4OUFR+"
                          + "47gj+TUuekAS2WMtglKox+/7mO6CA1gV+jZrAKo6YSVmPd+o"
                          + "FsgisRcqEgNh5MIURQIDAQAB";

    static void Log(string msg) {
        msgBuffer = msg + "\n" + msgBuffer;
        Viveport.Core.Logger.Log(msg);
    }

    // Use this for initialization
    void Start()
    {
        Api.Init(InitStatusHandler, APP_ID);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnGUI()
    {
        GUIStyle CustButton = new GUIStyle("button");
        GUIStyle CustButton1 = new GUIStyle("button");
#if UNITY_ANDROID
        CustButton.fontSize = 23;
        CustButton1.fontSize = 23;
#else
        CustButton1.fontSize = 10;
#endif

#if UNITY_ANDROID
        GUIStyle guiStyle = new GUIStyle("guiStyle");
        guiStyle.fontSize = 40;
        guiStyle.alignment = TextAnchor.UpperLeft;
        msgBuffer = GUI.TextArea(new Rect(10, nYStart + 4 * nWidth + 20, 1600, 800), msgBuffer, guiStyle);
#endif
        if (bInit == false)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.grey;

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

        // Api Version function
        if (GUI.Button(new Rect((nXStart + 2 * (nWidth + 10)), nYStart, nWidth, nHeight), "Version", CustButton))
        {
            if (bInit == true)
                Viveport.Core.Logger.Log("Version: " + Api.Version());
        }

#if !UNITY_ANDROID
        // QueryRunMode function
        if (GUI.Button(new Rect((nXStart + 3 * (nWidth + 10)), nYStart, nWidth, nHeight), "QueryRunMode", CustButton))
        {
            if (bInit == true)
                Api.QueryRuntimeMode(QueryRunTimeHandler);
        }
#endif
        // IsReady function
        if (GUI.Button(new Rect((nXStart + 4 * (nWidth + 10)), nYStart, nWidth, nHeight), "StatIsReady", CustButton))
        {
            if (bInit == true)
                UserStats.IsReady(IsReadyHandler);
        }

        /***************************************************************************/
        /*                        DRM sample code                                  */
        /***************************************************************************/
        // DRM function
        if (GUI.Button(new Rect((nXStart + 6 * (nWidth + 10)), nYStart, nWidth, nHeight), "DRM", CustButton))
        {
            if (bInit == true)
            {
                Api.GetLicense(new MyLicenseChecker(), APP_ID, APP_KEY);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

#if !UNITY_ANDROID
        /***************************************************************************/
        /*                        ArcadeLeaderboard sample code                    */
        /***************************************************************************/
        // UserProfile function
        if (GUI.Button(new Rect((nXStart + 5 * (nWidth + 10)), nYStart, nWidth, nHeight), "ArcadeIsReady", CustButton))
        {
            if (bInit == true)
                ArcadeLeaderboard.IsReady(IsArcadeLeaderboardReadyHandler);
        }

        if (bInit == true && bIsReady == true)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.grey;
#endif

        /***************************************************************************/
        /*                        User profile sample code                         */
        /***************************************************************************/
        // UserProfile function
        if (GUI.Button(new Rect((nXStart + 7 * (nWidth + 10)), nYStart, nWidth, nHeight), "UserProfileIsReady", CustButton1))
        {
            if (bInit && bIsReady)
            {
                User.IsReady(UserProfileIsReadyHandler);
            }
            else
            {
                Log("Please make sure init & isReady are successful.");
            }
        }
        if (GUI.Button(new Rect((nXStart + 8 * (nWidth + 10)), nYStart, nWidth, nHeight), "UserProfile", CustButton))
        {
            if (bInit && bIsReady && bUserProfileIsReady)
            {
                Log("UserId: " + User.GetUserId());
                Log("userName: " + User.GetUserName());
                Log("userAvatarUrl: " + User.GetUserAvatarUrl());
            }
            else
            {
                Log("Please make sure init & isReady are successful.");
            }
        }

        /***************************************************************************/
        /*                          Stat sample code                               */
        /***************************************************************************/
        stringToEdit = GUI.TextField(new Rect(10, nWidth + 10, 120, 20), stringToEdit, 50);
        StatsCount = GUI.TextField(new Rect(130, nWidth + 10, 220, 20), StatsCount, 50);

        // DownloadStat function
        if (GUI.Button(new Rect(nXStart, nYStart + nWidth + 10, nWidth, nHeight), "DownloadStat", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                UserStats.DownloadStats(DownloadStatsHandler);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // UploadStat function
        if (GUI.Button(new Rect((nXStart + 1 * (nWidth + 10)), nYStart + nWidth + 10, nWidth, nHeight), "UploadStat", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                UserStats.UploadStats(UploadStatsHandler);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // GetStat function
        if (GUI.Button(new Rect(nXStart + 2 * (nWidth + 10), nYStart + nWidth + 10, nWidth, nHeight), "GetStat", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                nResult = UserStats.GetStat(stringToEdit, nInitValue);
                Viveport.Core.Logger.Log("Get " + stringToEdit + " stat name as => " + nResult);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // SetStat function
        if (GUI.Button(new Rect(nXStart + 3 * (nWidth + 10), nYStart + nWidth + 10, nWidth, nHeight), "SetStat", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                Viveport.Core.Logger.Log("MaxStep is => " + int.Parse(StatsCount));
                nResult = int.Parse(StatsCount);
                UserStats.SetStat(stringToEdit, nResult);
                Viveport.Core.Logger.Log("Set" + stringToEdit + " stat name as =>" + nResult);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        /***************************************************************************/
        /*                          Achievement sample code                        */
        /***************************************************************************/

        achivToEdit = GUI.TextField(new Rect(10, 2 * nWidth + 15, 120, 20), achivToEdit, 50);

        // GetAchievement function
        if (GUI.Button(new Rect(nXStart, nYStart + 2 * nWidth + 10, nWidth, nHeight), "GetAchieve", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                bool bAchievement = false;
                bAchievement = UserStats.GetAchievement(achivToEdit);
                Viveport.Core.Logger.Log("Get achievement => " + achivToEdit + " , and value is => " + bAchievement);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // SetAchievement function
        if (GUI.Button(new Rect(nXStart + nWidth + 10, nYStart + 2 * nWidth + 10, nWidth, nHeight), "SetAchieve", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                UserStats.SetAchievement(achivToEdit);
                Viveport.Core.Logger.Log("Set achievement => " + achivToEdit);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // ClearAchievement function
        if (GUI.Button(new Rect(nXStart + 2 * (nWidth + 10), nYStart + 2 * nWidth + 10, nWidth, nHeight), "ClearAchieve", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                UserStats.ClearAchievement(achivToEdit);
                Viveport.Core.Logger.Log("Clear achievement => " + achivToEdit);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // GetAchievementUnlockTime function
        if (GUI.Button(new Rect(nXStart + 3 * (nWidth + 10), nYStart + 2 * nWidth + 10, nWidth, nHeight), "Achieve&Time", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                int nunlockTime = 0;
                nunlockTime = UserStats.GetAchievementUnlockTime(achivToEdit);
                Viveport.Core.Logger.Log("The achievement's unlock time is =>" + nunlockTime);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

#if UNITY_ANDROID
        // GetAchievementDisplayAttribute function
        if (GUI.Button(new Rect(nXStart + 4 * (nWidth + 10), nYStart + 2 * nWidth + 10, nWidth, nHeight), "Achieve Name", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                string displayName = UserStats.GetAchievementDisplayAttribute(achivToEdit, UserStats.AchievementDisplayAttribute.Name);
                Viveport.Core.Logger.Log("The achievement's name is =>" + displayName);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // GetAchievementDisplayAttribute function
        if (GUI.Button(new Rect(nXStart + 5 * (nWidth + 10), nYStart + 2 * nWidth + 10, nWidth, nHeight), "Achieve Desctiption", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                string description = UserStats.GetAchievementDisplayAttribute(achivToEdit, UserStats.AchievementDisplayAttribute.Desc, Locale.US);
                Viveport.Core.Logger.Log("The achievement's description is =>" + description);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }
#endif

        /***************************************************************************/
        /*                          Leaderboard sample code                        */
        /***************************************************************************/

        leaderboardToEdit = GUI.TextField(new Rect(10, 3 * nWidth + 20, 160, 20), leaderboardToEdit, 150);

        // DownloadLeaderboardScores around user function
        if (GUI.Button(new Rect(nXStart, nYStart + 3 * nWidth + 20, nWidth, nHeight), "DL Around", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                UserStats.DownloadLeaderboardScores(DownloadLeaderboardHandler, leaderboardToEdit, UserStats.LeaderBoardRequestType.GlobalDataAroundUser, UserStats.LeaderBoardTimeRange.AllTime, -5, 5);
                Viveport.Core.Logger.Log("DownloadLeaderboardScores");
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // DownloadLeaderboardScores global data function
        if (GUI.Button(new Rect(nXStart + 1 * (nWidth + 10), nYStart + 3 * nWidth + 20, nWidth, nHeight), "DL not Around", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                UserStats.DownloadLeaderboardScores(DownloadLeaderboardHandler, leaderboardToEdit, UserStats.LeaderBoardRequestType.GlobalData, UserStats.LeaderBoardTimeRange.AllTime, 0, 10);
                Viveport.Core.Logger.Log("DownloadLeaderboardScores");
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        leaderboardScore = GUI.TextField(new Rect(10 + 160, 3 * nWidth + 20, 160, 20), leaderboardScore, 50);

        // UploadLeaderboardScore function
        if (GUI.Button(new Rect(nXStart + 2 * (nWidth + 10), nYStart + 3 * nWidth + 20, nWidth, nHeight), "Upload LB", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                UserStats.UploadLeaderboardScore(UploadLeaderboardScoreHandler, leaderboardToEdit, int.Parse(leaderboardScore));
                Viveport.Core.Logger.Log("UploadLeaderboardScore");
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // GetLeaderboardScoreCount function
        if (GUI.Button(new Rect(nXStart + 3 * (nWidth + 10), nYStart + 3 * nWidth + 20, nWidth, nHeight), "Get LB count", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                nResult = UserStats.GetLeaderboardScoreCount();
                Viveport.Core.Logger.Log("GetLeaderboardScoreCount=> " + nResult);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // GetLeaderboardScore function
        if (GUI.Button(new Rect(nXStart + 4 * (nWidth + 10), nYStart + 3 * nWidth + 20, nWidth, nHeight), "Get LB Score", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                int nResult = (int)UserStats.GetLeaderboardScoreCount();

                Viveport.Core.Logger.Log("GetLeaderboardScoreCount => " + nResult);

                for (int i = 0; i < nResult; i++)
                {
                    Viveport.Leaderboard lbdata;
                    lbdata = UserStats.GetLeaderboardScore(i);
                    Viveport.Core.Logger.Log("UserName = " + lbdata.UserName + ", Score = " + lbdata.Score + ", Rank = " + lbdata.Rank);
                }
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }

        }

        // GetLeaderboardSortMethod function
        if (GUI.Button(new Rect(nXStart + 5 * (nWidth + 10), nYStart + 3 * nWidth + 20, nWidth, nHeight), "Get Sort Method", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                int nResult = (int)UserStats.GetLeaderboardSortMethod();
                Viveport.Core.Logger.Log("GetLeaderboardSortMethod=> " + nResult);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // GetLeaderboardDisplayType function
        if (GUI.Button(new Rect(nXStart + 6 * (nWidth + 10), nYStart + 3 * nWidth + 20, nWidth, nHeight), "Get Disp Type", CustButton))
        {
            if (bInit == true && bIsReady == true)
            {
                int nResult = (int)UserStats.GetLeaderboardDisplayType();
                Viveport.Core.Logger.Log("GetLeaderboardDisplayType=> " + nResult);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

#if !UNITY_ANDROID
        /***************************************************************************/
        /*                         ArcadeLeaderboard sample code                   */
        /***************************************************************************/

        if (bInit == true && bArcadeIsReady == true)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.grey;

        leaderboardToEdit = GUI.TextField(new Rect(10, 4 * nWidth + 20, 160, 20), leaderboardToEdit, 150);

        // DownloadLeaderboardScores function
        if (GUI.Button(new Rect(nXStart, nYStart + 4 * nWidth + 20, nWidth, nHeight), "DL Arca LB", CustButton))
        {
            if (bInit == true && bArcadeIsReady == true)
            {
                ArcadeLeaderboard.DownloadLeaderboardScores(DownloadLeaderboardHandler, leaderboardToEdit, ArcadeLeaderboard.LeaderboardTimeRange.AllTime, 10);
                Viveport.Core.Logger.Log("DownloadLeaderboardScores");
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        leaderboardUserName = GUI.TextField(new Rect(10 + 160, 4 * nWidth + 20, 160, 20), leaderboardUserName, 150);
        leaderboardScore = GUI.TextField(new Rect(10 + 320, 4 * nWidth + 20, 160, 20), leaderboardScore, 50);

        // UploadLeaderboardScore function
        if (GUI.Button(new Rect(nXStart + 1 * (nWidth + 10), nYStart + 4 * nWidth + 20, nWidth, nHeight), "UL Arca LB", CustButton))
        {
            if (bInit == true && bArcadeIsReady == true)
            {
                ArcadeLeaderboard.UploadLeaderboardScore(UploadLeaderboardScoreHandler, leaderboardToEdit, leaderboardUserName, int.Parse(leaderboardScore));
                Viveport.Core.Logger.Log("UploadLeaderboardScore");
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

         // GetLeaderboardScoreCount function
        if (GUI.Button(new Rect(nXStart + 2 * (nWidth + 10), nYStart + 4 * nWidth + 20, nWidth, nHeight), "Get Arca Count", CustButton))
        {
            if (bInit == true && bArcadeIsReady == true)
            {
                nResult = ArcadeLeaderboard.GetLeaderboardScoreCount();
                Viveport.Core.Logger.Log("GetLeaderboardScoreCount=> " + nResult);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & Arcade isReady are successful.");
            }
        }

        // GetLeaderboardScore function
        if (GUI.Button(new Rect(nXStart + 3 * (nWidth + 10), nYStart + 4 * nWidth + 20, nWidth, nHeight), "Get Arca Score", CustButton))
        {
            if (bInit == true && bArcadeIsReady == true)
            {
                int nResult = (int)ArcadeLeaderboard.GetLeaderboardScoreCount();

                Viveport.Core.Logger.Log("GetLeaderboardScoreCount => " + nResult);

                for (int i = 0; i < nResult; i++)
                {
                    Viveport.Leaderboard lbdata;
                    lbdata = ArcadeLeaderboard.GetLeaderboardScore(i);
                    Viveport.Core.Logger.Log("UserName = " + lbdata.UserName + ", Score = " + lbdata.Score + ", Rank = " + lbdata.Rank);
                }
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }

        }

        // GetLeaderboardUserScore function
        if (GUI.Button(new Rect(nXStart + 4 * (nWidth + 10), nYStart + 4 * nWidth + 20, nWidth, nHeight), "Get AC UScore", CustButton))
        {
            if (bInit == true && bArcadeIsReady == true)
            {
                int nResult = (int)ArcadeLeaderboard.GetLeaderboardUserScore();
                Viveport.Core.Logger.Log("GetLeaderboardUserScore=> " + nResult);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

        // GetLeaderboardUserRank function
        if (GUI.Button(new Rect(nXStart + 5 * (nWidth + 10), nYStart + 4 * nWidth + 20, nWidth, nHeight), "Get AC URank", CustButton))
        {
            if (bInit == true && bArcadeIsReady == true)
            {
                int nResult = (int)ArcadeLeaderboard.GetLeaderboardUserRank();
                Viveport.Core.Logger.Log("GetLeaderboardUserRank=> " + nResult);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & isReady are successful.");
            }
        }

#endif

        if (bInit == true)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.grey;

        if (GUI.Button(new Rect(nXStart, nYStart + 5 * nWidth + 20, nWidth, nHeight), "TokenIsReady", CustButton))
        {
            if (bInit == true)
                Token.IsReady(IsTokenReadyHandler);
        }

        if (bInit == true && bTokenIsReady == true)
            GUI.contentColor = Color.white;
        else
            GUI.contentColor = Color.grey;

        if (GUI.Button(new Rect(nXStart + 1* (nWidth + 10), nYStart + 5 * nWidth + 20, nWidth, nHeight), "SessionToken", CustButton))
        {
            if (bInit == true && bTokenIsReady == true)
            {
                Token.GetSessionToken(GetSessionTokenHandler);
            }
            else
            {
                Viveport.Core.Logger.Log("Please make sure init & tokenIsReady are successful.");
            }
        }

    }

    private static void InitStatusHandler(int nResult)
    {
        if (nResult == 0)
        {
            bInit = true;
            bIsReady = false;
            bArcadeIsReady = false;
            Viveport.Core.Logger.Log("InitStatusHandler is successful");
        }
        else
        {
            // Init error, close your app and make sure your app ID is correct or not.
            bInit = false;
            Viveport.Core.Logger.Log("InitStatusHandler error : " + nResult);
        }
    }

    private static void IsReadyHandler(int nResult)
    {
        if (nResult == 0)
        {
            bIsReady = true;
            bArcadeIsReady = false;
            Viveport.Core.Logger.Log("IsReadyHandler is successful");
        }
        else
        {
            // IsReady error, close your app and make sure the viveport is launched normally.
            bIsReady = false;
            Viveport.Core.Logger.Log("IsReadyHandler error: " + nResult);
        }
    }

    private static void IsTokenReadyHandler(int nResult)
    {
        if (nResult == 0)
        {
            bTokenIsReady = true;
            Viveport.Core.Logger.Log("IsTokenReadyHandler is successful");
        }
        else
        {
            bTokenIsReady = false;
            Viveport.Core.Logger.Log("IsTokenReadyHandler error: " + nResult);
        }
    }

	private static void UserProfileIsReadyHandler(int nResult)
	{
		if (nResult == 0) {
			bUserProfileIsReady = true;
			ViveportDemo.Log("UserProfileIsReadyHandler is successful");
		} else {
			bUserProfileIsReady = false;
			ViveportDemo.Log("UserProfileIsReadyHandler error: " + nResult);
		}
	}

    private static void GetSessionTokenHandler(int nResult, string message)
    {
        if (nResult == 0)
        {
            Viveport.Core.Logger.Log("GetSessionTokenHandler is successful, token:" + message);
        }
        else
        {
            if (message.Length != 0)
            {
                Viveport.Core.Logger.Log("GetSessionTokenHandler error: " + nResult + ", message:" + message);
            }
            else
            {
                Viveport.Core.Logger.Log("GetSessionTokenHandler error: " + nResult);
            }
        }
    }

    private static void QueryRunTimeHandler(int nResult,int nMode)
    {
        if (nResult == 0)
        {
            Viveport.Core.Logger.Log("QueryRunTimeHandler is successful" + nResult + "Running mode is " + nMode);
        }
        else
        {
            Viveport.Core.Logger.Log("QueryRunTimeHandler error: " + nResult);
        }
    }

    private static void IsArcadeLeaderboardReadyHandler(int nResult)
    {
        if (nResult == 0)
        {
            bArcadeIsReady = true;
            bIsReady = false;
            Viveport.Core.Logger.Log("IsArcadeLeaderboardReadyHandler is successful");
        }
        else
        {
            // IsArcadeLeaderboardReady error, close your app and make sure viveport has switched to Arcade mode
            // by using ViveportSwitch.exe first then launched normally.
            bArcadeIsReady = false;
            Viveport.Core.Logger.Log("IsArcadeLeaderboardReadyHandler error: " + nResult);
        }
    }

    private static void ShutdownHandler(int nResult)
    {
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
    }
    private static void DownloadStatsHandler(int nResult)
    {
        if (nResult == 0)
            Viveport.Core.Logger.Log("DownloadStatsHandler is successful ");
        else
            Viveport.Core.Logger.Log("DownloadStatsHandler error: " + nResult);
    }

    private static void UploadStatsHandler(int nResult)
    {
        if (nResult == 0)
            Viveport.Core.Logger.Log("UploadStatsHandler is successful");
        else
            Viveport.Core.Logger.Log("UploadStatsHandler error: " + nResult);
    }

    private static void DownloadLeaderboardHandler(int nResult)
    {
        if (nResult == 0)
            Viveport.Core.Logger.Log("DownloadLeaderboardHandler is successful");
        else
            Viveport.Core.Logger.Log("DownloadLeaderboardHandler error: " + nResult);
    }

    private static void UploadLeaderboardScoreHandler(int nResult)
    {
        if (nResult == 0)
            Viveport.Core.Logger.Log("UploadLeaderboardScoreHandler is successful.");
        else
            Viveport.Core.Logger.Log("UploadLeaderboardScoreHandler error : " + nResult);
    }

    class MyLicenseChecker : Api.LicenseChecker
    {
        public override void OnSuccess(long issueTime, long expirationTime, int latestVersion, bool updateRequired)
        {
            Viveport.Core.Logger.Log("[MyLicenseChecker] issueTime: " + issueTime);
            Viveport.Core.Logger.Log("[MyLicenseChecker] expirationTime: " + expirationTime);
            Viveport.Core.Logger.Log("[MyLicenseChecker] latestVersion: " + latestVersion);
            Viveport.Core.Logger.Log("[MyLicenseChecker] updateRequired: " + updateRequired);
        }

        public override void OnFailure(int errorCode, string errorMessage)
        {
            Viveport.Core.Logger.Log("[MyLicenseChecker] errorCode: " + errorCode);
            Viveport.Core.Logger.Log("[MyLicenseChecker] errorMessage: " + errorMessage);
        }
    }
}
