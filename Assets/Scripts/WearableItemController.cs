using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class WearableItemController : MonoBehaviour
{
    public WearableItem ItemType;
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
            if(GameData.Balance >= Convert.ToDecimal(ClickedItemPrice))
            {
                GameData.Balance  - Convert.ToDecimal(ClickedItemPrice);
                this.transform.GetChild(1).GetComponent<Text>().text = "";
                this.transform.GetComponent<Image>().color = new Color(255,255,255,128);
                this.Unlocked = true;
                TIC.SetItem(this);
            }
        }
    }
}