using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private GameObject _player;
    private Button rewardAdButton;
    private BalanceController _balance;
    private GameObject _rewardText;
    
    public string myPlacementId = "rewardedVideo";
    
    void Start()
    {
        _rewardText = GameObject.FindWithTag("RewardAd");
        _player = GameObject.FindWithTag("Player");
        _balance = (BalanceController) _player.GetComponent(typeof(BalanceController));
        rewardAdButton = gameObject.GetComponent<Button>();
        rewardAdButton.interactable = Advertisement.IsReady("rewardedVideo");
        if (rewardAdButton) rewardAdButton.onClick.AddListener(DisplayAd);
        
        Advertisement.AddListener(this);
        Advertisement.Initialize("3826605", true);
        _rewardText.SetActive(false);

    }
    
    void Update()
    {
    }

    public void DisplayAd()
    {
        Debug.Log("Here an ad should be displayed");
        Advertisement.Show(myPlacementId);
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
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
        if (placementId == myPlacementId) {        
            rewardAdButton.interactable = true;
        }
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    private decimal shuffleReward()
    {
       return Convert.ToDecimal(Random.Range(1.0f, 10.0f));
    }

    private void getReward()
    {
        var reward = 0m;
        if (_balance.GetBalance() <= 1000m)
        {
            reward = 10000m;
        }
        else
        {
            reward = shuffleReward() * _balance.GetBalance();
        }
        _balance.AddBalance(reward);
        _rewardText.SetActive(true);
        _rewardText.GetComponent<Text>().text = "+" + NumberShortener.ShortenNumber(reward) + " c";
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
