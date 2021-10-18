using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBase : MonoBehaviour
{
    public Vector2 velocity;
    public float gravityFactor;
    public float desiredx;
    public float desiredy;
    public bool allowVertical;
    public bool onGround;

    public bool inTrunk;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Movement(Vector2 move, bool horizontal)
    {
        if (move.magnitude < 0.00001f)
        {
            return;
        }

        // if vertical movement is NOT allowed, i.e, player input
        if (!allowVertical)
        {
            // store the hits from the Cast() method
            RaycastHit2D[] hits = new RaycastHit2D[16];
            int cnt =
                GetComponent<Rigidbody2D>()
                    .Cast(move, hits, move.magnitude + 0.00001f);
        
            for (int i = 0; i < cnt; ++i)
            {
                // if the object hit is horizontal, i.e. a floor
                if (hits[i].normal.y > 0.001f && !horizontal)
                {
                    // allow horizontal movement
                    velocity = new Vector2(velocity.x, 0);
                    return;
                }
                // if (Mathf.Abs(hits[i].normal.x) > 0.001f && horizontal && hits[i].collider.gameObject.name != "Trunk")
                // {
                //     velocity = new Vector2(velocity.x, 0);
                //     return;
                // }
            }
        }

        transform.position += (Vector3) move;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // if vertical movement is allowed, i.e, player input
        if (allowVertical)
        {
            velocity.y = desiredy;
        }
        else
        // otherwise, gravity will be applied
        {
            // Makes the object fall
            Vector2 acceleration = 9.81f * Vector2.down * gravityFactor;
            velocity += acceleration * Time.fixedDeltaTime;
        }

        // Calculate how much to move
        Vector2 move = velocity * Time.fixedDeltaTime;

        // Move horizontally
        velocity.x = desiredx;
        Movement(new Vector2(move.x, 0), true);

        // Move vertically
        Movement(new Vector2(0, move.y), false);
    }
}
