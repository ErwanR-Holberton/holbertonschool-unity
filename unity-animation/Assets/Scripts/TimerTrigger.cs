using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    private Timer timerScript; // Reference to the Timer script on the player


    void Start()
    {
        timerScript = GameObject.Find("Player").GetComponent<Timer>();
    }

    void OnTriggerExit(Collider other)
    {
        timerScript.enabled = true;
    }
}
