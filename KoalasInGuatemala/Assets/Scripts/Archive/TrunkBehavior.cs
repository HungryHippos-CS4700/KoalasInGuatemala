using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkBehavior : MonoBehaviour
{
    public bool inTrunk;
    public BoxCollider2D[] branchColliders;

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

    // Start is called before the first frame update
    void Start()
    {
        inTrunk = false;
    }
}
