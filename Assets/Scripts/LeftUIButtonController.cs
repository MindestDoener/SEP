using System;
using System.Collections;
using UnityEngine;

public class LeftUIButtonController : MonoBehaviour
{
    private Animation _animation;
    private Boolean _isenabled;

    private void Start()
    {
        _animation = GameObject.FindWithTag("CharCustomizer").GetComponent<Animation>();
    }


    public void OpenCharCustomizer()
    {
        if (!_isenabled)
        {
            _animation["CharCustomizerInOut"].speed = 1;
            _animation.Play("CharCustomizerInOut");
            _isenabled = true;
        }
    }

    public void CloseButton()
    {
            _animation["CharCustomizerInOut"].speed = -1;
            _animation["CharCustomizerInOut"].time = _animation["CharCustomizerInOut"].length;
            _animation.Play("CharCustomizerInOut");
            _isenabled = false;
    }
}
