using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    private readonly List<Button> _buttons = new List<Button>();
    private BalanceController _bc;
    private Transform _buttonParent;
    private StatDisplayController _mdc;
    private GameObject _statContainer;
    private GameObject _player;
    private List<ShopItemScriptableObject> shopItems;

    private void Start()
    {
        _buttonParent = GameObject.FindWithTag("ButtonContainer").transform;
        _player = GameObject.FindWithTag("Player");
        _statContainer = GameObject.FindWithTag("StatContainer");
        _bc = _player.GetComponent<BalanceController>();
        _mdc = _statContainer.GetComponent<StatDisplayController>();
        if (GameData.ShopItems is null) GameData.ShopItems = AssignItemsToArray();

        shopItems = GameData.ShopItems;
        InstantiateButtons();
    }

    private void Upgrade(int value)
    {
        var upgrade = shopItems[value];
        if (GetBalance() >= upgrade.UpgradeCosts)
            switch (upgrade.Type)
            {
                case UpgradeType.ClickMultiplierUpgrade:
                    DoClickUpgrade(upgrade);
                    break;
                case UpgradeType.AutocollectMultiplierUpgrade:
                    DoAutocollectUpgrade(upgrade);
                    break;
                case UpgradeType.AutocollectRateUpgrade:
                    DoAutocollectRateUpgrade(upgrade);
                    break;
                case UpgradeType.AutocollectRangeUpgrade:
                    DoAutocollectRangeUpgrade(upgrade);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        _mdc.UpdateDisplay();
    }

    private void DoAutocollectRangeUpgrade(ShopItemScriptableObject upgrade)
    {
        upgrade.UpgradeLevel++;
        _bc.AddBalance(-upgrade.UpgradeCosts);
        ModifyUpgradeCost(upgrade);
        GameData.AutoCollectRange *= upgrade.MultiplierIncrement;
        UpdateUpgradeDisplay(upgrade);
        GameObject.FindWithTag("CircleRenderer").GetComponent<CircleRendererController>().UpdateCircle();
    }

    private void DoAutocollectRateUpgrade(ShopItemScriptableObject upgrade)
    {
        upgrade.UpgradeLevel++;
        _bc.AddBalance(-upgrade.UpgradeCosts);
        ModifyUpgradeCost(upgrade);
        GameData.AutoCollectRate *= 1 - upgrade.MultiplierIncrement;
        UpdateUpgradeDisplay(upgrade);
    }

    private void DoClickUpgrade(ShopItemScriptableObject upgrade)
    {
        upgrade.UpgradeLevel++;
        _bc.AddBalance(-upgrade.UpgradeCosts);
        ModifyUpgradeCost(upgrade);
        GameData.ClickMultiplier += upgrade.MultiplierIncrement;
        UpdateUpgradeDisplay(upgrade);
    }

    private void DoAutocollectUpgrade(ShopItemScriptableObject upgrade)
    {
        upgrade.UpgradeLevel++;
        _bc.AddBalance(-upgrade.UpgradeCosts);
        ModifyUpgradeCost(upgrade);
        GameData.AutoMultiplier += upgrade.MultiplierIncrement;
        UpdateUpgradeDisplay(upgrade);
    }

    private void ModifyUpgradeCost(ShopItemScriptableObject upgrade)
    {
        upgrade.UpgradeCosts = (float) Math.Round(upgrade.UpgradeCosts * upgrade.CostIncrements);
    }

    private void UpdateUpgradeDisplay(ShopItemScriptableObject upgrade) // TODO: can be simplified
    {
        _buttons[shopItems.IndexOf(upgrade)].transform.GetChild(0).GetChild(0).GetComponent<Text>().text =
            NumberShortener.ShortenNumber(upgrade.UpgradeCosts);
        _buttons[shopItems.IndexOf(upgrade)].transform.GetChild(4).GetComponent<Text>().text =
            upgrade.UpgradeLevel + "";
    }

    public List<float> GetUpgradeCosts()
    {
        return shopItems.ConvertAll(item => item.UpgradeCosts);
    }

    public float GetBalance()
    {
        return _bc.GetBalance();
    }

    public List<Button> GetButtons()
    {
        return _buttons;
    }

    public static List<ShopItemScriptableObject> AssignItemsToArray()
    {
        var objectList = Resources.LoadAll<ShopItemScriptableObject>("ShopItems");
        Array.Sort(objectList, (item1, item2) => item1.UpgradeCosts.CompareTo(item2.UpgradeCosts));
        return objectList.ToList().ConvertAll(Instantiate);
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
            _buttons[index].transform.GetChild(0).GetChild(0).GetComponent<Text>().text =
                NumberShortener.ShortenNumber(item.UpgradeCosts);
            _buttons[index].transform.GetChild(1).GetComponent<Text>().text =
                item.ButtonText;
            _buttons[index].transform.GetChild(4).GetComponent<Text>().text = 
                item.UpgradeLevel + "";

            switch (item.Type)
            {
                case UpgradeType.ClickMultiplierUpgrade:
                case UpgradeType.AutocollectMultiplierUpgrade:
                    _buttons[index].transform.GetChild(2).GetComponent<Text>().text = "+" + item.MultiplierIncrement + "x";
                    break;
                case UpgradeType.AutocollectRangeUpgrade:
                    _buttons[index].transform.GetChild(2).GetComponent<Text>().text = "x" + item.MultiplierIncrement;
                    break;
                case UpgradeType.AutocollectRateUpgrade:
                    _buttons[index].transform.GetChild(2).GetComponent<Text>().text = "x" + item.MultiplierIncrement;
                    break;
            }
            
            SetButtonsItemImage(item, _buttons[index]);
            index++;
        }

        AddingListeners();
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
        var itemImage = button.transform.GetChild(3).gameObject;
        itemImage.GetComponent<Image>().sprite = item.ItemImage;
        itemImage.GetComponent<Image>().color = item.Color;
    }

    public static bool IsPointerOverUI()
    {
        var curPos = new PointerEventData(EventSystem.current);
        curPos.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(curPos, results);
        return results.Count > 0;
    }
}