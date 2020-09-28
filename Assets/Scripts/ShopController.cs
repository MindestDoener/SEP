using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;

    [SerializeField] private ShopItemScriptableObject[] shopItems;

    private readonly List<Button> _buttons = new List<Button>();

    private readonly List<decimal> _costIncrements = new List<decimal>();
    private readonly List<decimal> _multiplierIncrement = new List<decimal>();

    private readonly List<int> _upgradeClickLevel = new List<int>();
    private readonly List<decimal> _upgradeCosts = new List<decimal>();
    private readonly List<UpgradeType> _upgradeTypes = new List<UpgradeType>();

    private decimal _activeMultiplier;
    private BalanceController _bc;

    private Transform _buttonParent;
    private MultiplierDisplayController _mdc;

    private GameObject _mpDispay;

    private GameObject _player;


    private void Start()
    {
        _buttonParent = GameObject.FindWithTag("ButtonContainer").transform;
        _player = GameObject.FindWithTag("Player");
        _mpDispay = GameObject.FindWithTag("MultiplierText");
        _bc = _player.GetComponent<BalanceController>();
        _mdc = _mpDispay.GetComponent<MultiplierDisplayController>();
        _activeMultiplier = ObjectClickController.GetMultiplier();
        shopItems = AssignItemsToArray();
        InstantiateButtons();
    }

    public void Upgrade(int value)
    {
        if (GetBalance() >= _upgradeCosts[value] && _upgradeTypes[value] == UpgradeType.ClickUpgrade)
            DoClickUpgrade(value);
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
        ObjectClickController.IncreaseMultiplier(newMultiplier);
        _activeMultiplier = newMultiplier;
    }

    private void ModifyUpgradeCost(int value)
    {
        _upgradeCosts[value] = Math.Round(_upgradeCosts[value] * _costIncrements[value]);
    }

    private void UpdateUpgradeDisplay(int value)
    {
        _buttons[value].GetComponentInChildren<Text>().text = string.Format(shopItems[value].ButtonText,
            NumberShortener.ShortenNumber(_upgradeCosts[value]),
            _upgradeClickLevel[value],
            _multiplierIncrement[value]);
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
        var objectList = Resources.LoadAll<ShopItemScriptableObject>("ShopItems");
        Array.Sort(objectList, (item1, item2) => item1.ButtonNumber.CompareTo(item2.ButtonNumber));
        return objectList;
    }

    private void InstantiateButtons()
    {
        var index = 0;
        foreach (var item in shopItems)
        {
            var instantiatedObject = Instantiate(buttonPrefab, _buttonParent);
            var instanceDirections = instantiatedObject.GetComponent<RectTransform>();
            _buttons.Add(instantiatedObject.GetComponent<Button>());
            instanceDirections.localScale = new Vector3(1f, 1f, 1f);
            _buttons[index].GetComponentInChildren<Text>().text = string.Format(item.ButtonText,
                NumberShortener.ShortenNumber(item.UpgradeCosts),
                item.UpgradeLevel,
                item.MultiplierIncrement);
            InstantiateValues(item);
            SetButtonsItemImage(item, _buttons[index]);
            index++;
        }

        AddingListeners();
    }

    private void InstantiateValues(ShopItemScriptableObject item)
    {
        _upgradeClickLevel.Add(item.UpgradeLevel);
        _multiplierIncrement.Add(Convert.ToDecimal(item.MultiplierIncrement));
        _upgradeCosts.Add(Convert.ToDecimal(item.UpgradeCosts));
        _costIncrements.Add(Convert.ToDecimal(item.CostIncrements));
        _upgradeTypes.Add(item.Type);
    }

    private void AddingListeners()
    {
        for (var i = 0; i < _buttons.Count; i++)
        {
            var tempInt = i;
            _buttons[i].onClick.AddListener(delegate { Upgrade(tempInt); });
        }
    }

    private void SetButtonsItemImage(ShopItemScriptableObject item, Button button)
    {
        var itemImage = button.transform.GetChild(1).gameObject;
        itemImage.GetComponent<Image>().sprite = item.ItemImage;
    }
}