using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool isGameScene;
    public Animator transitionType;
    public Animator startOfGame;
    public float transitionTime = 1f;

    void Start() {
        transitionType.gameObject.SetActive(true);

        if (isGameScene) {
            transitionType.SetTrigger("End");
        } else {
            Debug.Log(startOfGame);
            startOfGame.gameObject.SetActive(true);
            startOfGame.SetTrigger("Start");
        }

    }
    public void StartBtn() {
        Debug.Log("Loading scene: Main");
        //SceneManager.LoadScene("Main");
        LoadNextLevel();
    }

    public void QuitBtn() {
        Debug.Log("Quitting to desktop.");
        Application.Quit();
    }

    public void LoadNextLevel() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) {
        // play animation
        transitionType.SetTrigger("Start");

        // wait
        yield return new WaitForSeconds(transitionTime);

        // load scene
        SceneManager.LoadScene(levelIndex);
    }
}
