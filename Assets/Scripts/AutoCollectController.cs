using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCollectController : MonoBehaviour
{
    
    [SerializeField] private float AutoCollectRate = 4;
    public void Start()
    {
        InvokeRepeating("DestroyClosestTrashInRadius", 5, AutoCollectRate);
            
    }
    // Start is called before the first frame update
    public void DestroyClosestTrashInRadius()
    {
        GameObject[] trashList = GameObject.FindGameObjectsWithTag("Trash");
        float closestDirection = Mathf.Infinity;
        GameObject closestTrash = null;

        foreach(var trash in trashList)
        {
            Vector3 directionToTrash = trash.transform.position - this.transform.position;
            float trashdistance = directionToTrash.sqrMagnitude;
            if(trashdistance < closestDirection)
            {
                closestDirection = trashdistance;
                closestTrash = trash;
            }
        }
        if (Mathf.Sqrt(closestDirection) < this.GetComponent<PlayerController>().CollectionRadius)
        {
            StartCoroutine(ObjectClickController.DestroyObject(closestTrash));
            AddValue(Convert.ToDecimal(closestTrash.GetComponent<TrashController>().GetCurrencyValue()));
        }
    }

    private void AddValue(decimal value)
    {
        BalanceController balance = (BalanceController)this.GetComponent(typeof(BalanceController));
        balance.AddBalance(value);
    }

}
