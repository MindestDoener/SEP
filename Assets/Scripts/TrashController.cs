using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0;
    [SerializeField]
    private float currencyValue = 0;
    [SerializeField]
    private float destroyOffset = 0;
    private Camera _cam;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private Vector3 _camCords;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _cam = Camera.main;
        _camCords = _cam.ScreenToWorldPoint(new Vector3(0, _cam.pixelHeight, _cam.nearClipPlane));
    }

    // Update is called once per frame
    void Update()
    {
        MoveObjectLeft();
        if (IsOutOfScreenLeft())
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
        spriteRenderer.sprite = sprite;
        gameObject.AddComponent<PolygonCollider2D>().isTrigger = true;
    }

    public float GetCurrencyValue()
    {
        return currencyValue;
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

    private bool IsOutOfScreenLeft()
    {
        return transform.position.x < _camCords.x - destroyOffset;
    }
}
