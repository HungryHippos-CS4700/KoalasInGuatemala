using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    private bool isTreeDeadDetected = false;

    private void Update() {
        if ((TreeHealth.treeHealth <= 0f) && (!isTreeDeadDetected)) {
            Debug.Log("test");
            Time.timeScale = 0;
            AudioListener.pause = true;
            gameOverUI.SetActive(true);

            isTreeDeadDetected = true;
        }
    }

    public void RestartGame() {
        DeactivateMenu();
        ResetGame.Reset();
    }

    public void DeactivateMenu() {
        Time.timeScale = 1;
        AudioListener.pause = false;
        gameOverUI.SetActive(false);
    }

    public void ReturnToMainMenu() {
        MainMenu.isFirstTime = false;
        Time.timeScale = 1;
        AudioListener.pause = true;
        SceneManager.LoadScene("MainMenu");
    }
}
