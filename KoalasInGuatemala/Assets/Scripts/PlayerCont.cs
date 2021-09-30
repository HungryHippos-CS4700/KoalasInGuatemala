using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : PhysicsBase
{
    private float tempGravity;

    public float horizontalSpeed;

    public Vector2 velocityInTree;

    public bool isInTrunk;

    // Start is called before the first frame update
    void Start()
    {
        tempGravity = gravityFactor;
        isInTrunk = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is colliding with the trunk
        var otherName = other.gameObject.name;
        if (otherName == "Trunk")
        {
            isInTrunk = true;
            gravityFactor = 0;
            Debug.Log("entered Trunk");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player is colliding with the trunk
        var otherName = other.gameObject.name;
        if (otherName == "Trunk")
        {
            isInTrunk = false;
            gravityFactor = tempGravity;
            Debug.Log("exited Trunk");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal Movement
        desiredx = 0;
        if (Input.GetAxis("Horizontal") > 0)
        {
            desiredx = horizontalSpeed;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            desiredx = -horizontalSpeed;
        }

        desiredy = 0;

        // Vertical Movement only if inside of Trunk
        if (isInTrunk)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                desiredy = 6.5f;
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                desiredy = -6.5f;
            }
            velocityInTree = new Vector2(desiredx/10f, desiredy);
            // Debug.Log (velocityInTree);
            Vector2 move = velocityInTree * Time.deltaTime;
            transform.position += (Vector3) move;
        }
    }
}
