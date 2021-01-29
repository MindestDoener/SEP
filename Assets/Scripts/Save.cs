using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public float savBalance;

    public float savMultiplier;

    public Dictionary<Rarity, Dictionary<string, float>> savTrashCollectCount;
    
    public Dictionary<WearableItem, string> savCustomCharacter;
    
    //TODO   
}