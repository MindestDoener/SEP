using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ShopItemScriptableObject : ScriptableObject
{
    public int UpgradeLevel;
    public float MultiplierIncrement;
    public float UpgradeCosts;
    public float CostIncrements;
    public String ButtonText;
    public UpgradeType Type;
    public int ButtonNumber;
    public Sprite ItemImage;
    public Color Color;
}
