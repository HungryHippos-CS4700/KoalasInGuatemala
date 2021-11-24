using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public static int scoreValue;
    public static TextMeshProUGUI scoreText;
    public static float fontSize;
    [SerializeField] private LeanTweenType easeType;

    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
        scoreText = GetComponent<TextMeshProUGUI>();
        fontSize = 15f;
    }

    // void Update()
    // {
    //     scoreText.fontSize = fontSize;
    // }

    public static void UpdateScore(int score)
    {
        scoreValue += score;
        scoreText.text = "" + scoreValue;
        LeanTween.scale(scoreText.gameObject, new Vector2(1.25f, 1.25f), 0.05f);
        // LeanTween.rotate(scoreText.gameObject, new Vector3(0f, 0f, Random.Range(-30, 30)), 0.05f);
        LeanTween.scale(scoreText.gameObject, new Vector2(1f, 1f), 0.05f).setDelay(0.05f);
        // LeanTween.rotate(scoreText.gameObject, new Vector3(0f, 0f, 0f), 0.05f).setDelay(0.08f);;
    }
}
