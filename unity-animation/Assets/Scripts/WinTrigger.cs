using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{

    private Timer timerScript; // Reference to the Timer script on the player
    private Text timerText; // Reference to the Text component
    public GameObject WinCanvas;

    void Start()
    {
        timerScript = GameObject.Find("Player").GetComponent<Timer>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
    }


    void OnTriggerEnter(Collider other)
    {
        PauseMenu pauseMenu = GameObject.Find("Player").GetComponent<PauseMenu>();
        pauseMenu.enabled = false;
        Time.timeScale = 0f;
        WinCanvas.SetActive(true);
        timerScript.Win();
        timerScript.enabled = false;
        timerText.color = Color.green;
        timerText.fontSize = 60;
    }
}
