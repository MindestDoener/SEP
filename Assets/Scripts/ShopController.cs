using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private Button upgradeClickButton;
    [SerializeField]
    private Text upgradeClickButtonText;
    private decimal _upgradeClickCost = 1000;
    private int _upgradeClickLevel = 0;
    
    private BalanceDisplayController _bdc;
    private BalanceController _bc;
   
    [SerializeField]
    private Camera _cam;
    
    private ObjectClickController _occ;
    
    [SerializeField]
    private GameObject _player;

    [SerializeField] 
    private GameObject _mpDispay;
    private MultiplierDisplayController _mdc;
    private float _activeMultiplier;
    private const float _multiplierIncrement = 0.5f;
    
    private const int _costIncrement = 2;
    
    
    void Start()
    {
        _bdc = GetComponent<BalanceDisplayController>();
        upgradeClickButtonText.text = "Upgrade Click: " + _upgradeClickCost + "(" + _upgradeClickLevel + ")";
        upgradeClickButton.onClick.AddListener(UpgradeClick);
        _bc = _player.GetComponent<BalanceController>();
        _occ = _cam.GetComponent<ObjectClickController>();
        _mdc = _mpDispay.GetComponent<MultiplierDisplayController>();
        _activeMultiplier = _occ.GetMultiplier();
    }

    private void UpgradeClick()
    {
        if (_bc.GetBalance() >= _upgradeClickCost)
        {
            DoUpgrade();
        }
    }

    private void DoUpgrade()
    {
        _upgradeClickLevel += 1;
        _bc.AddBalance(-_upgradeClickCost);
        ModifyUpgradeCost();
        ModifyCurrentMultiplier();
        UpdateShopDisplay();
        _mdc.UpdateDisplay();
    }

    private void ModifyCurrentMultiplier()
    {
        var newMultiplier = _activeMultiplier + _multiplierIncrement;
        _occ.IncreaseMultiplier((newMultiplier));
        _activeMultiplier = newMultiplier;
    }

    private void ModifyUpgradeCost()
    {
        _upgradeClickCost = _upgradeClickCost * _costIncrement;
    }

    private void UpdateShopDisplay()
    {
        upgradeClickButtonText.text = "Upgrade Click: " + _bdc.ShortenBalanceDisplay(_upgradeClickCost) + " (" + _upgradeClickLevel + ")";
    }
}
