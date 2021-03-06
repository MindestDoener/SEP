﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    public static string Username;
    public static float Balance = 0;
    public static float ClickMultiplier = 1;
    public static float AutoMultiplier = 1;
    public static float AutoCollectRate = 5;
    public static float AutoCollectRange = 5;

    public static Dictionary<Rarity, Dictionary<string, float>> TrashCollectCount;

    public static List<ShopItemScriptableObject> ShopItems;

    public static Dictionary<WearableItem, string> CustomCharacter = new Dictionary<WearableItem, string>
    {
        //{WearableItem.Skins, "StandardSkin"},
        //{WearableItem.Rings, "StandardRing"},
        {WearableItem.Pants, "PantsBlue"},
        {WearableItem.Bodys, ""},
        //{WearableItem.Eyes, "StandardEyes"},
        {WearableItem.Hairs, "StandardHair"},
        {WearableItem.Shoes, ""},
        {WearableItem.Beards, ""},
        {WearableItem.Hats, ""}
    };

    public static Dictionary<string, bool> WearablesUnlocked;

    public float autoSaveCycleTime = 5f;

    private SaveScript saveScript;

    private void Start()
    {
        saveScript = GetComponent<SaveScript>();
        if (saveScript.LoadData())
        {
            TrashManager.UpdateTrashItems(TrashCollectCount);
            foreach (var pair in CustomCharacter)
                WearableController.SetWearable(WearableController.GetSpriteByName(pair.Value, pair.Key), pair.Key);
        }
        else
        {
            CreateWearableDictionary();
            Time.timeScale = 0;
            SceneManager.LoadScene("Intro", LoadSceneMode.Additive);
        }

        StartCoroutine(AutoSave());
    }

    private void OnApplicationQuit()
    {
        UpdateTrashCollectCount();
        if (ShopItems is null) ShopItems = ShopController.AssignItemsToArray();
        saveScript.SaveData();
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(autoSaveCycleTime);
            UpdateTrashCollectCount();
            if (ShopItems is null) ShopItems = ShopController.AssignItemsToArray();
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

    private void CreateWearableDictionary()
    {
        var list = Resources.LoadAll<WearableItemScriptableObject>("WearableItems");
        WearablesUnlocked = list.ToDictionary(item => item.name, item => item.IsUnlocked);
        WearablesUnlocked.Add("Nothing", true);
    }
}