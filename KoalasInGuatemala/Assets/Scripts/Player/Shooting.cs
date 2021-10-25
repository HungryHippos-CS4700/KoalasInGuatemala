using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    enum FireMode {
        SEMI,
        SPREAD,
        BURST,
        RPG,
        AUTO,
        BRR
    }
    [SerializeField] private Animator animator;
    [SerializeField] private TreeBehavior treeBehavior;
    [SerializeField] private GameObject arm;
    [SerializeField] private Sprite[] gunSprites;
    [SerializeField] private FireMode fireMode;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private float fireRate;
    [SerializeField] private float nextFire;
    [SerializeField] private float cameraShakeOffset;
    [SerializeField] private bool isFiring;

    // Projectile types
    [SerializeField] private Bullet bullet;
    [SerializeField] private Rocket rocket;

    private Vector2 lookDirection;
    private float lookAngle;
    private bool canBurst;

    private void DisableBulletCollisionWithPlayer(Projectile projectile)
    {
        BoxCollider2D projectileCollider = projectile.GetComponent<BoxCollider2D>();
        BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(projectileCollider, playerCollider);
    }

    private IEnumerator BurstFire()
    {
        canBurst= false;
        for (int i = 0; i < 3; i++)
        {
            audioManager.Play("Auto");
            Bullet(30f, 30f, false, "Auto");
            yield return new WaitForSeconds(.1f);
        }
        canBurst = true;
    }

    // Everything to be done when a bullet is fired
    private void Bullet(float speed, float damage, bool spread, string audio)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Random.Range(-cameraShakeOffset, cameraShakeOffset),
                                                    Camera.main.transform.position.y + Random.Range(-cameraShakeOffset, cameraShakeOffset), -10f);
        audioManager.Play(audio);
        Bullet bulletClone;
        if (spread)
        {
            bulletClone = Instantiate(bullet, firePoint.position, Quaternion.Euler(0f, 0f, Random.Range(lookAngle - 10, lookAngle + 10)));
        }
        else
        {
            bulletClone = Instantiate(bullet, firePoint.position, Quaternion.Euler(0f, 0f, lookAngle));
        }
        bulletClone.damage = damage;
        bulletClone.speed = speed;
    }

    private void Rocket()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Random.Range(-cameraShakeOffset-.2f, cameraShakeOffset+.2f),
                                                    Camera.main.transform.position.y + Random.Range(-cameraShakeOffset-.2f, cameraShakeOffset+.2f), -10f);
        audioManager.Play("RPG");
        Rocket rocketClone;
        rocketClone = Instantiate(rocket, firePoint.position, Quaternion.Euler(0f, 0f, lookAngle));
        rocketClone.speed = 20f;
    }

    // Fire gun
    private void Fire()
    {
        switch (fireMode)
        {
            case FireMode.SEMI:
            {
                
                fireRate = 4f;
                Bullet(30f, 30f, false, "Pistol");
                break;
            }

            case FireMode.SPREAD:
            {
                fireRate = 1f;
                for (int i = 0; i < 5; i++)
                {
                    Bullet(20f, 40f, true, "Shotgun");
                }
                break;
            }

            case FireMode.BURST:
            {
                canBurst = true;
                fireRate = 2f;
                if (canBurst)
                    StartCoroutine(BurstFire());
                break;
            }

            case FireMode.RPG:
            {
                fireRate = 1f;
                Rocket();
                break;
            }

            case FireMode.AUTO:
            {
                fireRate = 10f;
                Bullet(35f, 20f, false, "Auto");
                break;
            }

            case FireMode.BRR:
            {
                fireRate = 100f;
                Bullet(40f, 1f, false, "Auto");
                break;
            }
        }
        nextFire = Time.time + 1f/fireRate;
    }

    // Start is called before the first frame update
    void Start()
    {
        isFiring = false;
        fireRate = 0.25f;
        nextFire = Time.time + fireRate;
        fireMode = FireMode.SEMI;
    }

    // Update is called once per frame
    void Update()
    {
        if (treeBehavior.inTrunk)
        {
            arm.GetComponent<SpriteRenderer>().enabled = false;
            nextFire = Time.time + 1f/fireRate;
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
        if (Time.time > nextFire)
        {
            if (isFiring && !treeBehavior.inTrunk)
            {
                Fire();
            }
        }
    }
}
