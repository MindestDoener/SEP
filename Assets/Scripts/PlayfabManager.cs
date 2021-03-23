using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    private string MyPlayfabID;
    void Start()
    {
        Login();
    }

    // Update is called once per frame
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result) {
        Debug.Log("Successful login/account create!");
        GetAccountInfo();
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
    }

    void OnError(PlayFabError error) {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderboard()
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "TrashScore",
                    Value = GetTrashItemCount()
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Successfull leaderboard sent");
    }

    private int GetTrashItemCount()
    {
        int sum = 0;
        foreach (var list in TrashManager.GetAllTrashItems().Values)
        {
            foreach (var trash in list)
            {
                sum += Convert.ToInt32(trash.Count);
            }
        }
        return sum;
    }
    public string GetPlayfabID()
    {
        return MyPlayfabID;
    }

    void GetAccountInfo()
    {
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, Success, Fail);
    }

    void Success(GetAccountInfoResult result)
    {
        MyPlayfabID = result.AccountInfo.PlayFabId;
        Debug.Log(MyPlayfabID);
    }

    void Fail(PlayFabError error) { Debug.LogError(error.GenerateErrorReport()); }
}
