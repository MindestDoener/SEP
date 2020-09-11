using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ShopItemScriptableObject : ScriptableObject
{
    public int UpgradeLevel;
    public float MultiplierIncrement;
    public int UpgradeCosts;
    public float CostIncrements;
    public String ButtonText;
    public UpgradeType Type;
    public int ButtonNumber;
}
