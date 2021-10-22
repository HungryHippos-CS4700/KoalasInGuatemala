using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehavior : MonoBehaviour
{
    public bool inTrunk;
    [SerializeField] private BoxCollider2D[] branchColliders;
    [SerializeField] private GameObject leaf;
    [SerializeField] private GameObject currentPlatform;
    private BoxCollider2D playerCollider;
    private bool onBranch;
    private float distance;
    private float inputHorizontal;
    private float playerHeight;

    // Start is called before the first frame update
    void Start()
    {
        inTrunk = false;
        playerCollider = GetComponent<BoxCollider2D>();
        onBranch = false;
        distance = 0.1f;
        playerHeight = playerCollider.size.y * transform.localScale.y;
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
        BoxCollider2D platformCollider = currentPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(.5f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Branch"))
        {
            currentPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Branch"))
        {
            currentPlatform = null;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Trunk"))
        {
            inTrunk = true;
            foreach (BoxCollider2D branchCollider in branchColliders)
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
            foreach (BoxCollider2D branchCollider in branchColliders)
            {
                BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();
                Physics2D.IgnoreCollision(branchCollider, playerCollider, false);
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(inTrunk)
        {
            currentPlatform = null;
        }
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        
        RaycastHit2D hitInfo = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - (playerHeight/2) - .1f), Vector2.down, distance);
        if (hitInfo.collider != null && hitInfo.collider.CompareTag("Branch"))
        {
            onBranch = true;
        }
        else
        {
            onBranch = false;
        }

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
