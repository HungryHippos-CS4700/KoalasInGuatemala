using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBehavior : MonoBehaviour
{
    // private bool onRight;
    private Rigidbody2D rb;
    [SerializeField] private float speed;

    private bool moveLeft;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Trunk") {

        }
    }

    private IEnumerator test()
    {
        yield return new WaitForSeconds(1f);
        rb.velocity = new Vector2(-1000f, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (transform.position.x < 0f)
        {
            rb.velocity = new Vector2(2f, 0f);
        }
        else
        {
            // rb.sleepMode = RigidbodySleepMode2D.StartAwake;
            rb.velocity = new Vector2(1f, 0f);
            // StartCoroutine(test());
            
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
