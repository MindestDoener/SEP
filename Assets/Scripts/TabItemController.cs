using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class TabItemController : MonoBehaviour
    {
        [SerializeField]
        private GameObject ItemPrefab;
        private GameObject CurrentItem;
        [SerializeField]
        private WearableItem ItemType; //muss einem Ordner aus Resources/WearableItems gleichen
        
        public void LoadItems()
        {
            var WearableItems = Resources.LoadAll<WearableItemScriptableObject>("WearableItems\\" + ItemType);
            GameObject ItemContainer = GameObject.FindWithTag("ItemContainer");

            foreach (Transform child in ItemContainer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            int xKoord = 70;
            int yKoord = 160;

            for (int i = 0; i < WearableItems.Length; i++)
            {
                if (i%4 == 0)
                {
                    yKoord -= 70;
                    xKoord = 70;
                }
                CurrentItem = Instantiate(ItemPrefab, new Vector3(xKoord, yKoord, 0), Quaternion.identity);
                CurrentItem.transform.SetParent(ItemContainer.transform);
                CurrentItem.transform.localScale = new Vector2(0.9f, 0.9f);
                CurrentItem.transform.GetChild(0).GetComponent<Image>().sprite = WearableItems[i].ItemImage;
                CurrentItem.transform.GetChild(1).GetComponent<Text>().text = WearableItems[i].Name;
                CurrentItem.GetComponent<WearableItemController>().ItemType = ItemType;
                xKoord += 100;
            }
        }
        public void Change()
        {
            
        }
    }
}
