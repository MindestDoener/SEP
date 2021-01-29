using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private Button rewardAdButton;
    
    public string myPlacementId = "rewardedVideo";
    
    // Start is called before the first frame update
    void Start()
    {
        rewardAdButton = gameObject.GetComponent<Button>();
        rewardAdButton.interactable = Advertisement.IsReady("rewardedVideo");
        if (rewardAdButton) rewardAdButton.onClick.AddListener(DisplayAd);
        
        Advertisement.AddListener(this);
        Advertisement.Initialize("3826605", true);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Advertisement.IsReady("rewardedVideo"));
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
        throw new System.NotImplementedException();
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
}
