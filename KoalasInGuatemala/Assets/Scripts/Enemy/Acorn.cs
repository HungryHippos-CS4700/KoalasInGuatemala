using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    [SerializeField] private GameObject explodeEffect;
    [SerializeField] private int acornDamage;

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Trunk"))
        {
            TreeHealth.treeHealth -= acornDamage;
            Destroy(Instantiate(explodeEffect, transform.position, Quaternion.identity), .517f);
            AudioManager.Instance.Play("Acorn_Hit", true);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AudioManager.Instance.Play("Throw_Acorn");
    }
}
