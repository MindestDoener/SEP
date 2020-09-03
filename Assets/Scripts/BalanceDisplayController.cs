using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BalanceDisplayController : MonoBehaviour
{
    private Text _balanceText;
    private string _suffix;
    private decimal _divisor;
    private int _exp;  

    void Start()
    {
        _balanceText = GetComponent<Text>(); 
    }

    public void DisplayBalance(decimal balance) 
    {
        _balanceText.text = ShortenBalanceDisplay(balance);
    }

    private string ShortenBalanceDisplay(decimal balance) 
    {

        if (balance >= Convert.ToDecimal(Math.Pow(10, 3)) && balance < Convert.ToDecimal(Math.Pow(10, 6))) {
            _suffix = " K";
            _exp = 3;
        }
        else if (balance >= Convert.ToDecimal(Math.Pow(10, 6)) && balance < Convert.ToDecimal(Math.Pow(10, 9))) 
        {
            _suffix = " M";
            _exp = 6;
        }
        else if (balance >= Convert.ToDecimal(Math.Pow(10, 9)) && balance < Convert.ToDecimal(Math.Pow(10, 12))) 
        {
            _suffix = " B";
            _exp = 9;
        }
        else if (balance >= Convert.ToDecimal(Math.Pow(10, 12)) && balance < Convert.ToDecimal(Math.Pow(10, 15))) 
        {
            _suffix = " T";
            _exp = 12;
        }

        _divisor = Convert.ToDecimal(Math.Pow(10, _exp));

        return Math.Round(balance/_divisor, 2).ToString() + _suffix;
       

    }
}
