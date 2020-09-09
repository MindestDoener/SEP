using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiplierDisplayController : MonoBehaviour
{
    private Camera _cam;

    private ObjectClickController _occ;

    private Text _multiplierDisplay;
    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        _multiplierDisplay = GetComponent<Text>();
        _occ = _cam.GetComponent<ObjectClickController>();
    }

    // Update is called once per frame

    public void UpdateDisplay()
    {
        _multiplierDisplay.text = _occ.GetMultiplier() + "x";
    }
}
