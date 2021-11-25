using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TreeHealth : MonoBehaviour
{
    private Image HealthBar;
    public static float treeHealth;
    [SerializeField] private float maxHealth;
    private float healthPct;
    public Gradient gradient;

    Color ColorFromGradient(float value)  // float between 0-1
    {
        return gradient.Evaluate(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        treeHealth = maxHealth;
        HealthBar = GetComponent<Image>();
    }

    public static void EndGame()
    {
        AudioManager.Instance.Stop("Wave");
        for(int i = 0; i < Shooting.ownedGuns.Length; i++)
        {
            Shooting.ownedGuns[i] = false;
        }
        PlayerController.gemCount = 0;
        Score.scoreValue = 0;
        WaveSpawner.state = WaveSpawner.SpawnState.COUNTING;
        
        print("You Lost!");
        SceneManager.LoadScene("GameScene");
    }

    void Update()
    {
        healthPct = treeHealth / maxHealth;
        HealthBar.fillAmount = healthPct;

        HealthBar.color = ColorFromGradient(healthPct);

        // Restart game upon getting 0 health
        if (treeHealth <= 0f)
        {
            TreeHealth.EndGame();
        }
    }

}
