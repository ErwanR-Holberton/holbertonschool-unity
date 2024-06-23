using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public Text TimerText;
    private float timer = 0f; // Elapsed time in seconds


    void Update()
    {
        timer += Time.deltaTime;
        int minutes = (int)(timer / 60);
        int seconds = (int)(timer % 60);
        int milliseconds = (int)((timer * 100) % 100);

        TimerText.text = string.Format("{0:0}:{1:00}.{2:00}", minutes, seconds, milliseconds);
    }
    public void Win()
    {
        TextMeshProUGUI WinText = GameObject.Find("FinalTime").GetComponent<TextMeshProUGUI>();
        WinText.text = TimerText.text;
    }

}
