using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{

    public Timer timerScript; // Reference to the Timer script on the player
    public Text timerText; // Reference to the Text component

    void OnTriggerEnter(Collider other)
    {
        timerScript.enabled = false;
        timerText.color = Color.green;
        timerText.fontSize = 60;
    }
}
