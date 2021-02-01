using UnityEngine;

public class BalanceController : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _balanceTextField;
    private BalanceDisplayController _bdc;

    // Start is called before the first frame update
    void Start()
    {
        _bdc = (BalanceDisplayController) _balanceTextField.GetComponent(typeof(BalanceDisplayController));
    }
    
    public void AddBalance(float amount) 
    {
        GameData.Balance += amount;
        UpdateBalanceDisplay();
    }

    public void ResetBalance() 
    {
        GameData.Balance = 0;
        UpdateBalanceDisplay();
    }

    private void UpdateBalanceDisplay() 
    {
        _bdc.DisplayBalance(GameData.Balance);
    }

    public float GetBalance()
    {
        return GameData.Balance;
    }
}
