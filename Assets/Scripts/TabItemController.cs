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
        private int FirstItemIndex = 0;
        
        public void LoadItems(int NavButtonInput)
        {
            var WearableItems = Resources.LoadAll<WearableItemScriptableObject>("WearableItems\\" + ItemType);
            GameObject ItemContainer = GameObject.FindWithTag("ItemContainer");
            FirstItemIndex += NavButtonInput;

            if(FirstItemIndex < 0)
            {
                FirstItemIndex = 0;
            }
            else
            {
                if(FirstItemIndex > WearableItems.Length - 1)
                {
                    FirstItemIndex -= 3;
                }
            }

            foreach (Transform child in ItemContainer.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            int xKoord = 240;
            int yKoord = 390;
            int maxItems = 12;

            if(WearableItems.Length - (FirstItemIndex + 1) < 12)
            {
                maxItems = WearableItems.Length - FirstItemIndex;
            }

            for (int i = 0; i < maxItems; i++)
            {
                if (i % 3 == 0)
                {
                    yKoord -= 70;
                    xKoord = 240;
                }
                CurrentItem = Instantiate(ItemPrefab, new Vector3(xKoord, yKoord, 0), Quaternion.identity);
                CurrentItem.transform.SetParent(ItemContainer.transform);
                CurrentItem.transform.localScale = new Vector2(0.9f, 0.9f);
                CurrentItem.transform.GetChild(0).GetComponent<Image>().sprite = WearableItems[i + FirstItemIndex].ItemImage;
                CurrentItem.transform.GetChild(1).GetComponent<Text>().text = WearableItems[i + FirstItemIndex].Name;
                CurrentItem.GetComponent<WearableItemController>().ItemType = ItemType;
                xKoord += 90;
            }
        }

        public void RightButtonClick()
        {
            GameObject currentTab = CurrentTab();

            if (currentTab != null)
            {
                currentTab.GetComponent<TabItemController>().LoadItems(3);
            }
        }
        public void LeftButtonClick()
        {
            GameObject currentTab = CurrentTab();

            if(currentTab != null)
            {
                currentTab.GetComponent<TabItemController>().LoadItems(-3);
            }
        }
        private GameObject CurrentTab()
        {
            var itemType = GameObject.FindWithTag("ItemContainer").transform.GetChild(0).GetComponent<WearableItemController>().ItemType;
            var tabBar = GameObject.FindWithTag("TabBar").transform;

            for (int i = 0; i < 5; i++)
            {
                if (tabBar.GetChild(i).GetComponent<TabItemController>().ItemType == itemType)
                {
                    return tabBar.GetChild(i).gameObject;
                }
            }

            return null;
        }
    }
}
