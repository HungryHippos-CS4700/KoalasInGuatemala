using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField] private GameObject explodeEffect;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Trunk"))
        {
            TreeHealth.treeHealth -= 2;
            Destroy(Instantiate(explodeEffect, transform.position, Quaternion.identity), .517f);
            audioManager.Play("Acorn_Hit", true);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("Throw_Acorn");
    }
}
