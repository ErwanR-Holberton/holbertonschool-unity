using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private int score = 0;
    public int health = 5;
    public Text scoreText;
    public Text healthText;
    public Text WinLoseText;
    public Image WinLoseBG;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            score += 1;
            SetScoreText();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Trap"))
        {
            health -= 1;
            SetHealthText();
        }
        else if (other.CompareTag("Goal"))
        {
            WinLoseText.text = "You Win!";
            WinLoseText.color = Color.black;
            WinLoseBG.color = Color.green;
            WinLoseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.back * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("menu");

        if (health == 0)
        {
            WinLoseText.text = "Game Over!";
            WinLoseText.color = Color.white;
            WinLoseBG.color = Color.red;
            WinLoseBG.gameObject.SetActive(true);
            StartCoroutine(LoadScene(3));
        }
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void SetHealthText()
    {
        healthText.text = "Health: " + health.ToString();
    }

    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
