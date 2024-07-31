using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    public Timer timerScript;
    public GameObject canvas;
    private bool pause = false;
    [SerializeField] private AudioMixerSnapshot snapshot_normal, snapshot_pause;

    public void Start()
    {
        Time.timeScale = 1f;
        snapshot_normal.TransitionTo(0);
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
        snapshot_pause.TransitionTo(0);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        timerScript.enabled = true;
        canvas.SetActive(false);
        pause = false;
        snapshot_normal.TransitionTo(0);
    }
    public void Restart()
    {
        snapshot_normal.TransitionTo(0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        snapshot_normal.TransitionTo(0);
        SceneManager.LoadScene("MainMenu");
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
}
