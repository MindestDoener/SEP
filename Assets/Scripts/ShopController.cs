﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private Text upgradeButtonText;

    private GameObject _player;

    private GameObject _mpDispay;

    private Camera _cam;

    private BalanceDisplayController _bdc;
    private BalanceController _bc;
    private ObjectClickController _occ;
    private MultiplierDisplayController _mdc;

    private int[] _upgradeClickLevel = {0, 0, 0};

    private float _activeMultiplier;
    
    private readonly float[] _multiplierIncrement = {0.5f, 2f, 5f};
    private decimal[] _upgradeCosts =  {1000m, 5000m, 10000m};
    private decimal[] _costIncrements = {1.5m, 2m, 2.5m};

    private string[] _upgradeTexts =
    {
        "Click Upgrade: {0}c (lvl. {1}) [+{2}x]",
        "Click Upgrade: {0}c (lvl. {1}) [+{2}x]",
        "Click Upgrade: {0}c (lvl. {1}) [+{2}x]"
    };

    private int _upgradeButtonNumber;

    
    
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _mpDispay = GameObject.FindWithTag("MultiplierText");
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
        _upgradeCosts[value] = Math.Round(_upgradeCosts[value] * _costIncrements[value]);
    }

    private void UpdateUpgradeDisplay(int value)
    {
        upgradeButtonText.text = String.Format(_upgradeTexts[value], _bdc.ShortenBalanceDisplay(_upgradeCosts[value]), _upgradeClickLevel[value], _multiplierIncrement[value]);
    }

    public void GetButtonText(Text buttonText)
    {
        upgradeButtonText = buttonText;
    }

    public decimal[] GetUpgradeCosts()
    {
        return _upgradeCosts;
    }

    public decimal GetBalance()
    {
        return _bc.GetBalance();
    }

}
