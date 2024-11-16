using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BallMove ball = other.GetComponent<BallMove>();
        if (ball != null)
        {
            ball.Boost();
            Destroy(gameObject);
        }
    }
}
