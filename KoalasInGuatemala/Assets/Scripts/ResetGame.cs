using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public static void Reset()
    {
        SceneManager.LoadScene("GameScene");
        if (AudioManager.Instance != null)
        {
            AudioListener.pause = false;
            AudioManager.Instance.Stop("Wave");
        }
        for (int i = 0; i < Shooting.ownedGuns.Length; i++)
        {
            Shooting.ownedGuns[i] = false;
        }
        PlayerController.gemCount = 0;
        Score.scoreValue = 0;
        WaveSpawner.state = WaveSpawner.SpawnState.COUNTING;
        WaveSpawner.waveCountdown = 1f;

    }
}
