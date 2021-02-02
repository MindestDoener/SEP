using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[RequireComponent(typeof(GameData))]
public class SaveScript : MonoBehaviour
{
    private Save latestSave;
    private string savePath;

    private void Start()
    {
        savePath = Application.persistentDataPath + "/savegame.ocs"; // ocs = ocean cleaner save
    }

    public void SaveData()
    {
        var save = new Save
        {
            savBalance = GameData.Balance,
            savClickMultiplier = GameData.ClickMultiplier,
            savAutoMultiplier = GameData.AutoMultiplier,
            savAutoCollectRate = GameData.AutoCollectRate,
            savTrashCollectCount = GameData.TrashCollectCount,
            savCustomCharacter = GameData.CustomCharacter,
            savUpgradeDatas = GameData.ShopItems.ConvertAll(item => new UpgradeData(item))
        };
        if (!save.Equals(latestSave))
        {
            latestSave = save;
            var binaryFormatter = new BinaryFormatter();
            using (var fileStream = File.Create(savePath))
            {
                binaryFormatter.Serialize(fileStream, save);
            }
        }
    }

    public bool LoadData()
    {
        if (File.Exists(savePath))
        {
            Save save;

            var binaryFormatter = new BinaryFormatter();
            using (var fileStream = File.Open(savePath, FileMode.Open))
            {
                save = (Save) binaryFormatter.Deserialize(fileStream);
            }

            GameData.Balance = save.savBalance;
            GameData.ClickMultiplier = save.savClickMultiplier;
            GameData.AutoMultiplier = save.savAutoMultiplier;
            GameData.AutoCollectRate = save.savAutoCollectRate;
            GameData.TrashCollectCount = save.savTrashCollectCount;
            GameData.CustomCharacter = save.savCustomCharacter;
            GameData.ShopItems = ShopController.AssignItemsToArray();
            for (var i = 0; i < GameData.ShopItems.Count; i++)
            {
                var item = GameData.ShopItems[i];
                var loadedItem = save.savUpgradeDatas[i];
                // if (item.ButtonNumber == loadedItem.ButtonNumber)
                // {
                item.UpgradeLevel = loadedItem.UpgradeLevel;
                item.MultiplierIncrement = loadedItem.MultiplierIncrement;
                item.UpgradeCosts = loadedItem.UpgradeCosts;
                item.CostIncrements = loadedItem.CostIncrements;
                // }
                // else
                // {
                //     Debug.LogError("Upgrade Data does not align!");
                //     throw new InvalidDataException("loaded upgrades do not align: itemNum: " + item.ButtonNumber +
                //                                    "; loadedItemNum: " + loadedItem.ButtonNumber);
                // }
            }

            Debug.Log("Data Loaded");
            return true;
        }

        Debug.LogWarning("Save file doesn't exist.");
        return false;
    }
}