using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{

    void Start()
    {

    }
    public void RetryBtn()
    {
        Debug.Log("Loading scene: GameScene");
        ResetGame.Reset();
    }

    public void QuitBtn()
    {
        Debug.Log("Loading scene: MainMenu");
        SceneManager.LoadScene("MainMenu");
    }
}
