using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementInTree
{
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
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            desiredx = horizontalSpeed;
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            desiredx = -horizontalSpeed;
        }

        desiredy = 0;

        if (allowVertical)
        {
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                desiredy = verticalSpeed;
            }
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                desiredy = -verticalSpeed;
            }
        }
    }
}
