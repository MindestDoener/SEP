using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BalanceDisplayController : MonoBehaviour
{
    private GameObject balancePanel;
    private Text _balanceText;
    private string _suffix;
    private decimal _divisor;
    private int _exp;

    void Start()
    {
        _balanceText = GetComponent<Text>();
        balancePanel = GameObject.FindWithTag("BalancePanel");
    }

    public void DisplayBalance(decimal balance) 
    {
        _balanceText.text = ShortenBalanceDisplay(balance);
        balancePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(30f + 5f * _balanceText.text.Length, 20f);
    }

    public string ShortenBalanceDisplay(decimal balance) 
    {

        if (balance >= GetPower(10, 3) && balance < GetPower(10, 6)) {
            _suffix = " K";
            _exp = 3;
        }
        else if (balance >= GetPower(10, 6) && balance < GetPower(10, 9)) 
        {
            _suffix = " M";
            _exp = 6;
        }
        else if (balance >= GetPower(10, 9) && balance < GetPower(10, 12)) 
        {
            _suffix = " B";
            _exp = 9;
        }
        else if (balance >= GetPower(10, 12) && balance < GetPower(10, 15)) 
        {
            _suffix = " T";
            _exp = 12;
        }
        else
        {
            _suffix = "";
            _exp = 0;
        }

        _divisor = GetPower(10, _exp);

        if (_exp > 0)
        {
            return Math.Round(balance/_divisor, 2) + _suffix;
        }
        else
        {
            return Math.Round(balance / _divisor) + _suffix;
        }


    }
    
    private decimal GetPower(int a, int b)
    {
        return Convert.ToDecimal(Math.Pow(a, b));
    }
}
