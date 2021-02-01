using UnityEngine;
using UnityEngine.UI;

internal class TabItemController : MonoBehaviour
{
    [SerializeField] private GameObject ItemPrefab;
    [SerializeField] private WearableItem ItemType; //muss einem Ordner aus Resources/WearableItems gleichen
    private GameObject _currentItem;

    public void LoadItems(int navButtonInput)
    {
        var wearableItems = Resources.LoadAll<WearableItemScriptableObject>("WearableItems\\" + ItemType);
        var itemContainer = GameObject.FindWithTag("ItemContainer");

        foreach (Transform child in itemContainer.transform) Destroy(child.gameObject);

        foreach (var item in wearableItems)
        {
            _currentItem = Instantiate(ItemPrefab, itemContainer.transform);
            _currentItem.transform.GetChild(0).GetComponent<Image>().sprite = item.ItemImage;
            _currentItem.transform.GetChild(1).GetComponent<Text>().text = item.Name;
            _currentItem.GetComponent<WearableItemController>().ItemType = ItemType;
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

    private GameObject CurrentTab()
    {
        try
        {
            var itemType = GameObject.FindWithTag("ItemContainer").transform.GetChild(0)
                .GetComponent<WearableItemController>().ItemType;
            var tabBar = GameObject.FindWithTag("TabBar").transform;

            for (var i = 0; i < 5; i++)
                if (tabBar.GetChild(i).GetComponent<TabItemController>().ItemType == itemType)
                    return tabBar.GetChild(i).gameObject;
        }
        catch (UnityException)
        {
            return null;
        }

        return null;
    }
}
