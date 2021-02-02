using UnityEngine;

[CreateAssetMenu]
public class ShopItemScriptableObject : ScriptableObject
{
    public int UpgradeLevel;
    public float MultiplierIncrement;
    public float UpgradeCosts;
    public float CostIncrements;
    public string ButtonText;

    public UpgradeType Type;

    // public int ButtonNumber;
    public Sprite ItemImage;
    public Color Color;
}