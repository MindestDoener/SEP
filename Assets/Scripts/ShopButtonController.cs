using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonController : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    [SerializeField] 
    private GameObject _shopPanel;

    private bool _isActive;

    void Start()
    {
        _shopPanel.SetActive(false);
        _isActive = false;
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (_isActive)
        {
            _shopPanel.SetActive(false);
            _isActive = false;
        }
        else
        {
            _shopPanel.SetActive(true);
            _isActive = true;
        }

    }
}
