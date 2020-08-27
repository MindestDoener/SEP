using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BalanceDisplayController : MonoBehaviour
{
    Text balanceText;
    private string _suffix;
    private decimal _divisor;
    private int _exp;  

    // Start is called before the first frame update
    void Start()
    {
        balanceText = GetComponent<Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayBalance(decimal balance) 
    {
        balanceText.text = ShortenBalanceDisplay(balance);
    }

    private string ShortenBalanceDisplay(decimal balance) 
    {
        _divisor = Convert.ToDecimal(Math.Pow(10, _exp));

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

        return Math.Round(balance/_divisor, 2).ToString() + _suffix;
       


    }
}
