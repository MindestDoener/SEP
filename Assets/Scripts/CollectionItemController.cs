using System;
using UnityEngine;
using UnityEngine.UI;

public class CollectionItemController : MonoBehaviour
{
    private static GameObject _detailView;

    private TrashScriptableObject _trashObject;

    public static void SetDetailView(GameObject detailView)
    {
        _detailView = detailView;
    }

    public TrashScriptableObject GetTrashObject()
    {
        return _trashObject;
    }

    public void SetTrashObject(TrashScriptableObject value)
    {
        _trashObject = value;
    }

    public void ShowDetails()
    {
        _detailView.SetActive(true);
        _detailView.transform.GetChild((int) DetailViewComponents.NameText).GetComponent<Text>().text = _trashObject.Name;
        _detailView.transform.GetChild((int) DetailViewComponents.ItemImage).GetComponent<Image>().sprite = _trashObject.Sprite;
        _detailView.transform.GetChild((int) DetailViewComponents.RarityText).GetComponent<Text>().text = _trashObject.Rarity.ToString();
        _detailView.transform.GetChild((int) DetailViewComponents.RarityText).GetComponent<Text>().color = TrashManager.GetRarityColor(_trashObject.Rarity);
        _detailView.transform.GetChild((int) DetailViewComponents.DescriptionText).GetComponent<Text>().text = _trashObject.Description;
        _detailView.transform.GetChild((int) DetailViewComponents.CountNumberText).GetComponent<Text>().text = NumberShortener.ShortenNumber(_trashObject.Count);
        _detailView.transform.GetChild((int) DetailViewComponents.BaseValueNumberText).GetComponent<Text>().text =
            NumberShortener.ShortenNumber(Convert.ToDecimal(_trashObject.Value));
        _detailView.transform.GetChild((int) DetailViewComponents.CurrentValueNumberText).GetComponent<Text>().text =
            NumberShortener.ShortenNumber(Convert.ToDecimal(_trashObject.Value) *
                                          ObjectClickController.GetMultiplier());
        transform.parent.gameObject.SetActive(false);
    }
}