using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    [SerializeField]
    private List<Button> _buttons;
    private ShopController _shop;

    void Start()
    {
        _shop = GetComponent<ShopController>();
    }
    
    void Update()
    {
        for (int i = 0; i < _buttons.Count; i++)
        {
            if (_shop.GetBalance() >= _shop.GetUpgradeCosts()[i])
            {
                _buttons[i].interactable = true;
            }
            else
            {
                _buttons[i].interactable = false;
            }
        }
        
    }
}
