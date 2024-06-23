using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Timer timerScript;
    public GameObject canvas;
    private bool pause = false;

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
}
