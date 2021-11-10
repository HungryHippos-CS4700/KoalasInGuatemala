using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    public static int scoreValue;
    public static Text scoreText;

    public static void AddScore(int points)
    {
        scoreValue += points;
        scoreText.text = "Score: " + scoreValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreValue = 0;
    }
}
