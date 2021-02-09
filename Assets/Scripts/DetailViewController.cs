using UnityEngine;

public class DetailViewController : MonoBehaviour
{
    private GameObject _collectionContainer;

    private void Start()
    {
        _collectionContainer = transform.parent.GetChild(0).gameObject;
    }

    public void Close()
    {
        _collectionContainer.SetActive(true);
        gameObject.SetActive(false);
    }
}