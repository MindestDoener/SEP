using UnityEngine;
using UnityEngine.UI;

public class WearableItemController : MonoBehaviour
{
    public WearableItem ItemType;
    public float Price;
    public bool Unlocked;
    public int Id;
    public Sprite Image;
    public TabItemController TIC;

    public void ChangeLook()
    {
        if (Unlocked)
        {
            GameData.CustomCharacter[ItemType] = Image.name;
            WearableController.SetWearable(Image, ItemType); 
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