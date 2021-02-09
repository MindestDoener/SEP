using System;
using System.Collections.Generic;

[Serializable]
public class Save
{
    public float savBalance;
    public float savClickMultiplier;
    public float savAutoMultiplier;
    public float savAutoCollectRate;
    public float savAutoCollectRange;
    public Dictionary<Rarity, Dictionary<string, float>> savTrashCollectCount;
    public Dictionary<WearableItem, string> savCustomCharacter;
    public List<UpgradeData> savUpgradeDatas;
}

[Serializable]
public class UpgradeData
{
    public int UpgradeLevel;
    public float MultiplierIncrement;
    public float UpgradeCosts;
    public float CostIncrements;

    public UpgradeData(ShopItemScriptableObject item)
    {
        UpgradeLevel = item.UpgradeLevel;
        MultiplierIncrement = item.MultiplierIncrement;
        UpgradeCosts = item.UpgradeCosts;
        CostIncrements = item.CostIncrements;
    }
}