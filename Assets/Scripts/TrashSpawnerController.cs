using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrashSpawnerController : MonoBehaviour
{
    public GameObject trashPrefab;

    [SerializeField] private float ySpawnOffset;
    [SerializeField] private float xSpawnOffset;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;
    private Camera _cam;
    private Vector3 _rightCorner;
    private Rarity _selectedRarity;

    private Dictionary<Rarity, List<TrashScriptableObject>> _trashItems;

    // Start is called before the first frame update
    private void Start()
    {
        _cam = Camera.main;
        var camCords = _cam.ScreenToWorldPoint(new Vector3(0, _cam.pixelHeight, _cam.nearClipPlane));
        _rightCorner = new Vector3(-camCords.x, camCords.y, camCords.z);
        TrashManager.AssignItemsToDictionary();
        _trashItems = TrashManager.GetAllTrashItems();
        StartCoroutine(SpawnTrash());
    }

    private IEnumerator SpawnTrash()
    {
        while (true)
        {
            _selectedRarity = GetRandomRarity();
            var type = _trashItems[_selectedRarity];
            InstantiateRandomObjectFromList(type);
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        }
    }

    private void InstantiateRandomObjectFromList(List<TrashScriptableObject> items)
    {
        var random = Random.Range(0, items.Count);
        var item = items.ElementAt(random);
        var randomRotation =
            Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, Random.Range(0, 360)));
        var instantiatedObject = Instantiate(trashPrefab,
            new Vector3(_rightCorner.x + xSpawnOffset,
                -(_rightCorner.y / 2) + Random.Range(-ySpawnOffset, ySpawnOffset), 0), randomRotation);
        var trashController = instantiatedObject.GetComponent<TrashController>();
        trashController.SetSprite(item.Sprite);
        trashController.SetCurrencyValue(item.Value);
        trashController.SetRarity(item.Rarity);
        trashController.SetMoveSpeed(item.MoveSpeed);
        trashController.name = item.Name;
    }

    private Rarity GetRandomRarity()
    {
        var rand = Random.Range(0, 1000 + 1);
        if (rand <= (int) Rarity.Legendary) return Rarity.Legendary;
        if (rand <= (int) Rarity.SuperRare) return Rarity.SuperRare;
        if (rand <= (int) Rarity.Rare) return Rarity.Rare;
        if (rand <= (int) Rarity.Uncommon) return Rarity.Uncommon;
        return Rarity.Common;
    }
}