using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTableController : MonoBehaviour
{
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private Sprite CurrentPlayerBackground;
    private GameObject row;
    string PlayfabID;
    void Start()
    {

        PlayfabID = GameObject.FindWithTag("MainCamera").GetComponent<PlayfabManager>().GetPlayfabID();
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "TrashScore",
            StartPosition = 0,
            MaxResultsCount = 20
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    void OnError(PlayFabError error)
    {
        Debug.Log("Error while getting the leaderboard data!");
        Debug.Log(error.GenerateErrorReport());
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (Transform child in this.transform.GetChild(4).GetChild(0))
        {
            GameObject.Destroy(child.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            row = Instantiate(ItemPrefab, this.transform.GetChild(4).GetChild(0));
            row.transform.GetChild(0).GetComponent<Text>().text = (item.Position + 1).ToString();
            row.transform.GetChild(1).GetComponent<Text>().text = item.PlayFabId.ToString();
            row.transform.GetChild(2).GetComponent<Text>().text = item.StatValue.ToString();
            if (PlayfabID == item.PlayFabId)
            {
                row.GetComponent<Image>().sprite = CurrentPlayerBackground;
            }
        }
    }
 
}
