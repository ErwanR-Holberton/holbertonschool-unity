using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Next()
    {
        string name = SceneManager.GetActiveScene().name;
        int last = name.Length - 1;
        char[] chars = name.ToCharArray();
        chars[last] += (char)1;

        if (chars[last] > '3')
            SceneManager.LoadScene("MainMenu");
        else
            SceneManager.LoadScene(new string(chars));

    }
}
