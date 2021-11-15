using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBehavior : MonoBehaviour
{
    // private bool onRight;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private bool moveLeft;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Trunk")
        {
            Destroy(gameObject);
            TreeHealth.treeHealth -= 10;
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
