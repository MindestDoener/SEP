using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierDisplayController : MonoBehaviour
{
    private Camera _cam;

    private ObjectClickController _occ;
    private BalanceDisplayController _bdc;
    private string _suffix;
    private decimal _divisor;
    private int _exp;

    private Text _multiplierDisplay;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _multiplierDisplay = GetComponent<Text>();
        _occ = _cam.GetComponent<ObjectClickController>();
    }

    // Update is called once per frame

    public void UpdateDisplay()
    {
        _multiplierDisplay.text = ShortenMultiplierDisplay(_occ.GetMultiplier()) + "x";
    }
    
    public string ShortenMultiplierDisplay(decimal multiplier) 
    {

        if (multiplier >= GetPower(10, 3) && multiplier < GetPower(10, 6)) {
            _suffix = "K";
            _exp = 3;
        }
        else if (multiplier >= GetPower(10, 6) && multiplier < GetPower(10, 9)) 
        {
            _suffix = "M";
            _exp = 6;
        }
        else if (multiplier >= GetPower(10, 9) && multiplier < GetPower(10, 12)) 
        {
            _suffix = "B";
            _exp = 9;
        }
        else if (multiplier >= GetPower(10, 12) && multiplier < GetPower(10, 15)) 
        {
            _suffix = "T";
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
            return Math.Round(multiplier/_divisor, 2) + " " + _suffix;
        }
        else
        {
            return (multiplier / _divisor) + " " + _suffix;
        }


    }
    
    private decimal GetPower(int a, int b)
    {
        return Convert.ToDecimal(Math.Pow(a, b));
    }
}
