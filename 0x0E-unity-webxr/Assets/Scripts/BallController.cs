using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float lateralSpeed = 2f;
    public GameObject currentBall;
    public Animator cameraAnimator;

    void Update()
    {
        if (currentBall == null) return;

        float horizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(lateralSpeed * horizontal, 0f, 0f);
        currentBall.transform.Translate(movement * Time.deltaTime, Space.World);
    }

    public void SetControlledBall(GameObject ball)
    {
        currentBall = ball;
        cameraAnimator.SetBool("IsPlaying", false);
    }
    public void UnSetControlledBall(GameObject ball)
    {
        if (currentBall == ball)
        {
            currentBall = null;
            cameraAnimator.SetBool("IsPlaying", true);
        }
    }
}
