using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's Transform
    public float rotationSpeed = 5.0f; // Speed of camera rotation

    private Vector3 initialOffset; // Initial offset between the player and the camera
    private float yaw = 0.0f; // Horizontal rotation
    private float pitch = 0.0f; // Vertical rotation

    void Start()
    {
        // Calculate the initial offset between the camera and the player
        initialOffset = transform.position - playerTransform.position;

        // Initialize camera rotation based on current rotation
        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        // Get mouse input
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, -89f, 89f); // Clamp the pitch to avoid flipping the camera

        // Calculate the desired rotation based on the yaw and pitch
        Quaternion desiredRotation = Quaternion.Euler(pitch, yaw, 0.0f);

        // Rotate the player only around the y-axis (horizontal rotation)
        playerTransform.rotation = Quaternion.Euler(0, yaw, 0);

        // Calculate the new offset based on the player's rotation
        Vector3 rotatedOffset = desiredRotation * initialOffset;

        // Set the camera position to the desired position
        transform.position = playerTransform.position + rotatedOffset;

        // Set the camera rotation
        transform.rotation = desiredRotation;
    }
}
