using System;
using UnityEngine;

public class ValueDisplayer : MonoBehaviour
{
    public Font font;
    public Material material;

    public GameObject CreateText(TrashController trash)
    {
        var trashValue = new GameObject("ValueTextMesh");

        var textMesh = trashValue.AddComponent<TextMesh>();
        trashValue.AddComponent<ValueTextAnimationController>();
        textMesh.text = "+" + NumberShortener.ShortenNumber(trash.GetCurrencyValue() * ObjectClickController.GetMultiplier());
        textMesh.transform.position = trash.gameObject.transform.position;
        textMesh.font = font;
        trashValue.GetComponent<MeshRenderer>().material = material;
        trashValue.GetComponent<MeshRenderer>().sortingOrder = 6;

        return trashValue;
    }
}