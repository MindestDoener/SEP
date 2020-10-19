using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class WearableItemController : MonoBehaviour
{
    public WearableItem ItemType;
    private GameObject _player;
    private GameObject _playerCustomizerModel;
    public void ChangeLook()
    {
        _player = GameObject.FindWithTag("Player");
        _playerCustomizerModel = GameObject.FindWithTag("PlayerCustomizerModel");
        Sprite ClickedItem = this.transform.GetChild(0).GetComponent<Image>().sprite;
        switch (ItemType)
        {
            case WearableItem.Bodys:
                _player.GetComponent<SpriteRenderer>().sprite = ClickedItem;
                _playerCustomizerModel.transform.GetChild(0).GetComponent<Image>().sprite = ClickedItem;
                break;
            case WearableItem.Shoes:
                _player.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = ClickedItem;
                _playerCustomizerModel.transform.GetChild(1).GetComponent<Image>().sprite = ClickedItem;
                break;
            case WearableItem.Pants:
                _player.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = ClickedItem;
                _playerCustomizerModel.transform.GetChild(2).GetComponent<Image>().sprite = ClickedItem;
                break;
            case WearableItem.Hats:
                _player.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = ClickedItem;
                _playerCustomizerModel.transform.GetChild(3).GetComponent<Image>().sprite = ClickedItem;
                break;
            case WearableItem.Faces:
                _player.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite = ClickedItem;
                _playerCustomizerModel.transform.GetChild(4).GetComponent<Image>().sprite = ClickedItem;
                break;
        }
    }
}
