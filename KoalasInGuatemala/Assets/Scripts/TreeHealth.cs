using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TreeHealth : MonoBehaviour
{
    public int treeHealth;
    [SerializeField] private Text treeHealthText;
    public void TakeDamage(int damage)
    {
        treeHealth -= damage;
        treeHealthText.text = "Health: " + treeHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        treeHealth = 100;
        treeHealthText.text = "Health: " + treeHealth;
    }

    void Update()
    {
        if (treeHealth <= 0)
        {
            // Application.LoadLevel(Application.loadedLevel);
            SceneManager.LoadScene("GameScene");
        }
    }

}
