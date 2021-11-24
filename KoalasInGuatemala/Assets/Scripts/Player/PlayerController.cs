using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    private Rigidbody2D rb;
    [SerializeField] private TreeBehavior treeBehavior;
    [SerializeField] private float speed;
    public float powerUpSpeed;
    public float buySpeed;
    private float inputHorizontal;
    private float inputVertical;
    public Animator animator;
    public static int gemCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        powerUpSpeed = 1;
        buySpeed = 1;
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
        rb.velocity = new Vector2(inputHorizontal * speed * powerUpSpeed * buySpeed, rb.velocity.y);

        // Vertical movement
        if (treeBehavior.inTrunk)
        {
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * speed * powerUpSpeed * buySpeed);
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