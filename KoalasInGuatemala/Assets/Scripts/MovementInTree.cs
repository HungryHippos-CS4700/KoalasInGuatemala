using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementInTree : PhysicsBase
{

    // Start is called before the first frame update
    void Start()
    {
        allowVertical = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is colliding with the trunk
        var otherName = other.gameObject.name;
        if (otherName == "Trunk")
        {
            allowVertical = true;
            velocity.y = 0;
            Debug.Log("entered Trunk");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player is colliding with the trunk
        var otherName = other.gameObject.name;
        if (otherName == "Trunk")
        {
            allowVertical = false;
            Debug.Log("exited Trunk");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
