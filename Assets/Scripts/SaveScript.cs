using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[RequireComponent(typeof(GameData))]
public class SaveScript : MonoBehaviour
{
    private GameData gameData;
    private string savePath;

    private void Start()
    {
        gameData = GetComponent<GameData>();
        savePath = Application.persistentDataPath + "/savegame.ocs";     // ocs = ocean cleaner save
    }

    public void SaveData()
    {
        var save = new Save()
        {
            savBalance = GameData.Balance,
            savMultiplier = GameData.Multiplier,
            savTrashCollectCount = GameData.TrashCollectCount,
            savCustomCharacter = GameData.CustomCharacter
        };
        var binaryFormatter = new BinaryFormatter();
        using (var fileStream = File.Create(savePath))
        {
            binaryFormatter.Serialize(fileStream, save);
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
            GameData.Multiplier = save.savMultiplier;
            GameData.TrashCollectCount = save.savTrashCollectCount;
            GameData.CustomCharacter = save.savCustomCharacter;

            Debug.Log("Data Loaded");
            return true;
        }
        else
        {
            Debug.LogWarning("Save file doesn't exist.");
            return false;
        }
    }
}