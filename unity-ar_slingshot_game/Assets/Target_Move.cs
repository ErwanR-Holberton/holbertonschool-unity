using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using Unity.Collections;
using UnityEngine.UI;
using System;

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


}
