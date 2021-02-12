using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    private List<Button> _buttons;
    private ShopController _shop;

    private void Start()
    {
        _shop = GetComponent<ShopController>();
        _buttons = _shop.GetButtons();
        StartCoroutine(UpdateButtons());
    }

    private void OnEnable()
    {
        if (!(_shop is null)) StartCoroutine(UpdateButtons());
    }

    private IEnumerator UpdateButtons()
    {
        while (true)
        {
            for (var i = 0; i < _buttons.Count; i++)
            {
                
                var interactable = (_shop.GetBalance() >= _shop.GetUpgradeCosts()[i]);
                _buttons[i].transform.GetChild(0).GetComponent<Button>().interactable = interactable;
                _buttons[i].transform.GetChild(5).gameObject.SetActive(!interactable);
            }

            yield return new WaitForSeconds(0.025f);
        }
    }
}