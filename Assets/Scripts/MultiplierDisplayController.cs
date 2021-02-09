using UnityEngine;
using UnityEngine.UI;

public class MultiplierDisplayController : MonoBehaviour
{
    private GameObject _clickMultiplierDisplay;
    private GameObject _autoCollectMultiplierDisplay;
    private GameObject _autoCollectRateDisplay;
    private GameObject _autoCollectRangeDisplay;
    
    // Start is called before the first frame update
    private void Start()
    {
        _clickMultiplierDisplay = GameObject.FindWithTag("MultiplierDisplay");
        _autoCollectMultiplierDisplay = GameObject.FindWithTag("AutoCollectMultiplierDisplay");
        _autoCollectRateDisplay = GameObject.FindWithTag("AutoCollectRateDisplay");
        _autoCollectRangeDisplay = GameObject.FindWithTag("AutoCollectRangeDisplay");
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        _clickMultiplierDisplay.GetComponent<Text>().text =
            GameData.ClickMultiplier + "x";
        _autoCollectMultiplierDisplay.GetComponent<Text>().text =
            GameData.AutoMultiplier + "x";
        _autoCollectRateDisplay.GetComponent<Text>().text =
            GameData.AutoCollectRate + "s";
        _autoCollectRangeDisplay.GetComponent<Text>().text =
            GameData.AutoCollectRange + "x";
    }
}