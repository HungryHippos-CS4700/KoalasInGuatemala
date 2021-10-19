using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float speed;
    private Rigidbody2D rb;
    private float inputHorizontal;
    private float inputVertical;
    private bool allowVertical;
    private bool inTrunk;
    private bool onBranch;

    // Shooting variables
    public GameObject arm;
    public GameObject bullet;
    public Transform firePoint;
    public float bulletSpeed = 20;
    public float fireRate;
    public bool canFire;
    public bool isFiring;

    // Particle System variables
    public ParticleSystem leafTrail;
    private ParticleSystem.VelocityOverLifetimeModule leafTrailVelocity;

    // Animation variables
    public Animator animator;
    Vector2 lookDirection;
    public float lookAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        allowVertical = false;
        inTrunk = false;
        onBranch = false;
        leafTrailVelocity = leafTrail.velocityOverLifetime;

    }

    // Fire gun
    private IEnumerator Fire()
    {
        canFire = false;
        GameObject bulletClone = Instantiate(bullet);
        bulletClone.transform.position = firePoint.position;
        bulletClone.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
        bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
        Object.Destroy(bulletClone, 0.8f);
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    // Climbing tree
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Tree")
        {
            rb.gravityScale = 0f;
            allowVertical = true;
            animator.SetBool("IsClimbing", true);
            arm.GetComponent<SpriteRenderer>().enabled = false;
            inTrunk = true;
            // isFiring = false;
        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Tree")
        {
            rb.gravityScale = 1f;
            allowVertical = false;
            animator.SetBool("IsClimbing", false);
            arm.GetComponent<SpriteRenderer>().enabled = true;
            inTrunk = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Branch")
        {
            Debug.Log("aioserfjio");
            onBranch = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "Branch")
        {
            Debug.Log("left thahfdjasdf");
            onBranch = false;
        }
    }

    void createLeafTrail()
    {
        leafTrail.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // Gun pointer
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(transform.position.x, transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        firePoint.rotation = Quaternion.Euler(0f, 0f, lookAngle);

        if ((lookAngle > 90 || lookAngle < -90) && !inTrunk)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            arm.transform.rotation = Quaternion.Euler(180f, 0f, -lookAngle);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            arm.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
        }

        if (Input.GetMouseButtonDown(0) && !inTrunk)
        {
            isFiring = !isFiring;
        }
        if (isFiring && canFire && !inTrunk)
        {
            StartCoroutine(Fire());
        }
    }

    void FixedUpdate()
    {
        // Horizontal movement
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputHorizontal * speed, rb.velocity.y);

        // Vertical movement
        inputVertical = Input.GetAxisRaw("Vertical");
        if (allowVertical)
        {
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, inputVertical * speed);
        }

        if (Mathf.Abs(inputHorizontal) > 0 || (Mathf.Abs(inputVertical) > 0f && allowVertical))
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }

        if (Mathf.Abs(inputHorizontal) > 0 && onBranch)
        {
            leafTrailVelocity.x = 0.8f * inputHorizontal;
            createLeafTrail();
        }
    }
}
