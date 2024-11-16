using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private BallController BallController;

    void Start()
    {
        SetMass();
        BallController = FindObjectOfType<BallController>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("alley"))
        {
            BallController.SetControlledBall(gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name.StartsWith("alley"))
            BallController.UnSetControlledBall(gameObject);
    }

    private void SetMass()
    {
        float minMass = 5f;
        float maxMass = 10f;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.mass = Random.Range(minMass, maxMass);
    }

}
