using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrashSpawnerController : MonoBehaviour
{

    public GameObject trashPrefab;
    private readonly List<TrashScriptableObject> _commonItems = new List<TrashScriptableObject>();
    private readonly List<TrashScriptableObject> _uncommonItems = new List<TrashScriptableObject>();
    private readonly List<TrashScriptableObject> _rareItems = new List<TrashScriptableObject>();
    private readonly List<TrashScriptableObject> _superRareItems = new List<TrashScriptableObject>();
    private readonly List<TrashScriptableObject> _legendaryItems  = new List<TrashScriptableObject>();
    [SerializeField]
    private float ySpawnOffset = 0;
    [SerializeField]
    private float xSpawnOffset = 0;
    [SerializeField]
    private float minSpawnTime = 0;
    [SerializeField]
    private float maxSpawnTime = 0;
    private Camera _cam;
    private Rarity _selectedRarity;
    private Vector3 _rightCorner;
    
    // Start is called before the first frame update
    void Start()
    {    
        _cam = Camera.main;
        var camCords = _cam.ScreenToWorldPoint(new Vector3(0, _cam.pixelHeight, _cam.nearClipPlane));
        _rightCorner = new Vector3(-camCords.x, camCords.y, camCords.z);
        AssignItemsToArray();
        StartCoroutine(SpawnTrash());
    }
    
    IEnumerator SpawnTrash()
    {
        while (true)
        {
            _selectedRarity = GetRandomRarity();
            List<TrashScriptableObject> type;
            switch (_selectedRarity)
            {
                case Rarity.Common:
                    type = _commonItems;
                    break;
                case Rarity.Uncommon: 
                    type = _uncommonItems;
                    break;
                case Rarity.Rare: 
                    type = _rareItems;
                    break;
                case Rarity.SuperRare: 
                    type = _superRareItems;
                    break;
                case Rarity.Legendary: 
                    type = _legendaryItems;
                    break;
                default:
                    type = _commonItems;
                    break;
            }
            InstantiateRandomObjectFromList(type);
            yield return new WaitForSeconds(Random.Range(minSpawnTime,maxSpawnTime));   
        }
    }

    private void InstantiateRandomObjectFromList(List<TrashScriptableObject> items)
    {
        var random = Random.Range(0, items.Count);
        var item = items.ElementAt(random);
        var randomRotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, Random.Range(0,360)));
        var instantiatedObject = Instantiate (trashPrefab, 
            new Vector3(_rightCorner.x + xSpawnOffset, -(_rightCorner.y / 2) + Random.Range(-ySpawnOffset, ySpawnOffset), _rightCorner.z), 
            randomRotation);
        var trashController = instantiatedObject.GetComponent<TrashController>();
        trashController.SetSprite(item.Sprite);
        trashController.SetCurrencyValue(item.Value);
        trashController.SetMoveSpeed(item.MoveSpeed);
        trashController.name = item.Name;
    }

    private Rarity GetRandomRarity()
    {
        var rand = Random.Range(0, 100+1);
        if (rand <= (int)Rarity.Legendary) return Rarity.Legendary;
        else if (rand <= (int)Rarity.Legendary + (int)Rarity.SuperRare) return Rarity.SuperRare;
        else if (rand <= (int)Rarity.SuperRare + (int)Rarity.Rare) return Rarity.Rare;
        else if (rand <= (int)Rarity.Rare + (int)Rarity.Uncommon) return Rarity.Uncommon;
        else if (rand <= (int)Rarity.Uncommon + (int)Rarity.Common) return Rarity.Common;
        else return Rarity.Common;
    }

    private void AssignItemsToArray()
    {
        var trashItems = Resources.LoadAll<TrashScriptableObject>("TrashItems");
        foreach (var item in trashItems)
        {
            switch (item.Rarity)
            {
                case Rarity.Common:
                    _commonItems.Add(item);
                    break;
                case Rarity.Uncommon:
                    _uncommonItems.Add(item);
                    break;
                case Rarity.Rare:
                    _rareItems.Add(item);
                    break;
                case Rarity.SuperRare:
                    _superRareItems.Add(item);
                    break;
                case Rarity.Legendary:
                    _legendaryItems.Add(item);
                    break;
                default: 
                    Debug.LogWarning("No Rarity Found");
                    break;
            }
        }
    }
}
