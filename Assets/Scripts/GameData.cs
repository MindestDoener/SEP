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
        {WearableItem.Bodys, "Body"},
        {WearableItem.Faces, "Face"},
        {WearableItem.Hats, "HatBlack"},
        {WearableItem.Pants, "Pants"},
        {WearableItem.Shoes, "Boots"}
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
            yield return new WaitForSeconds(autoSaveCycleTime);
            UpdateTrashCollectCount();
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