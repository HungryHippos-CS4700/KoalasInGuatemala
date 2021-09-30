using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : PhysicsBase
{
    public bool isInTrunk;

    private float tempGravity;

    // Start is called before the first frame update
    void Start()
    {
        isInTrunk = false;
        Debug.Log(gravityFactor);
        tempGravity = gravityFactor;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is colliding with the trunk
        var otherName = other.gameObject.name;
        if (otherName == "Trunk")
        {
            isInTrunk = true;
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
            Debug.Log("exited Trunk");
        }
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(tempGravity);
        // Horizontal Movement
        desiredx = 0;
        if (Input.GetAxis("Horizontal") > 0)
        {
            desiredx = 3;
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            desiredx = -3;
        }

        desiredy = 0;

        // Vertical Movement only if inside of Trunk
        if (isInTrunk)
        {
            gravityFactor = 0;
            if (Input.GetAxis("Vertical") > 0)
            {
                desiredy = 6.5f;
            }
            if (Input.GetAxis("Vertical") < 0)
            {
                desiredy = -6.5f;
            }
        }
        else if (!isInTrunk)
        {
            gravityFactor = tempGravity;
        }
    }
}
