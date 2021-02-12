using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtonController : MonoBehaviour
{
    private GameObject _shopPanel;
    private Animation _animation;
    private Button _button;

    private bool _isActive;

    void Start()
    {
        _shopPanel = GameObject.FindWithTag("ShopPanel");
        //_shopPanel.SetActive(false);
        _button = GetComponent<Button>();
        _animation = GameObject.FindWithTag("RightUI").GetComponent<Animation>();
        _isActive = false;
    }

    public void OpenPanel()
    {
        StartCoroutine(OpenPanelCoroutine());
    }

    public void ClosePanel()
    {
        StartCoroutine(ClosePanelCoroutine());
    }

    private IEnumerator OpenPanelCoroutine()
    {
        if (!_isActive)
        {
            _isActive = true;       
            _animation["ShopToggleAnimation"].speed = 1f;
            _animation.Play("ShopToggleAnimation");
            yield return new WaitForSeconds(_animation["ShopToggleAnimation"].length);
        }
    }
    
    private IEnumerator ClosePanelCoroutine()
    {
        _animation["ShopToggleAnimation"].speed = -1f;
        _animation["ShopToggleAnimation"].time = _animation ["ShopToggleAnimation"].length;
        _animation.Play("ShopToggleAnimation");
        yield return new WaitForSeconds(_animation["ShopToggleAnimation"].length);
        _isActive = false;
    }
    
    public void TogglePanel()
    {
        StartCoroutine(TogglePanelCoroutine());
    }
    
    private IEnumerator TogglePanelCoroutine()
    {
        _button.enabled = false;
        if (_isActive)
        {
            _animation["ShopToggleAnimation"].speed = -1f;
            _animation["ShopToggleAnimation"].time = _animation ["ShopToggleAnimation"].length;
            _animation.Play("ShopToggleAnimation");
            yield return new WaitForSeconds(_animation["ShopToggleAnimation"].length);
            _shopPanel.SetActive(false);
            _isActive = false;
        }
        else
        {
            _shopPanel.SetActive(true);
            _isActive = true;
            _animation["ShopToggleAnimation"].speed = 1f;
            _animation.Play("ShopToggleAnimation");
            yield return new WaitForSeconds(_animation["ShopToggleAnimation"].length);
        }
        _button.enabled = true;
    }
}
