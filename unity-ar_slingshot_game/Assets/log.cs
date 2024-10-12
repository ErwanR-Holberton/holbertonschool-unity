using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class log : MonoBehaviour
{
    private Text logs;

    public void Start()
    {
        logs = GameObject.Find("Textlog").GetComponent<Text>();
        logs.text = "spawned tower";
    }
}
