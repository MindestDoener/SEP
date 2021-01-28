using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public decimal savBalance;

    public decimal savMultiplier;

    public Dictionary<Rarity, Dictionary<string, decimal>> savTrashCollectCount;
    
    public Dictionary<WearableItem, string> savCustomCharacter;
    
    //TODO   
}