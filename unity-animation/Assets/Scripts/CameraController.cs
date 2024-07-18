using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform playerTransform; // Reference to the player's Transform
    public float rotationSpeed = 5.0f; // Speed of camera rotation

    private Vector3 initialOffset; // Initial offset between the player and the camera
    private float yaw = 0.0f; // Horizontal rotation
    private float pitch = 0.0f; // Vertical rotation

    public bool isInverted;

    void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();

        isInverted = PlayerPrefs.GetInt("isInverted", 0) == 1; //false by default

        initialOffset = transform.position - playerTransform.position;
        yaw = transform.eulerAngles.y;//initial values
        pitch = transform.eulerAngles.x;
    }

    void LateUpdate()
    {
        yaw += Input.GetAxis("Mouse X") * rotationSpeed;

        if (isInverted)
            pitch += Input.GetAxis("Mouse Y") * rotationSpeed;
        else
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
        pitch = Mathf.Clamp(pitch, -89f, 89f); // Clamp the pitch to avoid flipping the camera

        Quaternion desiredRotation = Quaternion.Euler(pitch, yaw, 0.0f);
        //playerTransform.rotation = Quaternion.Euler(0, yaw, 0);

        // Calculate the new offset based on the player's rotation
        Vector3 rotatedOffset = desiredRotation * initialOffset;
        // Set the camera position to the desired position
        transform.position = playerTransform.position + rotatedOffset;
        // Set the camera rotation
        transform.rotation = desiredRotation;

    }

}
