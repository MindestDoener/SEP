using UnityEngine;

public class CloudController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float offScreenOffset = 1f;
    [SerializeField] private float ySpawnOffset = 1f;
    private Camera _cam;
    private Vector3 _camCords;


    // Start is called before the first frame update
    private void Start()
    {
        _cam = Camera.main;
        _camCords = _cam.ScreenToWorldPoint(new Vector3(0, _cam.pixelHeight, _cam.nearClipPlane));
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += MoveObject();
        if (IsOutOfScreenLeft()) ResetPositon();
    }

    private Vector3 MoveObject()
    {
        var move = new Vector3(-1 * moveSpeed, 0, 0) * Time.deltaTime;
        return move;
    }

    private void ResetPositon()
    {
        transform.position = new Vector3(-_camCords.x + offScreenOffset, _camCords.y * 0.66f + Random.Range(-ySpawnOffset, ySpawnOffset));
    }

    private bool IsOutOfScreenLeft()
    {
        return transform.position.x < _camCords.x - offScreenOffset;
    }
}