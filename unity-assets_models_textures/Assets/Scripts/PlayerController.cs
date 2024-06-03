using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player movement
    public float jumpForce = 5.0f; // Force applied when the player jumps
    private bool isGrounded; // To check if the player is on the ground
    private Rigidbody rb; // Reference to the player's Rigidbody component

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            moveVertical = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            moveVertical = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            moveHorizontal = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            moveHorizontal = 1f;

        Vector3 newPosition = new Vector3(moveVertical, 0.0f, -moveHorizontal) * speed * Time.deltaTime;


        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        transform.Translate(newPosition);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1.1f))
        {
            if (!hit.collider.CompareTag("Player"))
                isGrounded = true;
        }
        else
            isGrounded = false;

    }

}
