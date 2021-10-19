using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchPlatform : MonoBehaviour
{
    public GameObject player;
    [SerializeField] private BoxCollider2D platformCollider;

    private IEnumerator DisableCollision()
    {
        BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(.5f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        platformCollider = this.gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            if(player != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
    }
}
