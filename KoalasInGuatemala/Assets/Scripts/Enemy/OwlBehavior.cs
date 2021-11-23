using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBehavior : MonoBehaviour
{
    // private bool onRight;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private GameObject explodeEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trunk")
        {
            TreeHealth.treeHealth -= 10;
            Destroy(Instantiate(explodeEffect, transform.position, Quaternion.identity), .517f);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.position.x < 0f)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            rb.velocity = new Vector2(2f, 0f);
        }
        else
        {
            rb.velocity = new Vector2(-2f, 0f);
        }
    }
}
