using System;
using System.Collections;
using UnityEngine;

public class ObjectClickController : MonoBehaviour
{
    private BalanceController _balance;
    private RaycastHit2D _hit;
    private GameObject _hitObject;
    private Canvas _mainCanvas;
    private GameObject _player;
    private TrashController _trash;
    public PlayfabManager playfabManager;

    private void Start()
    {
        _mainCanvas = GameObject.Find("UICanvas").GetComponent<Canvas>();
        _player = GameObject.FindWithTag("Player");
        _balance = (BalanceController) _player.GetComponent(typeof(BalanceController));
    }


    private void Update()
    {
        if (!PauseMenuController.GetGameIsPaused() && !ShopController.IsPointerOverUI()) GetClickedObject();
    }

    private void GetClickedObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!(_hit.collider is null))
            {
                _hitObject = _hit.collider.gameObject;
                if (_hitObject == _player)
                {
                }
                else
                {
                    if (_hitObject.GetComponent<TrashController>().IsBeeingDestroyed) return;
                    _trash = _hitObject.GetComponent<TrashController>();
                    AddValue(_trash.GetCurrencyValue() * GameData.ClickMultiplier);
                    AddValueText(_trash);
                    StartCoroutine(DestroyObject(_hitObject));
                }
            }
        }
    }

    private void AddValue(float value)
    {
        _balance.AddBalance(value);
        playfabManager.SendLeaderboard();
    }

    public static IEnumerator DestroyObject(GameObject objectToDestroy)
    {
        objectToDestroy.GetComponent<TrashController>().IsBeeingDestroyed = true;
        objectToDestroy.GetComponentInChildren<ParticleSystem>().Play();
        objectToDestroy.GetComponent<Animation>().Play();
        TrashManager.IncreaseCount(objectToDestroy.name, objectToDestroy.GetComponent<TrashController>().GetRarity());
        yield return new WaitForSeconds(1f);
        Destroy(objectToDestroy);
    }

    private void AddValueText(TrashController trash)
    {
        _mainCanvas.GetComponent<ValueDisplayer>().CreateText(trash, true);
    }

    
}