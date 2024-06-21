using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetString("previous_scene", "MainMenu");
    }
    public void LevelSelect(int level)
    {
        SceneManager.LoadScene("Level" + level.ToString("D2"));
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }
    public void Exit()
    {
        Debug.Log("Exited");
        Application.Quit();
    }
}
