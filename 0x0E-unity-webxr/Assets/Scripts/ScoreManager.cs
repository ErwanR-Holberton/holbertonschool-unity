using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
    public TMP_Text scoreText;

    public void AddScore(int toAdd)
    {
        score += toAdd;
        scoreText.text = $"{score}";
    }
}
