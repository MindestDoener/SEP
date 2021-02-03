using UnityEngine;
using UnityEngine.UI;

internal class TabItemController : MonoBehaviour
{
    public class TabItemController : MonoBehaviour
    {
        [SerializeField] private GameObject ItemPrefab;
        [SerializeField] private WearableItem ItemType; 
        private GameObject _currentItem;
        private WearableItemScriptableObject[] _WearableItems;

        public void Start()
        {
            _WearableItems = Resources.LoadAll<WearableItemScriptableObject>("WearableItems\\" + ItemType);
        }
        public void LoadItems(int NavButtonInput)
        {
            var ItemContainer = GameObject.FindWithTag("ItemContainer");

        foreach (Transform child in itemContainer.transform) Destroy(child.gameObject);

            for (var i = 0; i < _WearableItems.Length; i++)
            {
                _currentItem = Instantiate(ItemPrefab, ItemContainer.transform);
                _currentItem.transform.GetChild(0).GetComponent<Image>().sprite = _WearableItems[i].ItemImage;
                _currentItem.transform.GetChild(0).localScale = new Vector3(GetScale(ItemType), GetScale(ItemType), 1);

                if (!_WearableItems[i].IsUnlocked)
                {
                    _currentItem.transform.GetChild(1).GetComponent<Text>().text = _WearableItems[i].Price.ToString();
                    _currentItem.GetComponent<Image>().color = new Color(0, 0, 0, 128);
                }
                else
                {
                    _currentItem.transform.GetChild(1).GetComponent<Text>().text = "";
                    _currentItem.GetComponent<WearableItemController>().ItemType = ItemType;
                    _currentItem.GetComponent<WearableItemController>().Unlocked = _WearableItems[i].IsUnlocked;
                    _currentItem.GetComponent<WearableItemController>().Id = _WearableItems[i].id;
                    _currentItem.GetComponent<WearableItemController>().TIC = this;
                }
            }
        }
    }

    public float GetScale(WearableItem ItemType)
    {
        switch (ItemType)
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
                    return 2f;
                case WearableItem.Faces:
                    return 1.2f;
            }

            return 1f;
        }
        
        public void SetItem(WearableItemController WearableItem)
        {
            for(int i = 0; i < _WearableItems.Length; i++)
            {
                if(_WearableItems[i].id == WearableItem.Id)
                {
                    _WearableItems[i].IsUnlocked = WearableItem.Unlocked;
                }
            }
        }

        return null;
    }
}
