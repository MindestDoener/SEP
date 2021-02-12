using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelController : MonoBehaviour
{
   // private GameObject _autoRateDisplay;
    private GameObject _circleRenderer;
    private bool _initialEnable = true;

    private void OnEnable()
    {
        if (!_initialEnable)
        {
            _circleRenderer.GetComponent<CircleRendererController>().Rendered = true;
            //_autoRateDisplay.SetActive(true);
           // _autoRateDisplay.GetComponent<Text>().text =
             //   "Autocollect Rate: every " + Math.Round(GameData.AutoCollectRate, 1) + "s";
        }
        else
        {
            //_autoRateDisplay = GameObject.FindWithTag("AutoCollectRateDisplay");
            _circleRenderer = GameObject.FindWithTag("CircleRenderer");
            _initialEnable = false;
        }
    }

    private void OnDisable()
    {
        //_autoRateDisplay.SetActive(false);
        if (!(_circleRenderer is null)) _circleRenderer.GetComponent<CircleRendererController>().Rendered = false;
    }
}