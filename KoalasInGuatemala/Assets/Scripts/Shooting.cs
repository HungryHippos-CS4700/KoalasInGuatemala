using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    enum FireMode {
        SEMI,
        SPREAD,
        BURST
    }
    [SerializeField] private Animator animator;
    [SerializeField] private TreeBehavior treeBehavior;
    [SerializeField] private GameObject arm;
    [SerializeField] private Sprite[] gunSprites;
    [SerializeField] private FireMode fireMode;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioSource gunSound;
    [SerializeField] private float fireRate;
    [SerializeField] private float timeSinceLastShot;
    [SerializeField] private float cameraShakeOffset;
    [SerializeField] private bool isFiring;
    private Vector2 lookDirection;
    private float lookAngle;
    private bool canBurst;

    private void DisableBulletCollisionWithPlayer(Bullet bullet)
    {
        BoxCollider2D bulletCollider = bullet.GetComponent<BoxCollider2D>();
        BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(bulletCollider, playerCollider);
    }

    private IEnumerator BurstFire()
    {
        canBurst= false;
        for (int i = 0; i < 3; i++)
        {
            ShootingDefaults();
            Bullet bulletClone = Instantiate(bullet, firePoint.position, Quaternion.Euler(0f, 0f, lookAngle));
            bulletClone.speed = 30f;
            DisableBulletCollisionWithPlayer(bulletClone);
            yield return new WaitForSeconds(.1f);
        }
        canBurst = true;
    }

    // Everything to be done when a bullet is fired
    private void ShootingDefaults()
    {
        gunSound.Play();
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Random.Range(-cameraShakeOffset, cameraShakeOffset), Camera.main.transform.position.y + Random.Range(-cameraShakeOffset, cameraShakeOffset), -10f);
    }
    // Fire gun
    private void Fire()
    {
        
        switch (fireMode)
        {
            case FireMode.SEMI:
            {
                ShootingDefaults();
                fireRate = .25f;
                Bullet bulletClone = Instantiate(bullet, firePoint.position, Quaternion.Euler(0f, 0f, lookAngle));
                bulletClone.speed = 30f;
                DisableBulletCollisionWithPlayer(bulletClone);
                break;
            }

            case FireMode.SPREAD:
            {
                ShootingDefaults();
                fireRate = 1f;
                for (int i = 0; i < 5; i++)
                {
                    Bullet bulletClone = Instantiate(bullet, firePoint.position, Quaternion.Euler(0f, 0f, Random.Range(lookAngle - 10, lookAngle + 10)));
                    bulletClone.speed = 20f;
                    DisableBulletCollisionWithPlayer(bulletClone);
                }
                break;
            }

            case FireMode.BURST:
            {
                canBurst = true;
                fireRate = .5f;
                if (canBurst)
                    StartCoroutine(BurstFire());
                break;
            }
        }
        timeSinceLastShot = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeSinceLastShot = 0f;
        isFiring = false;
        fireMode = FireMode.SEMI;
    }

    // Update is called once per frame
    void Update()
    {
        if (treeBehavior.inTrunk)
        {
            arm.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            arm.GetComponent<SpriteRenderer>().enabled = true;
        }
        arm.GetComponent<SpriteRenderer>().sprite = gunSprites[(int)fireMode];
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(transform.position.x, transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        if ((lookAngle > 90 || lookAngle < -90) && !treeBehavior.inTrunk)
        {
            animator.SetBool("FacingRight", false);
            GetComponent<SpriteRenderer>().flipX = true;
            arm.transform.rotation = Quaternion.Euler(180f, 0f, -lookAngle);
        }
        else
        {
            animator.SetBool("FacingRight", true);
            GetComponent<SpriteRenderer>().flipX = false;
            arm.transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
        }
        firePoint.rotation = Quaternion.Euler(0f, 0f, lookAngle);

        // Shoot
        if (Input.GetMouseButtonDown(0) && !treeBehavior.inTrunk)
        {
            isFiring = !isFiring;
        }
        if (timeSinceLastShot >= fireRate)
        {
            if (isFiring && !treeBehavior.inTrunk)
            {
                Fire();
            }
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
    }
}
