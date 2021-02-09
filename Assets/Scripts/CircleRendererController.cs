using System;
using UnityEngine;

public class CircleRendererController : MonoBehaviour
{
    private const float CircleDegree = 360f;
    [SerializeField] private int pointCount = 36;
    [SerializeField] private int orderInLayer = 2;
    [SerializeField] private Material material;
    private bool _rendered;

    public bool Rendered
    {
        get => _rendered;
        set
        {
            _rendered = value;
            if (value)
                RenderCircle();
            else
                Hide();
        }
    }

    public void UpdateCircle()
    {
        if (_rendered) RenderCircle();
    }

    private void Hide()
    {
        GetComponent<MeshFilter>().mesh = new Mesh();
    }

    private void RenderCircle()
    {
        var mesh = new Mesh();
        var vertices = new Vector3[pointCount + 1];

        var angle = CircleDegree / pointCount;

        for (var i = 0; i < pointCount; i++)
        {
            var x = Math.Cos(DegToArc(angle * i)) * GameData.AutoCollectRange;
            var y = Math.Sin(DegToArc(angle * i)) * GameData.AutoCollectRange;
            vertices[i] = new Vector3((float) x, (float) y);
        }

        vertices[pointCount] = new Vector3(0f, 0f);

        mesh.vertices = vertices;

        var triangles = new int[pointCount * 3];

        for (var i = 0; i < pointCount; i++)
        {
            triangles[3 * i] = pointCount;
            triangles[3 * i + 1] = i;
            if (i < pointCount - 1)
                triangles[3 * i + 2] = i + 1;
            else
                triangles[3 * i + 2] = 0;
        }

        mesh.triangles = triangles;

        GetComponent<MeshFilter>().mesh = mesh;

        GetComponent<MeshRenderer>().material = material;
        GetComponent<MeshRenderer>().sortingOrder = orderInLayer;
    }

    private static double DegToArc(float angle)
    {
        return angle * Math.PI / 180;
    }
}