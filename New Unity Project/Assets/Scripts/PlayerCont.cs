using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCont : PhysicsBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public bool isInTrunk() {
        // Check if the player is colliding with the trunk
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal Movement
        desiredx = 0;
        if (Input.GetAxis("Horizontal") > 0) {
            desiredx = 3;
        }
        if (Input.GetAxis("Horizontal") < 0) {
            desiredx = -3;
        }

        desiredy = 0;
        // Vertical Movement only if inside of Trunk
        if (isInTrunk()) {
            if (Input.GetAxis("Vertical") > 0) {
                desiredy = 6.5f;
            }
            if (Input.GetAxis("Vertical") < 0) {
                desiredy = -6.5f;
            }
        }
    }
}
