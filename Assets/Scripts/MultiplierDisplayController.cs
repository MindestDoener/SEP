using UnityEngine;
using UnityEngine.UI;

public class MultiplierDisplayController : MonoBehaviour
{
    private Text _multiplierDisplay;

    // Start is called before the first frame update
    private void Start()
    {
        _multiplierDisplay = GetComponent<Text>();
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        _multiplierDisplay.text = NumberShortener.ShortenNumber(ObjectClickController.GetMultiplier()) + "x";
    }
}