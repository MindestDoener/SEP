using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class WearableItemScriptableObject : ScriptableObject
{
    public int id;
    public int Price;
    public Sprite ItemImage;
    public bool IsUnlocked = false;
    public WearableItem ItemType;
}