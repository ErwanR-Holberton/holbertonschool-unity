using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the player movement
    public float jumpForce = 5.0f; // Force applied when the player jumps
    private bool isGrounded; // To check if the player is on the ground
    private Rigidbody rb; // Reference to the player's Rigidbody component
    private float rotationSpeed = 5f;
    private Animator animator;

    void Start()
    {
        Transform tyTransform = transform.Find("ty");
        if (tyTransform != null)
            animator = tyTransform.GetComponent<Animator>();
        else
            Debug.LogError("'ty' child not found.");

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;
        animator.SetBool("IsMoving", false);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            moveVertical = 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            moveVertical = -1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveHorizontal = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveHorizontal = 1f;
        }
        if (moveHorizontal != 0 || moveVertical != 0)
            animator.SetBool("IsMoving", true);

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            RotatePlayer(-rotationSpeed);
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            RotatePlayer(rotationSpeed);

        Vector3 newPosition = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed * Time.deltaTime;


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
            {
                isGrounded = true;
                animator.SetBool("IsGrounded", true);
            }
        }
        else
        {
            isGrounded = false;
            animator.SetBool("IsGrounded", false);
        }

        if (transform.position.y < -20)
        {
            transform.position = new Vector3(0, 20, 0);
        }

    }


    private void RotatePlayer(float rotationAmount)
    {
        Vector3 currentEulerAngles = transform.eulerAngles;
        currentEulerAngles.y += rotationAmount;
        transform.eulerAngles = currentEulerAngles;
    }

}
