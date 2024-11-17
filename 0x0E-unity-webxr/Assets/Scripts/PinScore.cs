using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScore : MonoBehaviour
{
    private float fallenThreshold = 0.3f;
    private bool hasFallen = false;
    private ScoreManager ScoreManager;
    private Vector3 originalPosition;

    void Start()
    {
        ScoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        originalPosition = transform.position;
    }

    void Update()
    {
        Vector3 pinUp = transform.up;
        float alignment = Vector3.Dot(pinUp, Vector3.up);

        if (alignment > fallenThreshold && !hasFallen)
        {
            hasFallen = true;
            ScoreManager.AddScore(1);
        }
        if (Vector3.Distance(transform.position, originalPosition) > 0.5f && !hasFallen)
        {
            hasFallen = true;
            ScoreManager.AddScore(1);
        }
        if (transform.position.y < -10 && !hasFallen)
        {
            hasFallen = true;
            ScoreManager.AddScore(1);
        }
    }
}
