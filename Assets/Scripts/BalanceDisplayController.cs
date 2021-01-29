using UnityEngine;
using UnityEngine.UI;

public class BalanceDisplayController : MonoBehaviour
{
    private Text _balanceText;
    private GameObject _balancePanel;

    private void Start()
    {
        _balanceText = GetComponent<Text>();
        _balancePanel = GameObject.FindWithTag("BalancePanel");
    }

    public void DisplayBalance(float balance)
    {
        _balanceText.text = NumberShortener.ShortenNumber(balance);
        _balancePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(30f + 5f * _balanceText.text.Length, 20f);
    }
}