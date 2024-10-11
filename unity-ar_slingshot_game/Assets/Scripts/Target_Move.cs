using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.Collections;
using UnityEngine.UI;

public class Target_Move : MonoBehaviour
{
    private float speed = 0.1f;           // Movement speed
    private float maxTurnAngle = 20.0f;   // Maximum degrees to turn per frame

    public ARPlane arPlane;             // The AR plane we are moving on
    private Vector2[] planeBoundary;     // Boundary of the AR plane in 2D (local space)

    private float currentDirection;      // Current Y-axis rotation in degrees

    private Text logs;
    private void Start()
    {
        logs = GameObject.Find("Textlog").GetComponent<Text>();

        // Retrieve and store the ARPlane boundary
        NativeArray<Vector2> nativeBoundary= arPlane.boundary;
        planeBoundary = new Vector2[nativeBoundary.Length];
        nativeBoundary.CopyTo(planeBoundary);

        // Set a random initial direction (Y-axis rotation)
        currentDirection = UnityEngine.Random.Range(0f, 360f);
        transform.rotation = Quaternion.Euler(0, currentDirection, 0);

    }

    private void Update()
    {
        AdjustDirection();
        if (transform.position.y < arPlane.transform.position.y - 20)
            Respawn();

        Vector3 forwardMovement = transform.forward * speed * Time.deltaTime;
        Vector3 targetPosition = transform.position + forwardMovement;
        Vector3 constrainedPosition = StayOnPlane(targetPosition);
        transform.position = constrainedPosition;
    }

    private void AdjustDirection()
    {
        // Apply a small random change to the Y-axis direction
        float randomTurn = UnityEngine.Random.Range(-maxTurnAngle, maxTurnAngle);
        currentDirection += randomTurn;

        // Update the object's rotation based on the new direction
        transform.rotation = Quaternion.Euler(0, currentDirection, 0);
    }

    private Vector3 StayOnPlane(Vector3 worldPosition)
    {
        // Convert the world position to the ARPlane's local coordinate system
        Vector3 localPosition = arPlane.transform.InverseTransformPoint(worldPosition);
        Vector2 localPos2D = new Vector2(localPosition.x, localPosition.z); // Ignore Y-axis

        if (IsInsideBoundary(localPos2D, planeBoundary))  // Check if the new position is inside the AR plane boundary
            return arPlane.transform.TransformPoint(localPosition);  // Keep the object flat on the plane (lock Y-axis to 0)
        else
        {
            currentDirection += 180f;
            currentDirection += UnityEngine.Random.Range(-maxTurnAngle *2, maxTurnAngle*2);
            return transform.position;
        }
    }

    private bool IsInsideBoundary(Vector2 point, Vector2[] boundary)
    {
        // Ray-casting or winding number algorithm to check if point is inside the boundary polygon
        int n = boundary.Length;
        bool inside = false;

        for (int i = 0, j = n - 1; i < n; j = i++)
        {
            if (((boundary[i].y > point.y) != (boundary[j].y > point.y)) &&
                (point.x < (boundary[j].x - boundary[i].x) * (point.y - boundary[i].y) / (boundary[j].y - boundary[i].y) + boundary[i].x))
            {
                inside = !inside;
            }
        }
        return inside;
    }

    private void Respawn()
    {
        Vector2[] planeBoundary = new Vector2[arPlane.boundary.Length];
        arPlane.boundary.CopyTo(planeBoundary);

        List<int> indices = Triangulate(planeBoundary);
        Vector3 randomPositionOnPlane = GetRandomPointInPolygon(planeBoundary, indices);
        Vector3 worldPosition = arPlane.transform.TransformPoint(randomPositionOnPlane);
        worldPosition.y = arPlane.transform.position.y + 0.1f;
        transform.position = worldPosition;
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
