using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera camera;
    private float zoomSpeed = 10f;
    private float minFOV = 15f;
    private float maxFOV = 60f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f)
        {
            camera.fieldOfView -= scroll * zoomSpeed;
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, minFOV, maxFOV);
        }
    }
}
