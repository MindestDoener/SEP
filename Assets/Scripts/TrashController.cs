using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{

    public float moveSpeed;
    public float currencyValue;
    public float destroyOffset;
    public Camera cam;
    public SpriteRenderer SpriteRenderer;
    private Vector3 _camCords;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        _camCords = cam.ScreenToWorldPoint(new Vector3(0, cam.pixelHeight, cam.nearClipPlane));
    }

    // Update is called once per frame
    void Update()
    {
        MoveObjectLeft();
        if (CheckIfOutOfScreen())
        {
            DestroyObject();
        }
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetCurrencyValue(float value)
    {
        currencyValue = value;
    }

    public void SetSprite(Sprite sprite)
    {
        SpriteRenderer.sprite = sprite;
    }

    private void MoveObjectLeft()
    {
        var move = new Vector3(-1 * moveSpeed, 0, 0);
        transform.position += move * Time.deltaTime;
    }

    private void DestroyObject()
    {
        Destroy (this.gameObject);
    }

    private bool CheckIfOutOfScreen()
    {
        return transform.position.x < _camCords.x - destroyOffset;
    }
}
