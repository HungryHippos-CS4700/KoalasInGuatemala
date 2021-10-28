using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    private Rigidbody2D rb;
    [SerializeField] private TreeBehavior treeBehavior;
    [SerializeField] private float speed;
    private float inputHorizontal;
    private float inputVertical;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movement input
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        if (treeBehavior.inTrunk)
        {
            rb.gravityScale = 0f;
            animator.SetBool("IsClimbing", true);
        }
        else
        {
            rb.gravityScale = 3f;
            animator.SetBool("IsClimbing", false);
        }
    }

    void FixedUpdate()
    {
        // Horizontal movement
        rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);

        // Vertical movement
        if (treeBehavior.inTrunk)
        {
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * speed);
        }

        if (Mathf.Abs(inputHorizontal) > 0 || (Mathf.Abs(inputVertical) > 0f && treeBehavior.inTrunk))
        {
            animator.SetBool("IsMoving", true);
            animator.SetFloat("Speed", inputHorizontal);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }
}