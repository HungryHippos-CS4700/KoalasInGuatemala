using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public bool isGameScene;
    public Animator transitionType;
    public float transitionTime = 1f;

    void Start() {
        transitionType.gameObject.SetActive(true);


    }
    public void StartBtn() {
        Debug.Log("Loading scene: Main");
        SceneManager.LoadScene("GameScene");
    }

    public void QuitBtn() {
        Debug.Log("Loading scene: Main");
        SceneManager.LoadScene("MainMenu");
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
