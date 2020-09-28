using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common = 100,
    Uncommon = 50,
    Rare = 20,
    SuperRare = 6,
    Legendary = 1
}

public enum UpgradeType
{
    ClickUpgrade = 0,
    AutocollectUpgrade = 1
}

public enum DetailViewComponents
{
    NameText = 0,
    ItemImage = 1,
    RarityText = 2,
    DescriptionText = 3,
    CountNumberText = 5,
    BaseValueNumberText = 7,
    CurrentValueNumberText = 9
}
