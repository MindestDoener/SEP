using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WearableController : MonoBehaviour
{
    private static GameObject _player;
    private static GameObject _playerCustomizerModel;

    public static Sprite GetSpriteByName(string name, WearableItem type)
    {
        var wearableItems = Resources.LoadAll<WearableItemScriptableObject>("WearableItems\\" + type);
        Sprite sprite;
        try
        {
            sprite = wearableItems.First(item => item.ItemImage.name.Equals(name)).ItemImage;
        }
        catch (Exception e)
        {
            Debug.LogWarning("wearable not found our is none");
            sprite = null;
        }

        return sprite;
    }

    public static void SetWearable(Sprite sprite, WearableItem type)
    {
        if (_player is null) _player = GameObject.FindWithTag("Player");

        if (_playerCustomizerModel is null) _playerCustomizerModel = GameObject.FindWithTag("PlayerCustomizerModel");

        _player.transform.GetChild((int) type).GetComponent<SpriteRenderer>().sprite = sprite;

        if (!(_playerCustomizerModel is null))
        {
            _playerCustomizerModel.transform.GetChild((int) type).GetComponent<Image>().sprite = sprite;
            _playerCustomizerModel.transform.GetChild((int) type).GetComponent<Image>().color =
                new Color(255, 255, 255, 255);
        }
    }
}