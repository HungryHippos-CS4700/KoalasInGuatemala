using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementInTree
{
    public ParticleSystem dust;

    public float horizontalSpeed;

    public float verticalSpeed;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal Movement
        desiredx = 0;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            // Debug.Log("am i here??? > 0");
            desiredx = horizontalSpeed;
            // createDust();
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            // Debug.Log("am i here??? < 0");
            desiredx = -horizontalSpeed;
            // createDust();
        }

        desiredy = 0;

        if (allowVertical)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                desiredy = verticalSpeed;
            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                desiredy = -verticalSpeed;
            }
        }
    }

    void createDust() {
        dust.Play();
    }
}
