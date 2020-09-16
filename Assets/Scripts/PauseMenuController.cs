using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    private readonly GameObject[] _otherUI = new GameObject[2];
    private GameObject _pauseMenu;

    public static bool GameIsPaused { get; private set; }

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
        foreach (var ui in _otherUI) ui.SetActive(false);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void Resume()
    {
        _pauseMenu.SetActive(false);
        foreach (var ui in _otherUI) ui.SetActive(true);
        Time.timeScale = 1;
        GameIsPaused = false;
    }
}
