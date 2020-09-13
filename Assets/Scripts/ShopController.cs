using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField]
    private GameObject _buttonPrefab;

    private GameObject _player;

    private List<Button> _buttons = new List<Button>();

    private GameObject _mpDispay;

    private Camera _cam;

    private BalanceDisplayController _bdc;
    private BalanceController _bc;
    private ObjectClickController _occ;
    private MultiplierDisplayController _mdc;
    
    private float _activeMultiplier;
    
    private List<int> _upgradeClickLevel = new List<int>();
    private List<float> _multiplierIncrement = new List<float>();
    private List<decimal> _upgradeCosts = new List<decimal>();
    private List<decimal> _costIncrements = new List<decimal>();
    private List<UpgradeType> _upgradeTypes = new List<UpgradeType>();

    [SerializeField]
    private ShopItemScriptableObject[] _shopItems;



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
        _shopItems = AssignItemsToArray();
        InstantiateButtons();
    }

    public void Upgrade(int value)
    {
        if (GetBalance() >= _upgradeCosts[value] && _upgradeTypes[value] == UpgradeType.ClickUpgrade)
        {
            DoClickUpgrade(value);
        }
    }

    private void DoClickUpgrade(int value)
    {
        _upgradeClickLevel[value] += 1;
        _bc.AddBalance(-_upgradeCosts[value]);
        ModifyUpgradeCost(value);
        ModifyCurrentMultiplier(value);
        UpdateUpgradeDisplay(value);
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
        _buttons[value].GetComponentInChildren<Text>().text = String.Format(_shopItems[value].ButtonText, 
                                                                            _bdc.ShortenBalanceDisplay(_upgradeCosts[value]), 
                                                                            _upgradeClickLevel[value], 
                                                                            _multiplierIncrement[value]);;
    }

    public List<decimal> GetUpgradeCosts()
    {
        return _upgradeCosts;
    } 

    public decimal GetBalance()
    {
        return _bc.GetBalance();
    }

    public List<Button> GetButtons()
    {
        return _buttons;
    }

    private ShopItemScriptableObject[] AssignItemsToArray()
    {
        var ObjectList = Resources.LoadAll<ShopItemScriptableObject>("ShopItems");
        Array.Sort(ObjectList, (item1, item2) => item1.ButtonNumber.CompareTo(item2.ButtonNumber));
        return ObjectList;
    }

    private void InstantiateButtons()
    {
        int index = 0;
        foreach (var item in _shopItems)
        {
            var instantiatedObject = Instantiate(_buttonPrefab);
            var instanceDirections = instantiatedObject.GetComponent<RectTransform>();
            _buttons.Add(instantiatedObject.GetComponent<Button>());
            instantiatedObject.transform.SetParent(GameObject.FindWithTag("ButtonContainer").transform);
            instanceDirections.localScale = new Vector3(1f, 1f, 1f);
            _buttons[index].GetComponentInChildren<Text>().text = String.Format(item.ButtonText, 
                                                                                            _bdc.ShortenBalanceDisplay(item.UpgradeCosts), 
                                                                                            item.UpgradeLevel, 
                                                                                            item.MultiplierIncrement);
            InstantiateValues(item);
            index++;
        }
        AddingListeners();

    }

    private void InstantiateValues(ShopItemScriptableObject item)
    {
        _upgradeClickLevel.Add(item.UpgradeLevel);
        _multiplierIncrement.Add(item.MultiplierIncrement);
        _upgradeCosts.Add(Convert.ToDecimal(item.UpgradeCosts));
        _costIncrements.Add(Convert.ToDecimal(item.CostIncrements));
        _upgradeTypes.Add(item.Type);
    }

    private void AddingListeners()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            int tempInt = i;
            _buttons[i].onClick.AddListener(delegate { Upgrade(tempInt); });  
        }
            
    }

}
