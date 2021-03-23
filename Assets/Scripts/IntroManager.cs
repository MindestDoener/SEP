using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Sprite MoeveTalking;

    public void OnSubmit()
    {
        var text = GameObject.FindWithTag("UsernameInput").GetComponent<Text>();
        GameData.Username = text.text;
        transform.GetChild(0).gameObject.SetActive(false);
        Time.timeScale = 1;
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        StartCoroutine(MoeweTalk());
    }

    public void OnSureBro()
    {
        StartCoroutine(MoeweTalk());
        transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text =
            "Nice Dude! I'll say this much: just tap it and you'll figure out the rest";
        var button = transform.GetChild(3).gameObject;
        button.GetComponentInChildren<Text>().text = "A'ight Mate!";
        button.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
        button.GetComponentInChildren<Button>().onClick.AddListener(OnAightMate);
    }

    public void OnAightMate()
    {
        StartCoroutine(EndIntro());
    }

    public IEnumerator MoeweTalk()
    {
        for (var i = 0; i < 4; i++)
        {
            var moewe = transform.GetChild(2).gameObject;
            var sprite = moewe.GetComponent<Image>().sprite;
            yield return new WaitForSeconds(0.3f);
            moewe.GetComponent<Image>().sprite = MoeveTalking;
            yield return new WaitForSeconds(0.4f);
            moewe.GetComponent<Image>().sprite = sprite;
        }
    }

    public IEnumerator EndIntro()
    {
        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.GetComponent<Image>().sprite = MoeveTalking;
        transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = "AEAEAEAE AEAEAEAE AEAEAEAE!!!!";
        yield return new WaitForSeconds(2f);
        SceneManager.UnloadSceneAsync("Intro");
    }
}