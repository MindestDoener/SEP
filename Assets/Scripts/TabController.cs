using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{

    [SerializeField] private GameObject menuToToggle;
    private Toggle _tab;

    private void Start()
    {
        _tab = GetComponent<Toggle>();
    }

    public void Toggle()
    {
        menuToToggle.SetActive(_tab.isOn);
        if(menuToToggle.gameObject.name == "Leaderboard")
        {
            menuToToggle.transform.GetComponent<LeaderboardTableController>().GetLeaderboard();
        }
    }
}
