using UnityEngine;
using UnityEngine.UI;

public class StatDisplayController : MonoBehaviour
{
    private GameObject _clickMultiplierDisplay;
    private GameObject _autoCollectMultiplierDisplay;
    private GameObject _autoCollectRateDisplay;
    private GameObject _autoCollectRangeDisplay;
    private GameObject _balanceDisplay;
    
    // Start is called before the first frame update
    private void Start()
    {
        _clickMultiplierDisplay = GameObject.FindWithTag("MultiplierDisplay");
        _autoCollectMultiplierDisplay = GameObject.FindWithTag("AutoCollectMultiplierDisplay");
        _autoCollectRateDisplay = GameObject.FindWithTag("AutoCollectRateDisplay");
        _autoCollectRangeDisplay = GameObject.FindWithTag("AutoCollectRangeDisplay");
        _balanceDisplay = GameObject.FindWithTag("Balance");
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        _clickMultiplierDisplay.GetComponent<Text>().text =
            NumberShortener.ShortenNumber(GameData.ClickMultiplier, false) + "x";
        _autoCollectMultiplierDisplay.GetComponent<Text>().text =
            NumberShortener.ShortenNumber(GameData.AutoMultiplier, false) + "x";
        _autoCollectRateDisplay.GetComponent<Text>().text =
            NumberShortener.ShortenNumber(GameData.AutoCollectRate, false) + "s";
        _autoCollectRangeDisplay.GetComponent<Text>().text =
            NumberShortener.ShortenNumber(GameData.AutoCollectRange, false) + "x";
        _balanceDisplay.GetComponent<Text>().text = NumberShortener.ShortenNumber(GameData.Balance);
    }
}