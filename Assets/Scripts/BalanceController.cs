using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceController : MonoBehaviour
{
    
    private static decimal _balance;
    GameObject TextField;
    BalanceDisplayController bdc;
    
    // Start is called before the first frame update
    void Start()
    {
        TextField = GameObject.FindWithTag("Balance");
        bdc = (BalanceDisplayController) TextField.GetComponent(typeof(BalanceDisplayController));

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddBalance(decimal amount) 
    {
        _balance += amount;
        UpdateBalanceDisplay();
    }

    private void UpdateBalanceDisplay() 
    {
        bdc.DisplayBalance(_balance);
    }
}
