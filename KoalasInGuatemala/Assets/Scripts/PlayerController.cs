using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementInTree
{

    public float horizontalSpeed;
    public float verticalSpeed;
    public ParticleSystem leafTrail;

    public bool moving;

    // Start is called before the first frame update
    void Start()
    {
        isHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal Movement
        desiredx = 0;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            desiredx = horizontalSpeed;
            moving = true;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            desiredx = -horizontalSpeed;
            moving = true;
        }

        desiredy = 0;

        if (allowVertical)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                desiredy = verticalSpeed;
            }
            if (Input.GetAxisRaw("Vertical") < 0 && !onGround)
            {
                desiredy = -verticalSpeed;
            }
        }

        if (moving && Mathf.Abs(velocity.x) < 0.1f && onBranch) {
            moving = false;
            createLeafTrail();
        }
    }

    void createLeafTrail()
    {
        leafTrail.Play();
    }
}
