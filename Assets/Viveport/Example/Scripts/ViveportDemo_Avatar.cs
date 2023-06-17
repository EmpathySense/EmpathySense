using System.Collections.Generic;
using UnityEngine;

public class ViveportDemo_Avatar : MonoBehaviour
{
    private int _left = 10, _top = 35;
#if UNITY_ANDROID
    private int _width = 180, _height = 100;
#else
    private int _width = 140, _height = 40;
#endif
    private bool _hasAvatarList = false;
    private List<Viveport.Avatar.AvatarData> _avatarList;

    private void OnGUI()
    {
        var style = new GUIStyle("button");
#if UNITY_ANDROID
        style.fontSize = 23;
#endif

        // GetAvatarList
        if (GUI.Button(new Rect(_left, _top, _width, _height), "GetAvatarList", style))
        {
            Viveport.Avatar.GetAvatarList(GetAvatarListCallback);
        }

        GUI.contentColor = _hasAvatarList ? Color.white : Color.gray;

        // DownloadAvatar(ID)
        if (GUI.Button(new Rect(_left + 1 * (_width + 10), _top, _width, _height), "DownloadAvatar(ID)", style))
        {
            if (_hasAvatarList)
            {
                foreach (var avatar in _avatarList)
                {
                    if (avatar.IsCurrent)
                    {
                        Viveport.Avatar.DownloadAvatar(DownloadAvatarCallback, avatar.Id);
                        break;
                    }
                }
            }
        }

        // DownloadAvatar(URL)
        if (GUI.Button(new Rect(_left + 2 * (_width + 10), _top, _width, _height), "DownloadAvatar(URL)", style))
        {
            if (_hasAvatarList)
            {
                foreach (var avatar in _avatarList)
                {
                    if (avatar.IsCurrent)
                    {
                        Viveport.Avatar.DownloadAvatar(DownloadAvatarCallback, avatar.VrmBinaryDataUrl);
                        break;
                    }
                }
            }
        }
    }

    private void GetAvatarListCallback(int result, string message)
    {
        if (result == 0)
        {
            Viveport.Core.Logger.Log("GetAvatarList success");
            Viveport.Core.Logger.Log("Avatars JSON string: " + message);
            Viveport.Core.Logger.Log("Avatars:");

            _avatarList = Viveport.Avatar.ParseAvatarList(message);

            foreach (var avatar in _avatarList)
            {
                Viveport.Core.Logger.Log("    Id: " + avatar.Id);
                Viveport.Core.Logger.Log("        ViveportId: " + avatar.ViveportId);
                Viveport.Core.Logger.Log("        DataType: " + avatar.DataType);
                Viveport.Core.Logger.Log("        Data: " + avatar.Data);
                Viveport.Core.Logger.Log("        MetaData: " + avatar.MetaData);
                Viveport.Core.Logger.Log("        S3KeyBin: " + avatar.S3KeyBin);
                Viveport.Core.Logger.Log("        S3KeySnapshot: " + avatar.S3KeySnapshot);
                Viveport.Core.Logger.Log("        S3KeyHeadIcon: " + avatar.S3KeyHeadIcon);
                Viveport.Core.Logger.Log("        S3KeyVrmBin: " + avatar.S3KeyVrmBin);
                Viveport.Core.Logger.Log("        BinaryDataUrl: " + avatar.BinaryDataUrl);
                Viveport.Core.Logger.Log("        VrmBinaryDataUrl: " + avatar.VrmBinaryDataUrl);
                Viveport.Core.Logger.Log("        SnapshotDataUrl: " + avatar.SnapshotDataUrl);
                Viveport.Core.Logger.Log("        HeadIconDataUrl: " + avatar.HeadIconDataUrl);
                Viveport.Core.Logger.Log("        UpdateTimeUtcInSec: " + avatar.UpdateTimeUtcInSec);
                Viveport.Core.Logger.Log("        CreateTimeUtcInMilli: " + avatar.CreateTimeUtcInMilli);
                Viveport.Core.Logger.Log("        UpperBodyNft: " + avatar.UpperBodyNft);
                Viveport.Core.Logger.Log("        LowerBodyNft: " + avatar.LowerBodyNft);
                Viveport.Core.Logger.Log("        FootNft: " + avatar.FootNft);
                Viveport.Core.Logger.Log("        IsEncrypted: " + avatar.IsEncrypted);
                Viveport.Core.Logger.Log("        IsCurrent: " + avatar.IsCurrent);
            }

            _hasAvatarList = true;
        }
        else
        {
            Viveport.Core.Logger.Log("GetAvatarList failure");
            Viveport.Core.Logger.Log("Error code: " + result);
            Viveport.Core.Logger.Log("Error message: " + message);

            _hasAvatarList = false;
        }
    }

    private void DownloadAvatarCallback(int result, string message)
    {
        if (result == 0)
        {
            Viveport.Core.Logger.Log("DownloadAvatar success");
            Viveport.Core.Logger.Log("Avatar file path: " + message);
        }
        else
        {
            Viveport.Core.Logger.Log("DownloadAvatar failure");
            Viveport.Core.Logger.Log("Error code: " + result);
            Viveport.Core.Logger.Log("Error message: " + message);
        }
    }
}
