using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static decimal Balance = 0;
    public static decimal Multiplier = 1;

    public static Dictionary<Rarity, Dictionary<string, decimal>> TrashCollectCount;

    private SaveScript saveScript;

    private void Start()
    {
        print(Application.persistentDataPath);
        saveScript = GetComponent<SaveScript>();
        if (saveScript.LoadData())
        {
            GameObject.FindWithTag("Balance").GetComponent<BalanceDisplayController>().DisplayBalance(Balance);
            TrashManager.UpdateTrashItems(TrashCollectCount);
        }
        StartCoroutine(AutoSave());
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
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