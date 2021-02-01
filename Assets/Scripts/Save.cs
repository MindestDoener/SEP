using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public float savBalance;
    public float savMultiplier;
    public Dictionary<Rarity, Dictionary<string, float>> savTrashCollectCount;
    public Dictionary<WearableItem, string> savCustomCharacter;
    public List<UpgradeData> savUpgradeDatas;
}

[System.Serializable]
public class UpgradeData
{
    public int ButtonNumber;
    public int UpgradeLevel;
    public float MultiplierIncrement;
    public float UpgradeCosts;
    public float CostIncrements;

    public UpgradeData(ShopItemScriptableObject item)
    {
        ButtonNumber = item.ButtonNumber;
        UpgradeLevel = item.UpgradeLevel;
        MultiplierIncrement = item.MultiplierIncrement;
        UpgradeCosts = item.UpgradeCosts;
        CostIncrements = item.CostIncrements;
    }
}