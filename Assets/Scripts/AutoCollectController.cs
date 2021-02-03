using UnityEngine;

public class AutoCollectController : MonoBehaviour
{
    private Canvas _mainCanvas;

    public void Start()
    {
        InvokeRepeating(nameof(DestroyClosestTrashInRadius), 5, GameData.AutoCollectRate);
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
            var trashDistance = directionToTrash.sqrMagnitude;
            if (trashDistance < closestDirection)
            {
                closestDirection = trashDistance;
                closestTrash = trash;
            }
        }

        if (Mathf.Sqrt(closestDirection) < GameData.AutoCollectRange &&
            !closestTrash.GetComponent<TrashController>().IsBeeingDestroyed)
        {
            StartCoroutine(ObjectClickController.DestroyObject(closestTrash));
            AddValue(closestTrash.GetComponent<TrashController>().GetCurrencyValue());
            AddValueText(closestTrash.GetComponent<TrashController>());
        }
    }

    private void AddValue(float value)
    {
        var balance = (BalanceController) GetComponent(typeof(BalanceController));
        balance.AddBalance(value * GameData.AutoMultiplier);
    }

    private void AddValueText(TrashController trash)
    {
        _mainCanvas.GetComponent<ValueDisplayer>().CreateText(trash, false);
    }
}