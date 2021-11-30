using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused;
    //public Animator transitionType;
    //public float transitionTime = 1f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
        }
    }

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void ReturnToMainMenu()
    {
        MainMenu.isFirstTime = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void RestartGame()
    {
        DeactivateMenu();
        EndGame();
    }

    public static void EndGame()
    {
        AudioManager.Instance.Stop("Wave");
        for (int i = 0; i < Shooting.ownedGuns.Length; i++)
        {
            Shooting.ownedGuns[i] = false;
        }
        PlayerController.gemCount = 0;
        Score.scoreValue = 0;
        WaveSpawner.state = WaveSpawner.SpawnState.COUNTING;

        SceneManager.LoadScene("GameScene");
    }

    /*public void LoadPrevLevel() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }

    IEnumerator LoadLevel(int levelIndex) {
        // play animation
        transitionType.SetTrigger("Start");

        // wait
        yield return new WaitForSeconds(transitionTime);

        // load scene
        SceneManager.LoadScene(levelIndex);
    }*/
}
