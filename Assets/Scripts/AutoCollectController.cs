using System.Collections;
using UnityEngine;

public class AutoCollectController : MonoBehaviour
{
    private Canvas _mainCanvas;

    public void Start()
    {
        StartCoroutine(DestroyClosestTrashInRadius());
        _mainCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
    }

    public IEnumerator DestroyClosestTrashInRadius()
    {
        while (true)
        {
            yield return new WaitForSeconds(GameData.AutoCollectRate);
            var trashList = GameObject.FindGameObjectsWithTag("Trash");
            var closestDirection = Mathf.Infinity;
            GameObject closestTrash = null;

            foreach (var trash in trashList)
            {
                var directionToTrash = trash.transform.position - transform.position;
                var trashDistance = directionToTrash.sqrMagnitude;
                if (trashDistance < closestDirection && !trash.GetComponent<TrashController>().IsBeeingDestroyed)
                {
                    closestDirection = trashDistance;
                    closestTrash = trash;
                }
            }

            if (Mathf.Sqrt(closestDirection) <= GameData.AutoCollectRange &&
                !closestTrash.GetComponent<TrashController>().IsBeeingDestroyed)
            {
                StartCoroutine(ObjectClickController.DestroyObject(closestTrash));
                AddValue(closestTrash.GetComponent<TrashController>().GetCurrencyValue());
                AddValueText(closestTrash.GetComponent<TrashController>());
            }
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