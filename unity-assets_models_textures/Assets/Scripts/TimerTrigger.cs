using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Timer timerScript; // Reference to the Timer script on the player


    void Update()
    {
    }

    void OnTriggerExit(Collider other)
    {
        timerScript.enabled = true;
    }
}
