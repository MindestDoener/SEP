using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickController : MonoBehaviour
{
    private RaycastHit2D _hit;
    private BalanceController _balance;
    private TrashController _trash;
    private GameObject _hitObject;
    [SerializeField]
    private GameObject _player;
    private float _multiplier = 1;

    void Start()
    {
        _balance = (BalanceController) _player.GetComponent(typeof(BalanceController));
    }


    void Update()
    {
        GetClickedObject();
    }

    private void GetClickedObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
      
            if (_hit.collider != null)
            {
                _hitObject = _hit.collider.gameObject;
                _trash = _hitObject.GetComponent<TrashController>();
                AddValue(Convert.ToDecimal(_trash.GetCurrencyValue() * _multiplier));
                StartCoroutine(DestroyObject(_hitObject));
            }  
        }
        
    }

    private void AddValue(decimal value)
    {
        _balance.AddBalance(value);
    }

    public static IEnumerator DestroyObject(GameObject objectToDestroy)
    {
        objectToDestroy.GetComponentInChildren<ParticleSystem>().Play();
        objectToDestroy.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(objectToDestroy);
    }

    public void IncreaseMultiplier(float value)
    {
        _multiplier = value;
    }

    public float GetMultiplier()
    {
        return _multiplier;
    }
    
}
