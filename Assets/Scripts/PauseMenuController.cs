using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{

    public static bool GameIsPaused = false;
    private GameObject _pauseMenu;
    private GameObject[] _otherUI = new GameObject[2];

    private void Start()
    {
        _pauseMenu = GameObject.FindWithTag("PauseMenu");
        _pauseMenu.SetActive(false);
        _otherUI[0] = GameObject.FindWithTag("RightUI");
        _otherUI[1] = GameObject.FindWithTag("BalanceObject");
    }

    public void Pause()
    {
        _pauseMenu.SetActive(true);
        foreach (var ui in _otherUI)
        {
            ui.SetActive(false);
        }
        Time.timeScale = 0;
        GameIsPaused = true;
    }
    
    public void Resume()
    {
        _pauseMenu.SetActive(false);
        foreach (var ui in _otherUI)
        {
            ui.SetActive(true);
        }
        Time.timeScale = 1;
        GameIsPaused = false;
    }

}
