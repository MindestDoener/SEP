using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierDisplayController : MonoBehaviour
{
    private Camera _cam;

    private ObjectClickController _occ;
    private BalanceDisplayController _bdc;

    private Text _multiplierDisplay;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _multiplierDisplay = GetComponent<Text>();
        _occ = _cam.GetComponent<ObjectClickController>();
        _bdc = GetComponent<BalanceDisplayController>();
    }

    // Update is called once per frame

    public void UpdateDisplay()
    {
        _multiplierDisplay.text = _bdc.ShortenBalanceDisplay(_occ.GetMultiplier()) + "x";
    }
}
