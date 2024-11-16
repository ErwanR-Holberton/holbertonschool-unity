using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private BallController BallController;
    private Rigidbody rb;
    private bool isBoosting = false;
    private float boostFactor = 5f;
    private float boostDuration = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetMass();
        BallController = FindObjectOfType<BallController>();
    }

    public void Boost()
    {
        if (!isBoosting)
            StartCoroutine(ApplyBoost());
    }

    private IEnumerator ApplyBoost()
    {
        isBoosting = true;
        Vector3 originalVelocity = rb.velocity;
        Vector3 boostedVelocity = rb.velocity * boostFactor;
        rb.velocity = boostedVelocity;

        float timeElapsed = 0f;
        while (timeElapsed < boostDuration)
        {
            rb.velocity = Vector3.Lerp(boostedVelocity, originalVelocity, timeElapsed / boostDuration);
            timeElapsed += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        rb.velocity = originalVelocity;

        isBoosting = false;
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
