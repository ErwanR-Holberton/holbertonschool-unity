using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBehavior : MonoBehaviour
{
    public float distanceFromCamera = 0.5f;
    private Camera arCamera;
    private bool isDragging = false;
    private bool wasThrown = false;
    private float initialTouchY, initialTouchX;
    private float maxDragDistance = 2.0f;

    private Vector3 centerPositon;
    private Rigidbody rb;

    public Text logs;

    void Start()
    {
        arCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        logs.text = "got body";
    }

    void Update()
    {
        if (!wasThrown)
        {
            CalculateCenterPosition();

            // Check for touch input
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Ray ray = arCamera.ScreenPointToRay(touch.position);

                if (touch.phase == TouchPhase.Began)
                    detectTouch(touch, ray);
                else if (touch.phase == TouchPhase.Moved && isDragging)
                    dragAmmo(touch);
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    ThrowAmmo();
                    isDragging = false;
                    wasThrown = true;
                }
            }
        }
    }

    void CalculateCenterPosition()
    {
        Vector3 cameraForward = arCamera.transform.forward;
        centerPositon = arCamera.transform.position + cameraForward * distanceFromCamera;

        // Maintain the sphere in front of the camera when not dragging
        if (!isDragging)
            transform.position = centerPositon;
    }

    void detectTouch(Touch touch, Ray ray)
    {
        logs.text = "touch";
                RaycastHit hit;
        // On touch start, cast a ray from the camera to the touch point
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform == transform)// If the sphere is hit, start dragging
            {
                isDragging = true;
                initialTouchX = touch.position.x; // Store the initial touch position
                initialTouchY = touch.position.y; // Store the initial touch position
            }
        }
    }

    void dragAmmo(Touch touch)
    {
        logs.text = "drag";
        float deltaY = initialTouchY - touch.position.y;// Calculate the change in touch position
        float deltaX = initialTouchX - touch.position.x;

        // Move the sphere down based on the device's downward direction
        Vector3 downwardDirection = -arCamera.transform.up; // Inverse of camera up is down
        Vector3 newPosition = centerPositon + downwardDirection * (deltaY * 0.0001f) - arCamera.transform.right * (deltaX * 0.0001f); // Adjust the multiplier as needed


        // Ensure the sphere doesn't go below the maximum drag distance
        float distanceFromOriginal = Vector3.Distance(newPosition, transform.position);
        logs.text = "" + distanceFromOriginal.ToString();
        transform.position = newPosition;
    }

    void ThrowAmmo()
    {
        // Calculate the direction to throw the ammo
        Vector3 throwDirection = (centerPositon - transform.position).normalized; // Direction from sphere to center position
        float throwStrength = Vector3.Distance(centerPositon, transform.position); // Strength based on distance

        logs.text = "throw " + throwStrength.ToString();
        rb.useGravity = true;
        rb.AddForce(throwDirection * throwStrength * 1000f); // 500f is a multiplier for force scaling
    }
}
