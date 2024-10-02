using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
public class PlaneSelectionManager : MonoBehaviour
{
    public ARPlaneManager arPlaneManager;  // Reference to ARPlaneManager
    public GameObject startButtonPrefab;   // Reference to the Start button prefab
    private GameObject startButtonInstance; // Instance of the Start button
    
    public static ARPlane selectedPlane;// Static variable to store the selected ARPlane
    void Start()
    {
        // Initialize ARPlaneManager
        arPlaneManager = GetComponent<ARPlaneManager>();
    }
    void Update()
    {
        // Detect user touch and tap
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // Get the touch position
            Vector2 touchPosition = Input.GetTouch(0).position;
            if (TryGetTappedPlane(touchPosition, out ARPlane plane))
            {
                // Plane successfully tapped, process selection
                SelectPlane(plane);
            }
        }
    }

    bool TryGetTappedPlane(Vector2 touchPosition, out ARPlane tappedPlane)
    {
        tappedPlane = null;
        // Perform a raycast on the ARPlane from the touch position
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        if (arPlaneManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
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
        // Disable detection of other planes
        DisableOtherPlanes(plane);
        // Instantiate the Start button if it hasn't been created yet
        if (startButtonInstance == null)
        {
            // Create the Start button in the center of the screen
            startButtonInstance = Instantiate(startButtonPrefab, Vector3.zero, Quaternion.identity);
            // Set the position of the button to be at the center of the screen
            startButtonInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        }
    }
    void DisableOtherPlanes(ARPlane selectedPlane)
    {
        // Loop through all the detected planes
        foreach (ARPlane plane in arPlaneManager.trackables)
            if (plane != selectedPlane) // Disable all planes except the selected one
                plane.gameObject.SetActive(false);
        // Disable plane detection
        arPlaneManager.enabled = false;
    }
}
