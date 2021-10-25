using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    public static int score;
    // [SerializeField] private Text scoreText;
    public Text scoreText;

    public static void addScore(int points)
    {
        score += points;
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            scoreText.text = "Score: " + score;
        }
        catch (NullReferenceException e)
        {
            // might slow things down
        }
    }
}
