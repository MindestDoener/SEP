using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCustomizerModelController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        var playerParts = player.GetComponentsInChildren(typeof(SpriteRenderer));
        var playerPreviewParts = transform.GetComponentsInChildren(typeof(Image));
        
        for (int i = 0; i < 9; i++)
        {
            if(((SpriteRenderer)playerParts[i + 1]).sprite == null)
            {
                ((Image)playerPreviewParts[i]).color = new Color(0, 0, 0, 0);
            }
            else
            {
                ((Image)playerPreviewParts[i]).sprite = ((SpriteRenderer)playerParts[i + 1]).sprite;
            }
        }
    }
}
