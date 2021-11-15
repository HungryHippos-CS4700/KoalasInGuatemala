using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TreeHealth : MonoBehaviour
{
    public static int treeHealth;
    Text treeHealthText;
    
    // Start is called before the first frame update
    void Start()
    {
        treeHealthText = GetComponent<Text>();
        treeHealth = 100;
    }

    void Update()
    {
        treeHealthText.text = "Health: " + treeHealth;

        // Restart game upon getting 0 health
        if (treeHealth <= 0)
        {
            SceneManager.LoadScene("GameScene");
        }
    }

}
