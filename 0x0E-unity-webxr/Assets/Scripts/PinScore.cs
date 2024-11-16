using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinScore : MonoBehaviour
{
    private float fallenThreshold = 0.4f;
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
            Debug.Log($"Hit {alignment} < {fallenThreshold}");
            hasFallen = true;
            ScoreManager.AddScore(1);
        }
        if (Vector3.Distance(transform.position, originalPosition) > 0.5f && !hasFallen)
        {
            Debug.Log($"fell position");
            hasFallen = true;
            ScoreManager.AddScore(1);
        }
    }
}
