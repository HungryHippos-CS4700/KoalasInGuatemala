using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    private Rigidbody2D rb;
    [SerializeField] private TreeBehavior treeBehavior;
    [SerializeField] private float speed;
    [SerializeField] private OwlEnemy owl;
    private float inputHorizontal;
    private float inputVertical;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            Enemy newOwl = Instantiate(owl, new Vector3(Random.Range(-3f, 3f), Random.Range(-4f, 4f), 0), Quaternion.identity);
        }
        rb = GetComponent<Rigidbody2D>();
        treeBehavior.inTrunk = false;
    }

    // Update is called once per frame
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
            rb.gravityScale = 1f;
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