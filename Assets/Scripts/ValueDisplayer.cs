using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public class ValueDisplayer : MonoBehaviour
{
    
    public Font font;
    public Material material;
    private ObjectClickController _occ;
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
        _occ = _cam.GetComponent<ObjectClickController>();
    }
    public GameObject CreateText(TrashController trash)
    {
        GameObject trashValue = new GameObject("ValueTextMesh");

        TextMesh textMesh = trashValue.AddComponent<TextMesh>();
        trashValue.AddComponent<ValueTextAnimationController>();
        textMesh.text = "+" + Convert.ToDecimal(trash.GetCurrencyValue()) * _occ.GetMultiplier();
        textMesh.transform.position = trash.gameObject.transform.position;
        textMesh.font = font;
        trashValue.GetComponent<MeshRenderer>().material = material;
        trashValue.GetComponent<MeshRenderer>().sortingOrder = 6;

        return trashValue;
    }
}
