using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInTree : PhysicsBase
{

    // Start is called before the first frame update
    void Start()
    {
        allowVertical = false;
        onGround = false;
        inTrunk = false;
        onBranch = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is colliding with the trunk
        var otherName = other.gameObject.name;

        if (otherName == "Tree")
        {
            allowVertical = true;
            Debug.Log("entered Tree");
        } else if (otherName == "Branch") {
            onBranch = true;
        }
        else if (otherName == "Ground")
        {
            onGround = true;
            Debug.Log("hit ground");
            Debug.Log("On ground?: " + onGround);
        }
        else if (otherName == "Trunk")
        {
            inTrunk = true;
            Debug.Log("entered Trunk");
        }
        else if (otherName == "Bullet") {
            isHit = true;
            Debug.Log("it's hit");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player is colliding with the trunk
        var otherName = other.gameObject.name;
        if (otherName == "Tree")
        {
            // velocity.y *= 0;
            allowVertical = false;
            Debug.Log("exited Tree");
        } else if (otherName == "Branch") {
            onBranch = false;
        }
        else if (otherName == "Ground")
        {
            onGround = false;
        }
        else if (otherName == "Trunk")
        {
            inTrunk = false;
            Debug.Log("exited Trunk");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
