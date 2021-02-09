using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private Button rewardAdButton;
    private GameObject _rewardText;

    public string myPlacementId = "rewardedVideo";

    void Start()
    {
        _rewardText = GameObject.FindWithTag("RewardAd");
        rewardAdButton = gameObject.GetComponent<Button>();
        rewardAdButton.interactable = Advertisement.IsReady("rewardedVideo");
        if (rewardAdButton) rewardAdButton.onClick.AddListener(DisplayAd);

        Advertisement.AddListener(this);
        Advertisement.Initialize("3826605", true);
        _rewardText.SetActive(false);
    }

    public void DisplayAd()
    {
        Debug.Log("Here an ad should be displayed");
        Advertisement.Show(myPlacementId);
    }

    public void OnUnityAdsDidError(string message)
    {
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            getReward();
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == myPlacementId)
        {
            rewardAdButton.interactable = true;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
    }

    private float shuffleReward()
    {
        return Random.Range(1.0f, 10.0f);
    }

    private void getReward()
    {
        var reward = 0f;
        if (GameData.Balance <= 1000)
        {
            reward = 10000;
        }
        else
        {
            reward = shuffleReward() * GameData.Balance;
        }

        GameData.Balance += reward;
        _rewardText.SetActive(true);
        _rewardText.GetComponent<Text>().text = "+" + NumberShortener.ShortenNumber(reward);
        StartCoroutine(cooldown());
    }

    IEnumerator cooldown()
    {
        rewardAdButton.interactable = false;
        Advertisement.RemoveListener(this);
        yield return new WaitForSeconds(5);
        _rewardText.SetActive(false);
        yield return new WaitForSeconds(85);
        Advertisement.AddListener(this);
        rewardAdButton.interactable = true;
    }
}