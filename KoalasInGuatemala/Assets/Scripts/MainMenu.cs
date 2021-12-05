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
    public static bool isFirstTime = true;
    public GameObject MainMenuHolder;
    public GameObject MainMenuUI;
    public GameObject TutorialUI;
    void Start()
    {
        Debug.Log("isFirstTime: " + isFirstTime);
        transitionType.gameObject.SetActive(true);

        if (AudioManager.Instance != null)
        {
            AudioListener.pause = false;
            AudioManager.Instance.Stop("Wave");
        }

        if (isGameScene)
        {
            transitionType.SetTrigger("End");
        }
        else
        {
            AudioManager.Instance.Play("Main_menu");
            if (isFirstTime)
            {
                Debug.Log(startOfGame);
                //startOfGame.gameObject.SetActive(true);
                startOfGame.SetTrigger("Start");
            }
            else
            {
                startOfGame.enabled = false;
                MainMenuHolder.transform.localPosition = new Vector3(0, 0, 0);

            }
        }

    }

    public void OnDestroy() {
        Debug.Log("is being destroyed");
    }
    public void StartBtn()
    {
        Debug.Log("Loading scene: Main");
        AudioListener.pause = false;
        AudioManager.Instance.Stop("Main_menu");
        LoadNextLevel();
    }

    public void TutorialBtn()
    {
        Debug.Log("Opening Tutorial Screen");
        MainMenuUI.SetActive(false);
        TutorialUI.SetActive(true);
    }

    public void TutorialBackBtn()
    {
        Debug.Log("Closing Tutorial Screen");
        MainMenuUI.SetActive(true);
        TutorialUI.SetActive(false);
    }

    public void QuitBtn()
    {
        Debug.Log("Quitting to desktop.");
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        Debug.Log("Setting transitionType.SetTrigger to Start");
        // play animation
        transitionType.SetTrigger("Start");

        Debug.Log("yield return new WaitForSeconds(transitionTime)");
        Debug.Log("Time.timeScale before (in coroutine): " + Time.timeScale);
        yield return new WaitForSeconds(transitionTime);

        // load scene
        Debug.Log("trying to loadscene");
        // SceneManager.LoadScene("GameScene");
        ResetGame.Reset();
        yield return null;
    }
}
