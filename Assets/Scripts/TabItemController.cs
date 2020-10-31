using System;
using System.Collections.Generic;
using System.Globalization;
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

            float width = ItemContainer.GetComponent<RectTransform>().rect.width;
            float xKoord = ItemContainer.transform.position.x - width;
            float yKoord = ItemContainer.GetComponent<RectTransform>().rect.height * 3.5f; 
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
                    xKoord = ItemContainer.transform.position.x - width;
                }
                CurrentItem = Instantiate(ItemPrefab, new Vector3(xKoord, yKoord, 0), Quaternion.identity);
                CurrentItem.transform.SetParent(ItemContainer.transform);
                CurrentItem.transform.localScale = new Vector2(GetScale(ItemType), GetScale(ItemType));
                CurrentItem.transform.GetChild(0).GetComponent<Image>().sprite = WearableItems[i + FirstItemIndex].ItemImage;
                CurrentItem.transform.GetChild(1).GetComponent<Text>().text = WearableItems[i + FirstItemIndex].Name;
                CurrentItem.GetComponent<WearableItemController>().ItemType = ItemType;
                xKoord += width;
            }
        }

        public float GetScale(WearableItem ItemType)
        {
            switch (ItemType)
            {
                case WearableItem.Bodys:
                    return 1f;
                case WearableItem.Shoes:
                    return 2f;
                case WearableItem.Pants:
                    return 1.5f;
                case WearableItem.Hats:
                    return 1.2f;
                case WearableItem.Faces:
                    return 1.2f;
            }
            return 1f;
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
            try
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
            }
            catch(UnityException)
            {
                return null;
            }
            return null;
        }
    }
}
