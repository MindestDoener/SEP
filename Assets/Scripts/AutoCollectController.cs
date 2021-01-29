using System;
using UnityEngine;

public class AutoCollectController : MonoBehaviour
{
    [SerializeField] private float autoCollectRate = 4;
    private Canvas _mainCanvas;

    public void Start()
    {
        InvokeRepeating(nameof(DestroyClosestTrashInRadius), 5, autoCollectRate);
        _mainCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
    }

    public void DestroyClosestTrashInRadius()
    {
        var trashList = GameObject.FindGameObjectsWithTag("Trash");
        var closestDirection = Mathf.Infinity;
        GameObject closestTrash = null;

        foreach (var trash in trashList)
        {
            var directionToTrash = trash.transform.position - transform.position;
            var trashdistance = directionToTrash.sqrMagnitude;
            if (trashdistance < closestDirection)
            {
                closestDirection = trashdistance;
                closestTrash = trash;
            }
        }

        if (Mathf.Sqrt(closestDirection) < GetComponent<PlayerController>().CollectionRadius && !closestTrash.GetComponent<TrashController>().IsBeeingDestroyed)
        {
            StartCoroutine(ObjectClickController.DestroyObject(closestTrash));
            AddValue(closestTrash.GetComponent<TrashController>().GetCurrencyValue());
            AddValueText(closestTrash.GetComponent<TrashController>());
        }
    }

    private void AddValue(float value)
    {
        var balance = (BalanceController) GetComponent(typeof(BalanceController));
        balance.AddBalance(value);
    }

    public void AddValueText(TrashController trash)
    {
        _mainCanvas.GetComponent<ValueDisplayer>().CreateText(trash);
    }
}
