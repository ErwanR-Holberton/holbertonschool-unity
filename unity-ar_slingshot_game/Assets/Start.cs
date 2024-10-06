using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.Collections;
using UnityEngine.UI;

public class Start : MonoBehaviour
{
    public GameObject prefabToInstantiate;

    public ARPlane arPlane;
    public Text logs;

    public void startFunction ()
    {
        // Get the boundary of the ARPlane (NativeArray<Vector2>)
        NativeArray<Vector2> nativeBoundary = arPlane.boundary;

        // Convert NativeArray<Vector2> to Vector2[]
        Vector2[] planeBoundary = new Vector2[nativeBoundary.Length];
        nativeBoundary.CopyTo(planeBoundary);
        logs.text = "" + planeBoundary[0] + " " + planeBoundary[1];

        if (planeBoundary.Length < 3)
        {
            Debug.LogWarning("ARPlane boundary is too small or not yet detected.");
            return;
        }

        List<int> indices = Triangulate(planeBoundary);

        for (int i = 0; i < 20; i++)
        {
            // Use center and size to ensure random points stay within the plane
            Vector3 randomPositionOnPlane = GetRandomPointInPolygon(planeBoundary, indices);
            Vector3 worldPosition = arPlane.transform.TransformPoint(randomPositionOnPlane);
            worldPosition.y = arPlane.transform.position.y + 0.1f; // Set the y position to match the ARPlane's height

            GameObject instantiatedObject = Instantiate(prefabToInstantiate, worldPosition, Quaternion.identity);
        }
    }

    List<int> Triangulate(Vector2[] boundary)
    {
        List<int> indices = new List<int>();
        for (int i = 1; i < boundary.Length - 1; i++)
        {
            indices.Add(0);      // The first vertex (pivot)
            indices.Add(i);      // Current vertex
            indices.Add(i + 1);  // Next vertex
        }
        return indices;
    }

    Vector3 GetRandomPointInPolygon(Vector2[] boundary, List<int> indices)
    {
        // Randomly pick a triangle from the triangulated polygon
        int triangleIndex = Random.Range(0, indices.Count / 3) * 3;

        // Get the vertices of the chosen triangle
        Vector2 p1 = boundary[indices[triangleIndex]];
        Vector2 p2 = boundary[indices[triangleIndex + 1]];
        Vector2 p3 = boundary[indices[triangleIndex + 2]];
        Vector2 randomPoint2D = GetRandomPointInTriangle(p1, p2, p3);

        return new Vector3(randomPoint2D.x, 0f, randomPoint2D.y);
    }

    Vector2 GetRandomPointInTriangle(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        float r1 = Random.Range(0f, 1f);
        float r2 = Random.Range(0f, 1f);

        if (r1 + r2 > 1f)
        {
            r1 = 1f - r1;
            r2 = 1f - r2;
        }

        float a = 1f - r1 - r2;
        return a * p1 + r1 * p2 + r2 * p3;
    }
}
