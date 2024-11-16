using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void Start()
    {
        Debug.Log("start trap");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Hit {other.gameObject.name}");
        if (other.gameObject.name.StartsWith("Ball"))
        {
            GameObject ball = other.gameObject;

            transform.Find("Explosion").gameObject.SetActive(true);
            Destroy(ball);
            StartCoroutine(DestroyAfterDelay(gameObject, 1f));
        }
    }

    private IEnumerator DestroyAfterDelay(GameObject ball, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(ball);
    }

}
