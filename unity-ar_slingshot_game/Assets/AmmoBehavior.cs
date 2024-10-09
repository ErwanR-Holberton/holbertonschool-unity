using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBehavior : MonoBehaviour
{
    public float distanceFromCamera = 0.5f; // Distance in meters in front of the camera
    private Camera arCamera;

    void Start()
    {
        arCamera = Camera.main;

        if (arCamera == null)
            Debug.LogError("AR Camera not found. Make sure the camera is tagged as 'MainCamera'.");
    }

    void Update()
    {
        if (arCamera != null)
        {
            Vector3 cameraForward = arCamera.transform.forward; // Calculate the new position in front of the camera
            Vector3 newPosition = arCamera.transform.position + cameraForward * distanceFromCamera;

            transform.position = newPosition;  // Set the sphere's position and rotation
            transform.rotation = arCamera.transform.rotation; // Optional: Align rotation with camera
        }
    }
}
