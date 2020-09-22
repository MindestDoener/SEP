using System;
using System.Collections;
using UnityEngine;

class ValueTextAnimationController : MonoBehaviour
{
    float i = 1;
    Vector3 position;
    private void Start()
    {
        position = transform.position;
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
            position.y += 0.03f;
            GetComponent<Transform>().gameObject.transform.position = position;
            i -= 0.01f;
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(this.gameObject);
    }
}

    

