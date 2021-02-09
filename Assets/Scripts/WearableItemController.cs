using UnityEngine;
using UnityEngine.UI;

public class WearableItemController : MonoBehaviour
{
    public WearableItem ItemType;
    public float Price;
    public bool Unlocked;
    public int Id;
    public TabItemController TIC;

    public void ChangeLook()
    {
        if (Unlocked)
        {
            var clickedItem = transform.GetChild(0).GetComponent<Image>().sprite;
            GameData.CustomCharacter[ItemType] = clickedItem.name;
            WearableController.SetWearable(clickedItem, ItemType); 
        }
        else
        {
            if (GameData.Balance >= Price)
            {
                GameData.Balance -= Price;
                this.transform.GetChild(2).gameObject.SetActive(false);
                this.transform.GetChild(1).GetComponent<Text>().text = "";
                this.transform.GetComponent<Image>().color = new Color(125,125,125,128);
                this.Unlocked = true;
                TIC.SetItem(this);
            }
        }
    }
}