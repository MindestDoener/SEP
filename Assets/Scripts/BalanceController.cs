using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceController : MonoBehaviour
{
    
    private static decimal _balance;
    private GameObject TextField;
    private BalanceDisplayController bdc;
    
    // Start is called before the first frame update
    void Start()
    {
        TextField = GameObject.FindWithTag("Balance");
        bdc = (BalanceDisplayController) TextField.GetComponent(typeof(BalanceDisplayController));

    }
    
    public void AddBalance(decimal amount) 
    {
        _balance += amount;
        UpdateBalanceDisplay();
    }

    public void ResetBalance() 
    {
        _balance = 0;
        UpdateBalanceDisplay();
    }

    private void UpdateBalanceDisplay() 
    {
        bdc.DisplayBalance(_balance);
    }
}
