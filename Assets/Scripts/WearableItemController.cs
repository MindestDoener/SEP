using UnityEngine;
using UnityEngine.UI;

public class WearableItemController : MonoBehaviour
{
    public WearableItem ItemType;

    public void ChangeLook()
    {
        var clickedItem = transform.GetChild(0).GetComponent<Image>().sprite;
        GameData.CustomCharacter[ItemType] = clickedItem.name;
        WearableController.SetWearable(clickedItem, ItemType);
    }
}