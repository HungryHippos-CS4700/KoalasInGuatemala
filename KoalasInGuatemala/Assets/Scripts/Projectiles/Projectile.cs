using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected AudioManager audioManager;
    // [SerializeField] protected BoxCollider2D playerCollider;
    public float speed;

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        audioManager = FindObjectOfType<AudioManager>();
        // playerCollider = FindObjectOfType<PlayerController>().GetComponent<BoxCollider2D>();
        // BoxCollider2D projectileCollider = GetComponent<BoxCollider2D>();
        // Physics2D.IgnoreCollision(projectileCollider, playerCollider);
        Physics2D.IgnoreLayerCollision(gameObject.layer, 9);
        Destroy(gameObject, 2f);
    }
}
