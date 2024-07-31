using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{

    private Timer timerScript; // Reference to the Timer script on the player
    private Text timerText; // Reference to the Text component
    public GameObject WinCanvas;
    private GameObject BGM;

    void OnTriggerEnter(Collider other)
    {
        timerScript = GameObject.Find("Player").GetComponent<Timer>();
        timerText = GameObject.Find("TimerText").GetComponent<Text>();
        PauseMenu pauseMenu = GameObject.Find("Player").GetComponent<PauseMenu>();
        BGM = GameObject.Find("BGM");
        BGM.SetActive(false);
        pauseMenu.enabled = false;
        Time.timeScale = 0f;
        WinCanvas.SetActive(true);
        timerScript.Win();
        timerScript.enabled = false;
        timerText.color = Color.green;
        timerText.fontSize = 60;
    }
}
