using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class WearableItemScriptableObject : ScriptableObject
{
    public String Name;
    public Sprite ItemImage;
    public bool IsUnlocked = false;
    public WearableItem ItemType;
}