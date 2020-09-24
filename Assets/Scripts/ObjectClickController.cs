using System;
using System.Collections;
using UnityEngine;

public class ObjectClickController : MonoBehaviour
{
    private GameObject _player;
    private Canvas _mainCanvas;
    private BalanceController _balance;
    private RaycastHit2D _hit;
    private GameObject _hitObject;
    private decimal _multiplier = 1;
    private TrashController _trash;

    private void Start()
    {
        _mainCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        _player = GameObject.FindWithTag("Player");
        _balance = (BalanceController) _player.GetComponent(typeof(BalanceController));
    }


    private void Update()
    {
        if (!PauseMenuController.GetGameIsPaused())
        {
            GetClickedObject(); 
        }
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
                AddValue(Convert.ToDecimal(_trash.GetCurrencyValue()) * _multiplier);
                AddValueText(_trash);
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

    public void AddValueText(TrashController trash)
    {
        _mainCanvas.GetComponent<ValueDisplayer>().CreateText(trash);
    }

    public void IncreaseMultiplier(decimal value)
    {
        _multiplier = value;
    }

    public decimal GetMultiplier()
    {
        return _multiplier;
    }
}
