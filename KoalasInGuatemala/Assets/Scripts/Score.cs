using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    public int scoreValue;
    [SerializeField] private Text scoreText;

    public void addScore(int points)
    {
        scoreValue += points;
        scoreText.text = "Score: " + scoreValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
