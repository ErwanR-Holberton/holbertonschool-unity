using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Timer timerScript;
    public GameObject canvas;
    private bool pause = false;

    public void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pause)
                Resume();
            else
                Pause();
        }
    }


    public void Pause()
    {
        Time.timeScale = 0f;
        timerScript.enabled = false;
        canvas.SetActive(true);
        pause = true;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        timerScript.enabled = true;
        canvas.SetActive(false);
        pause = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
}
