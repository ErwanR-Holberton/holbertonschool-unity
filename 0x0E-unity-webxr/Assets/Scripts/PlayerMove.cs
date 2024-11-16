using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    private BallController BallController;

    void Start()
    {
        BallController = FindObjectOfType<BallController>();
    }

    void Update()
    {
        if (BallController.currentBall == null) //only move player when no ball rolling
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");


            Vector3 movement = transform.forward;
            movement.y = 0;
            movement = movement * vertical + transform.right * horizontal;

            // Normalize the movement vector to avoid faster diagonal movement
            if (movement.magnitude > 1f)
                movement.Normalize();

            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
    }
}
