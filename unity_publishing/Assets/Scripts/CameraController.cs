using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("No target assigned to camera.");
            return;
        }
        transform.position = player.transform.position + offset;

    }
}
