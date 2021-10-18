using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementInTree
{

    public float horizontalSpeed;
    public float verticalSpeed;
    public bool movingRight;
    public bool moving;
    public ParticleSystem leafTrail;
    private ParticleSystem.VelocityOverLifetimeModule leafTrailVelocity;

    // Start is called before the first frame update
    void Start()
    {
        leafTrailVelocity = leafTrail.velocityOverLifetime;
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
            movingRight = true;
            moving = true;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            desiredx = -horizontalSpeed;
            movingRight = false;
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

        if (moving && Mathf.Abs(velocity.x) < 0.0001f && onBranch)
        {
            if (movingRight)
            {
                leafTrailVelocity.x = 0.8f;
            } else {
                leafTrailVelocity.x = -0.8f;
            }
            createLeafTrail();
            moving = false;

        }
    }

    void createLeafTrail()
    {
        leafTrail.Play();
    }
}
