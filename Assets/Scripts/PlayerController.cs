using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CircleRendererController circleRenderer;
    [SerializeField] private float collectionRadius = 1f;

    public float CollectionRadius
    {
        get => collectionRadius;
        set => collectionRadius = value;
    }
    private void Update()
    {
        if (Math.Abs(circleRenderer.Radius - collectionRadius) > 0.1f) circleRenderer.Radius = collectionRadius;
    }
    public void UpdateCollectionRadius(float factor)
    {
        collectionRadius *= factor;
    }

    public bool IsInReach(Vector2 coordinates)
    {
        var distanceVector = (Vector2) transform.position - coordinates;

        return distanceVector.magnitude <= collectionRadius;
    }

 
}