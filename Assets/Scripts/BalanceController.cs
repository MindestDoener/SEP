using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceController : MonoBehaviour
{
    
    private static decimal _balance = 0;
    [SerializeField]
    private GameObject _balanceTextField;
    private BalanceDisplayController _bdc;
    
    // Start is called before the first frame update
    void Start()
    {
        _bdc = (BalanceDisplayController) _balanceTextField.GetComponent(typeof(BalanceDisplayController));
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
        _bdc.DisplayBalance(_balance);
    }

    public decimal GetBalance()
    {
        return _balance;
    }
}
