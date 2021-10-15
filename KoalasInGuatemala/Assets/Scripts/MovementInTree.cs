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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is colliding with the trunk
        var otherName = other.gameObject.name;

        if (otherName == "Tree" || otherName == "Branch")
        {
            allowVertical = true;
            Debug.Log("entered Trunk");
        }
        if(otherName == "Ground")
        {
            onGround = true;
            Debug.Log("hit ground");
            Debug.Log("On ground?: " + onGround);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player is colliding with the trunk
        var otherName = other.gameObject.name;
        if (otherName == "Tree" || otherName == "Branch")
        {
            // velocity.y *= 0;
            allowVertical = false;
            Debug.Log("exited Trunk");
        }

        if(otherName == "Ground")
        {
            onGround = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
