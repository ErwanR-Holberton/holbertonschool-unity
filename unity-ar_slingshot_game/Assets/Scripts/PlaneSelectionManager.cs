using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


public class PlaneSelectionManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject searchingText;

    private ARPlaneManager arPlaneManager;
    private ARRaycastManager arRaycastManager;

    public static ARPlane selectedPlane;
    public Material planeMaterial;

    public Start startScript;

    void Start()
    {
        arPlaneManager = GetComponent<ARPlaneManager>();
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {
        // Detect user touch and tap
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Vector2 touchPosition = Input.GetTouch(0).position;
            if (TryGetTappedPlane(touchPosition, out ARPlane plane))
                SelectPlane(plane);
        }
    }

    bool TryGetTappedPlane(Vector2 touchPosition, out ARPlane tappedPlane)
    {
        tappedPlane = null;
        // Perform a raycast on the ARPlane from the touch position
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // Get the first plane hit by the raycast
            ARRaycastHit hit = hits[0];
            tappedPlane = arPlaneManager.GetPlane(hit.trackableId);
            if (tappedPlane != null)
                return true;
        }
        return false;
    }

    void SelectPlane(ARPlane plane)
    {
        selectedPlane = plane;

        DisableOtherPlanes(plane);

        startButton.SetActive(true);
        searchingText.SetActive(false);
        ApplyMaterialToSelectedPlane();
        startScript.arPlane = selectedPlane;
    }

    void DisableOtherPlanes(ARPlane selectedPlane)
    {
        foreach (ARPlane plane in arPlaneManager.trackables)// Loop through all the detected planes
            if (plane != selectedPlane) // Disable all planes except the selected one
                plane.gameObject.SetActive(false);

        arPlaneManager.enabled = false;  // Disable plane detection
    }

    void ApplyMaterialToSelectedPlane()
    {
        if (selectedPlane != null)
        {
            MeshRenderer planeMeshRenderer = selectedPlane.GetComponent<MeshRenderer>();

            if (planeMeshRenderer != null)
                planeMeshRenderer.material = planeMaterial;
            else
                Debug.LogWarning("MeshRenderer not found on the selected plane.");
        }
        else
            Debug.LogWarning("No plane selected.");
    }

}

