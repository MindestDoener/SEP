using System;
using System.Collections;
using UnityEngine;

class ValueTextAnimationController : MonoBehaviour
{
    float i = 1;
    private void Start()
    {
        StartCoroutine(FadingAnimation());
    }

    IEnumerator FadingAnimation()
    {
        while(i > 0)
        {
            Color color = Color.white;
            color.a = i;
            var fadingColor = color;
            GetComponent<TextMesh>().color = fadingColor;
            i = i - 0.01f;
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(this.gameObject);
    }
}

    

