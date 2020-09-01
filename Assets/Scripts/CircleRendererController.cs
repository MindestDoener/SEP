using System;
using UnityEngine;

public class CircleRendererController : MonoBehaviour
{
    private const float CircleDegree = 360f;
    [SerializeField] private int pointCount = 36;
    [SerializeField] private Material material;
    private float _radius;

    public float Radius
    {
        get => _radius;
        set
        {
            _radius = value;
            RenderCircle();
        }
    }

    private void RenderCircle()
    {
        var mesh = new Mesh();
        var vertices = new Vector3[pointCount + 1];

        var angle = CircleDegree / pointCount;

        for (var i = 0; i < pointCount; i++)
        {
            var x = Math.Cos(DegToArc(angle * i)) * _radius;
            var y = Math.Sin(DegToArc(angle * i)) * _radius;
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
        Debug.Log("Circle rendered");
    }

    private static double DegToArc(float angle)
    {
        return angle * Math.PI / 180;
    }
}