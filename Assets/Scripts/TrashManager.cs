using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    private static Dictionary<Rarity, List<TrashScriptableObject>> TrashItems = new Dictionary<Rarity, List<TrashScriptableObject>>
    {
        {Rarity.Common, new List<TrashScriptableObject>()},
        {Rarity.Uncommon, new List<TrashScriptableObject>()},
        {Rarity.Rare, new List<TrashScriptableObject>()},
        {Rarity.SuperRare, new List<TrashScriptableObject>()},
        {Rarity.Legendary, new List<TrashScriptableObject>()}
    };
    
    public static void AssignItemsToDictionary()
    {
        var trashItems = Resources.LoadAll<TrashScriptableObject>("TrashItems");
        foreach (var item in trashItems)
        {
            TrashItems[item.Rarity].Add(item);
        }
            
    }

    public static Dictionary<Rarity,List<TrashScriptableObject>> GetAllTrashItems()
    {
        return TrashItems;
    }

    public static void UpdateList(KeyValuePair<Rarity,List<TrashScriptableObject>> rarityListPair)
    {
        TrashItems[rarityListPair.Key] = rarityListPair.Value;
    }

    public static void IncreaseCount(string itemName, Rarity rarity)
    {
        var itemToBeIncreased = TrashItems[rarity].Find(item => item.Name == itemName);
        itemToBeIncreased.Count++;
        if (itemToBeIncreased.Count == decimal.One)
        {
            CollectionController.RequestRefresh();
        }
        Debug.Log(itemToBeIncreased.Name + ": amount collected = " + itemToBeIncreased.Count);
    }
}
