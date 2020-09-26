using UnityEngine;

public class TrashController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0;
    [SerializeField]
    private float currencyValue = 0;
    [SerializeField] 
    private float sinAmplitude = 1f;
    [SerializeField] 
    private float sinDilation= 1f;
    [SerializeField]
    private float destroyOffset = 0;
    private Camera _cam;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private Vector3 _camCords;
    private Rarity _rarity;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _cam = Camera.main;
        _camCords = _cam.ScreenToWorldPoint(new Vector3(0, _cam.pixelHeight, _cam.nearClipPlane));
        gameObject.tag = "Trash";
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += MoveObject();
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
    
    public Rarity GetRarity()
    {
        return _rarity;
    }

    public void SetRarity(Rarity rarity)
    {
        _rarity = rarity;
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

    private Vector3 MoveObject()
    {
        var xPosition = transform.position.x;
        var move = new Vector3(-1 * moveSpeed, Mathf.Sin(xPosition*sinAmplitude)*sinDilation, 0) * Time.deltaTime;
        return move;
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
