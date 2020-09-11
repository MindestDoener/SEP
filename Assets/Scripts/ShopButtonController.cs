using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonController : MonoBehaviour
{
    private GameObject _shopPanel;

    private bool _isActive;

    void Start()
    {
        _shopPanel = GameObject.FindWithTag("ShopPanel");
        _shopPanel.SetActive(false);
        _isActive = false;
    }

    public void TogglePanel()
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
