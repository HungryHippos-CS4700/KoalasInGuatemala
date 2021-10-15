using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MovementInTree
{
    public float horizontalSpeed;

    public float verticalSpeed;

    public bool facingRight;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rightVector;
        rightVector = new Vector3(-1.0f, 1.0f, 1.0f);
         if (transform.localScale== rightVector) {
            facingRight = true;
        }
        else{
            facingRight=false;
        }
        if (Input.GetAxisRaw("Horizontal") == 1 && !facingRight)
        {
            Flip();
        }
        if (Input.GetAxisRaw("Horizontal") == -1 && facingRight )
        {
            Flip();
        }

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
    void Flip (){
        facingRight=!facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
