using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    Toggle toggle;

    public void Start()
    {
        toggle = transform.Find("InvertYToggle").GetComponent<Toggle>();
        toggle.isOn = PlayerPrefs.GetInt("isInverted", 0) == 1;
    }
    public void Back()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("previous_scene"));
    }
    public void Apply()
    {
        PlayerPrefs.SetInt("isInverted", toggle.isOn ? 1 : 0);
        SceneManager.LoadScene(PlayerPrefs.GetString("previous_scene"));
    }
}
