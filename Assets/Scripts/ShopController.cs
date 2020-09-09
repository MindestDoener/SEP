using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private Text upgradeButtonText;

    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _mpDispay;

    private Camera _cam;

    private BalanceDisplayController _bdc;
    private BalanceController _bc;
    private ObjectClickController _occ;
    private MultiplierDisplayController _mdc;

    private int[] _upgradeClickLevel = {0};

    private float _activeMultiplier;
    
    private readonly float[] _multiplierIncrement = {0.5f};
    private decimal[] _upgradeCosts =  {1000m};
    private int[] _costIncrements = {2};

    private string[] _upgradeTexts =
    {
        "Click Upgrade: "
    };

    private int _upgradeButtonNumber;

    
    
    void Start()
    {
        _cam = Camera.main;
        _bdc = GetComponent<BalanceDisplayController>();
        _bc = _player.GetComponent<BalanceController>();
        _occ = _cam.GetComponent<ObjectClickController>();
        _mdc = _mpDispay.GetComponent<MultiplierDisplayController>();
        _activeMultiplier = _occ.GetMultiplier();
    }

    public void Upgrade(int upgradeNumber)
    {
        if (_bc.GetBalance() >= _upgradeCosts[upgradeNumber])
        {
            DoUpgrade(upgradeNumber);
        }
    }

    private void DoUpgrade(int upgradeNumber)
    {
        _upgradeClickLevel[upgradeNumber] += 1;
        _bc.AddBalance(-_upgradeCosts[upgradeNumber]);
        ModifyUpgradeCost(upgradeNumber);
        ModifyCurrentMultiplier(upgradeNumber);
        UpdateUpgradeDisplay(upgradeNumber);
        _mdc.UpdateDisplay();
    }

    private void ModifyCurrentMultiplier(int value)
    {
        var newMultiplier = _activeMultiplier + _multiplierIncrement[value];
        _occ.IncreaseMultiplier((newMultiplier));
        _activeMultiplier = newMultiplier;
    }

    private void ModifyUpgradeCost(int value)
    {
        _upgradeCosts[value] = _upgradeCosts[value] * _costIncrements[value];
    }

    private void UpdateUpgradeDisplay(int value)
    {
        upgradeButtonText.text = _upgradeTexts[value] 
                                 + _bdc.ShortenBalanceDisplay(_upgradeCosts[value]) 
                                 + " (lvl. " + _upgradeClickLevel[value] + ")" 
                                 + " +" + _multiplierIncrement[value] + "x" ;
    }

    public void GetButtonText(Text buttonText)
    {
        upgradeButtonText = buttonText;
    }
    
}
