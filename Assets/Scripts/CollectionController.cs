using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CollectionController : MonoBehaviour
{

    [SerializeField] private GameObject collectionItemPrefab;
    private Dictionary<Rarity, List<TrashScriptableObject>> _trashItems;
    private GameObject _collectionContainer;
    private static bool _refreshRequested = false;

    public static void RequestRefresh()
    {
        _refreshRequested = true;
    }

    private void Start()
    {
        _collectionContainer = GameObject.FindWithTag("CollectionContainer");
        _trashItems = TrashManager.GetAllTrashItems();
        SetUpCollection();
    }

    private void Update()
    {
        if (_refreshRequested)
        {
            Refresh();
        }
    }

    private void Refresh()
    {
        _trashItems = TrashManager.GetAllTrashItems();
        foreach (var pair in _trashItems)
        {
            foreach (var item in pair.Value.Where(item => !item.IsUnlocked && item.Count > 0))
            {
                Unlock(item);
            }
        }
        _refreshRequested = false;
    }

    private void SetUpCollection()
    {
        foreach (var rarityList in _trashItems)
        {
            InstantiateAllFromList(rarityList);
        }
    }

    private void Unlock(TrashScriptableObject item)
    {
        item.CollectionObject.transform.GetChild(1).GetComponent<Text>().text = item.Name;
        item.CollectionObject.transform.GetChild(0).GetComponent<Image>().color = Color.white;
        item.IsUnlocked = true;
    }

    private void InstantiateAllFromList(KeyValuePair<Rarity,List<TrashScriptableObject>> rarityListPair)
    {
        foreach (var item in rarityListPair.Value)
        {
            var instantiatedObject = Instantiate(collectionItemPrefab,_collectionContainer.transform);
            instantiatedObject.name = item.name;
            instantiatedObject.transform.GetChild(1).GetComponent<Text>().text = "???";
            instantiatedObject.transform.GetChild(0).GetComponent<Image>().sprite = item.Sprite;
            instantiatedObject.transform.GetChild(0).GetComponent<Image>().color = Color.black;
            item.IsUnlocked = false;
            item.CollectionObject = instantiatedObject;
        }
        //TrashManager.UpdateList(rarityListPair);
    }
}
