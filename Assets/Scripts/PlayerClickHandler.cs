using System;
using System.Collections;
using UnityEngine;

public class PlayerClickHandler : MonoBehaviour
{
    private Animation _animation;

    private void Start()
    {
        _animation = GameObject.FindWithTag("CharCustomizer").GetComponent<Animation>();
    }


    public void OpenCharCustomizer()
    {
        _animation["CharCustomizerInOut"].speed = 1;
        _animation.Play("CharCustomizerInOut");
    }

    private void CloseButton()
    {
        _animation["CharCustomizerInOut"].speed = -1;
        _animation["CharCustomizerInOut"].time = _animation["CharCustomizerInOut"].length;
        _animation.Play("CharCustomizerInOut");
    }
}
