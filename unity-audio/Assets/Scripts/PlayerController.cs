using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1.0f; // Speed of the player movement
    public float jumpForce = 5.0f; // Force applied when the player jumps
    private bool isGrounded; // To check if the player is on the ground
    private Rigidbody rb; // Reference to the player's Rigidbody component
    //private float rotationSpeed = 1f;
    private Animator animator;
    private Camera mainCamera;

    private Sounds Sound_script;

    void Start()
    {
        mainCamera = Camera.main;
        Sound_script = GameObject.Find("SFX").GetComponent<Sounds>();

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

        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            //RotatePlayer(-rotationSpeed);
        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            //RotatePlayer(rotationSpeed);
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();
        Vector3 desiredMoveDirection = cameraForward * moveVertical + cameraRight * moveHorizontal;

        if (desiredMoveDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(cameraForward);


        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        transform.position = transform.position + desiredMoveDirection * speed * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.up, out hit, 1.1f))
        {
            if (!hit.collider.CompareTag("Player"))
            {
                isGrounded = true;
                animator.SetBool("IsGrounded", true);
                animator.SetBool("IsFalling", false);

                Material material = hit.collider.GetComponent<Renderer>().material;

                if (material.name == "Grass (Instance)")
                    Sound_script.surface = "Grass";
                else
                    Sound_script.surface = "not grass";

            }
        }
        else
        {
            isGrounded = false;
            animator.SetBool("IsGrounded", false);
        }

        if (transform.position.y < -20)
        {
            animator.SetBool("IsFalling", true);
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
