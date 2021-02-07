using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static float Balance = 0;
    public static float Multiplier = 1;
    public float autoSaveCycleTime = 10f;

    public static Dictionary<Rarity, Dictionary<string, float>> TrashCollectCount;
    
    public static List<ShopItemScriptableObject> ShopItems;

    public static Dictionary<WearableItem, string> CustomCharacter = new Dictionary<WearableItem, string>()
    {
        {WearableItem.Skins, "Skin"},
        {WearableItem.Rings, "Ring"},
        {WearableItem.Pants, "Pant"},
        {WearableItem.Bodys, "Body"},
        {WearableItem.Eyes, "Eye"},
        {WearableItem.Hairs, "Hair"},
        {WearableItem.Shoes, "Shoe"},
        {WearableItem.Beards, "Beard"},
        {WearableItem.Hats, "Hat"}
    };

    private SaveScript saveScript;

    private void Start()
    {
        saveScript = GetComponent<SaveScript>();
        if (saveScript.LoadData())
        {
            GameObject.FindWithTag("Balance").GetComponent<BalanceDisplayController>().DisplayBalance(Balance);
            TrashManager.UpdateTrashItems(TrashCollectCount);
            foreach (var pair in CustomCharacter)
            {
                WearableController.SetWearable(WearableController.GetSpriteByName(pair.Value, pair.Key), pair.Key);
            }
        }

        StartCoroutine(AutoSave());
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);//autoSaveCycleTime);
            UpdateTrashCollectCount();
            if (ShopItems is null)
            {
                ShopItems = ShopController.AssignItemsToArray();
            }
            saveScript.SaveData();
        }
    }

    private void UpdateTrashCollectCount()
    {
        var trashItems = TrashManager.GetAllTrashItems();
        TrashCollectCount = trashItems.ToDictionary(pair => pair.Key,
            pair => pair.Value.ToDictionary(
                trashItem => trashItem.Name,
                trashItem => trashItem.Count
            )
        );
    }
}