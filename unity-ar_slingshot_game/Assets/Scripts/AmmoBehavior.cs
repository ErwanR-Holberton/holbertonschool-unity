using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBehavior : MonoBehaviour
{
    public float distanceFromCamera = 1f;
    private Camera arCamera;
    private bool isDragging = false;
    private bool wasThrown = false;
    private bool hasTriggeredNextAmmo = false;
    private float initialTouchY, initialTouchX;
    private LineRenderer lineRenderer;

    private Vector3 centerPosition;
    private Rigidbody rb;

    private float factor = 10;

    private Start StartScript;
    private Text logs;



    void Start()
    {
        logs = GameObject.Find("Textlog").GetComponent<Text>();
        StartScript = GameObject.Find("Restart").GetComponent<Start>();
        arCamera = Camera.main;
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (transform.position.y < -20)
            disableSelf();

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
                else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && isDragging)
                {
                    ThrowAmmo();
                    isDragging = false;
                    wasThrown = true;
                    lineRenderer.enabled = false;
                }
            }
            if (isDragging)
            {
                lineRenderer.enabled = true;
                DrawTrajectory();
            }
        }
    }

    void CalculateCenterPosition()
    {
        Vector3 cameraForward = arCamera.transform.forward;
        centerPosition = arCamera.transform.position + cameraForward * distanceFromCamera;

        // Maintain the sphere in front of the camera when not dragging
        if (!isDragging)
            transform.position = centerPosition;
    }

    void detectTouch(Touch touch, Ray ray)
    {
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
        float deltaY = initialTouchY - touch.position.y;// Calculate the change in touch position
        float deltaX = initialTouchX - touch.position.x;

        // Move the sphere down based on the device's downward direction
        Vector3 downwardDirection = -arCamera.transform.up; // Inverse of camera up is down
        Vector3 newPosition = centerPosition + downwardDirection * (deltaY * 0.0001f) - arCamera.transform.right * (deltaX * 0.0001f); // Adjust the multiplier as needed


        // Ensure the sphere doesn't go below the maximum drag distance
        float distanceFromOriginal = Vector3.Distance(newPosition, transform.position);
        newPosition -= arCamera.transform.forward * 1f * distanceFromOriginal;
        transform.position = newPosition;
    }

    void ThrowAmmo()
    {
        // Calculate the direction to throw the ammo
        Vector3 throwDirection = (centerPosition - transform.position); // Direction from sphere to center position
        throwDirection = multiply_forward(throwDirection);
        Vector3 pos = centerPosition - throwDirection;
        throwDirection = throwDirection.normalized;
        float throwStrength = Vector3.Distance(centerPosition, pos); // Strength based on distance

        rb.useGravity = true;
        rb.AddForce(throwDirection * throwStrength * 500f); // 500f is a multiplier for force scaling
        transform.position = centerPosition;
    }

    Vector3 multiply_forward(Vector3 throwDirection)
    {
        float cameraForwardFactor = 5.0f; // Change this value as needed
        Vector3 forwardComponent = Vector3.Project(throwDirection, arCamera.transform.forward); // Project throwDirection onto camera.forward
        Vector3 otherComponents = throwDirection - forwardComponent; // Get the remaining components
        return (forwardComponent * cameraForwardFactor + otherComponents) * 5f; // Scale forward component and recombine
    }

    void DrawTrajectory()
    {
        int resolution = 250; // Number of points in the trajectory line
        Vector3[] points = new Vector3[resolution];

        Vector3 throwDirection = (centerPosition - transform.position);// Calculate the same throw direction and strength as in ThrowAmmo
        throwDirection = multiply_forward(throwDirection);
        Vector3 pos = centerPosition - throwDirection;
        throwDirection = throwDirection.normalized;
        float throwStrength = Vector3.Distance(centerPosition, pos);


        Vector3 velocity = throwDirection * throwStrength * factor; // Use the same initial velocity as the throw
        Vector3 currentPosition = transform.position;
        Vector3 gravity = Physics.gravity; // Gravity vector from Unity's physics system

        for (int i = 0; i < resolution; i++)
        {
            float time = i * 0.01f; // Time increments for each point (adjust as needed)

            // Calculate each point on the trajectory using the physics formula for position with constant acceleration (gravity)
            points[i] = centerPosition + velocity * time + 0.5f * gravity * time * time;
        }

        // Set the points in the LineRenderer
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            StartScript.updateScore(10);
            collision.gameObject.SetActive(false);
            TriggerNext();
        }
        else if (collision.gameObject.CompareTag("Plane"))
        {
            TriggerNext();
        }
    }
    private void TriggerNext()
    {
        if (!hasTriggeredNextAmmo)
        {
            StartScript.ConsumeAmmo();
            hasTriggeredNextAmmo = true;
        }
    }

    private void disableSelf()
    {
        TriggerNext();
        gameObject.SetActive(false);
    }

}
