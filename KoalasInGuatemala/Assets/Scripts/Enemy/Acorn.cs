using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour
{

    [SerializeField] private GameObject explodeEffect;
    
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Trunk"))
        {
            TreeHealth.treeHealth -= 2;
            Destroy(Instantiate(explodeEffect, transform.position, Quaternion.identity), .517f);
            Destroy(gameObject);
        }
    }
}
