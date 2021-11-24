using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    public int test;
    // Start is called before the first frame update
    
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerRB.velocity = new Vector2(1 * test, playerRB.velocity.y);
        }
    }
}
