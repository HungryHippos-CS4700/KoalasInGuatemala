using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehavior : MonoBehaviour
{
    public bool inTrunk;
    [SerializeField] private EdgeCollider2D[] branchColliders;
    [SerializeField] private GameObject leaf;
    [SerializeField] private GameObject currentPlatform;
    private BoxCollider2D playerCollider;
    private bool onBranch;
    private float inputHorizontal;

    void Start()
    {
        inTrunk = false;
        playerCollider = GetComponent<BoxCollider2D>();
        onBranch = false;
    }

    private IEnumerator CreateLeafTrail()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject leafClone = Instantiate(leaf);
            leafClone.transform.position = transform.position - new Vector3(.5f * inputHorizontal, .5f, 0f);
            leafClone.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            leafClone.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1f, 4f) * -inputHorizontal, Random.Range(1f, 3f));
            Object.Destroy(leafClone, 0.25f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator DisableCollision()
    {
        EdgeCollider2D platformCollider = currentPlatform.GetComponent<EdgeCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Branch"))
        {
            onBranch = true;
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Branch"))
        {
            onBranch = false;
            currentPlatform = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Trunk"))
        {
            inTrunk = true;
            foreach (EdgeCollider2D branchCollider in branchColliders)
            {
                BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();
                Physics2D.IgnoreCollision(branchCollider, playerCollider);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Trunk"))
        {
            inTrunk = false;
            foreach (EdgeCollider2D branchCollider in branchColliders)
            {
                BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();
                Physics2D.IgnoreCollision(branchCollider, playerCollider, false);
            }
        }
    }
    
    void Update()
    {
        if(inTrunk)
        {
            currentPlatform = null;
        }
        inputHorizontal = Input.GetAxisRaw("Horizontal");

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && onBranch)
        {
            StartCoroutine(CreateLeafTrail());
        }

        if (Input.GetKeyDown(KeyCode.S) && currentPlatform)
        {
            StartCoroutine(DisableCollision());
        }
    }
}
