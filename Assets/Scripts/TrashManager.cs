using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    private static readonly Dictionary<Rarity, List<TrashScriptableObject>> TrashItems =
        new Dictionary<Rarity, List<TrashScriptableObject>>
        {
            {Rarity.Common, new List<TrashScriptableObject>()},
            {Rarity.Uncommon, new List<TrashScriptableObject>()},
            {Rarity.Rare, new List<TrashScriptableObject>()},
            {Rarity.SuperRare, new List<TrashScriptableObject>()},
            {Rarity.Legendary, new List<TrashScriptableObject>()}
        };

    private static Dictionary<Rarity, Color> _rarityColor;

    [SerializeField] private Color commonColor;
    [SerializeField] private Color uncommonColor;
    [SerializeField] private Color rareColor;
    [SerializeField] private Color superRareColor;
    [SerializeField] private Color legendaryColor;

    private void Start()
    {
        _rarityColor = new Dictionary<Rarity, Color>
        {
            {Rarity.Common, commonColor},
            {Rarity.Uncommon, uncommonColor},
            {Rarity.Rare, rareColor},
            {Rarity.SuperRare, superRareColor},
            {Rarity.Legendary, legendaryColor}
        };
    }

    public static Color GetRarityColor(Rarity rarity)
    {
        return _rarityColor[rarity];
    }

    public static void AssignItemsToDictionary()
    {
        var trashItems = Resources.LoadAll<TrashScriptableObject>("TrashItems");
        foreach (var item in trashItems) TrashItems[item.Rarity].Add(item);
    }

    public static Dictionary<Rarity, List<TrashScriptableObject>> GetAllTrashItems()
    {
        return TrashItems;
    }

    public static void UpdateTrashItems(Dictionary<Rarity, Dictionary<string, decimal>> trashCollectCount)
    {
        foreach (var pair in trashCollectCount)
        {
            foreach (var subPair in pair.Value)
            {
                TrashItems[pair.Key].Find(item => item.Name == subPair.Key).Count = subPair.Value;
            }
        }
        CollectionController.RequestRefresh();
    }

    public static void IncreaseCount(string itemName, Rarity rarity)
    {
        var itemToBeIncreased = TrashItems[rarity].Find(item => item.Name == itemName);
        itemToBeIncreased.Count++;
        if (itemToBeIncreased.Count == decimal.One) CollectionController.RequestRefresh();
    }
}