using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public enum FireMode {
        SEMI,
        SPREAD,
        BURST,
        RPG,
        AUTO,
        BRR
    }
    public FireMode fireMode;
    public static bool[] ownedGuns = {false, false, false, false};
    public static bool pauseShooting = false;
    [SerializeField] private Animator animator;
    [SerializeField] private TreeBehavior treeBehavior;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject arm;
    [SerializeField] private Sprite[] gunSprites;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate;
    private float powerUpRate;
    private float buyRate;
    [SerializeField] private float nextFire;
    [SerializeField] private float cameraShakeOffset;
    [SerializeField] private bool isFiring;
    private bool canBurst;
    public bool hasPowerUp;
    public bool powerUpSpawned;

    // Projectile types
    [SerializeField] private Bullet bullet;
    private int powerUpDamage;
    private int buyDamage;
    private int instakill;
    [SerializeField] private Rocket rocket;

    private Vector2 lookDirection;
    private float lookAngle;

    private IEnumerator PowerUpTime(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        powerUpDamage = 1;
        powerUpRate = 1;
        instakill = 1;
        player.powerUpSpeed = 1f;
        hasPowerUp = false;
    }

    public void PowerUp(string mode, int seconds)
    {
        hasPowerUp = true;
        switch (mode)
        {
            case "damage":
            {
                powerUpDamage = 2;
                break;
            }

            case "rate":
            {
                powerUpRate = 2;
                break;
            }

            case "speed":
            {
                player.powerUpSpeed = 1.5f;
                break;
            }
            
            case "insta":
            {
                instakill = 1000;
                break;
            }
        }
        StartCoroutine(PowerUpTime(seconds));
    }
    
    private IEnumerator BurstFire()
    {
        canBurst= false;
        for (int i = 0; i < 3; i++)
        {
            AudioManager.Instance.Play("Auto");
            Bullet(80f, 25, false, "Auto");
            yield return new WaitForSeconds(.075f);
        }
        canBurst = true;
    }

    private void Bullet(float speed, int damage, bool spread, string audio)
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Random.Range(-cameraShakeOffset, cameraShakeOffset),
        Camera.main.transform.position.y + Random.Range(-cameraShakeOffset, cameraShakeOffset), -10f);
        AudioManager.Instance.Play(audio);
        Bullet bulletClone;
        if (spread)
        {
            bulletClone = Instantiate(bullet, firePoint.position, Quaternion.Euler(0f, 0f, Random.Range(lookAngle - 10, lookAngle + 10)));
        }
        else
        {
            bulletClone = Instantiate(bullet, firePoint.position, Quaternion.Euler(0f, 0f, lookAngle));
        }
        bulletClone.damage = damage * buyDamage * powerUpDamage * instakill;
        bulletClone.speed = speed;
    }

    private void Rocket()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + Random.Range(-cameraShakeOffset-.2f, cameraShakeOffset+.2f),
        Camera.main.transform.position.y + Random.Range(-cameraShakeOffset-.2f, cameraShakeOffset+.2f), -10f);
        AudioManager.Instance.Play("RPG");
        Rocket rocketClone;
        rocketClone = Instantiate(rocket, firePoint.position, Quaternion.Euler(0f, 0f, lookAngle));
        rocketClone.damage = 80 * buyDamage * powerUpDamage * instakill;
        rocketClone.speed = 20f;
    }

    // Fire gun
    private void Fire()
    {
        switch (fireMode)
        {
            case FireMode.SEMI:
            {
                fireRate = 4f * buyRate * powerUpRate;
                Bullet(80f, 20, false, "Pistol");
                break;
            }

            case FireMode.SPREAD:
            {
                fireRate = 1f * buyRate * powerUpRate;
                for (int i = 0; i < 5; i++)
                {
                    Bullet(40f, 40, true, "Shotgun");
                }
                break;
            }

            case FireMode.BURST:
            {
                canBurst = true;
                fireRate = 1.5f * buyRate * powerUpRate;
                if (canBurst)
                    StartCoroutine(BurstFire());
                break;
            }

            case FireMode.RPG:
            {
                fireRate = 2f * buyRate * powerUpRate;
                Rocket();
                break;
            }

            case FireMode.AUTO:
            {
                fireRate = 12f * buyRate * powerUpRate;
                Bullet(80f, 15, false, "Auto");
                break;
            }

            case FireMode.BRR:
            {
                fireRate = 100f;
                Bullet(40f, 6, false, "Auto");
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
        powerUpRate = 1f;
        buyRate = 1f;
        nextFire = Time.time + fireRate;
        fireMode = FireMode.SEMI;
        hasPowerUp = false;
        powerUpSpawned = false;
        powerUpDamage = 1;
        buyDamage = 1;
        instakill = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (treeBehavior.inTrunk)
        {
            arm.GetComponent<SpriteRenderer>().enabled = false;
            isFiring = false;
        }
        else
        {
            arm.GetComponent<SpriteRenderer>().enabled = true;
        }
        arm.GetComponent<SpriteRenderer>().sprite = gunSprites[(int)fireMode];
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(transform.position.x, transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        if ((lookAngle > 90 || lookAngle < -90))
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
        // if (Input.GetMouseButtonDown(0) && !treeBehavior.inTrunk)
        // {
        //     isFiring = !isFiring;
        // }

        if (Input.GetAxisRaw("Fire1") > 0 && Time.time > nextFire && !treeBehavior.inTrunk && !pauseShooting)
        {
            Fire();
        }

        // Switch Weapons
        if (Input.GetKeyDown(KeyCode.Alpha1))
            fireMode = FireMode.SEMI;

        if (Input.GetKeyDown(KeyCode.Alpha2) && ownedGuns[0])
            fireMode = FireMode.SPREAD;
        
        if (Input.GetKeyDown(KeyCode.Alpha3) && ownedGuns[1])
            fireMode = FireMode.BURST;

        if (Input.GetKeyDown(KeyCode.Alpha4) && ownedGuns[2])
            fireMode = FireMode.AUTO;
        
        if (Input.GetKeyDown(KeyCode.Alpha5) && ownedGuns[3])
            fireMode = FireMode.RPG;

        if (Input.GetKeyDown(KeyCode.L))
            fireMode = FireMode.BRR;
    }
}
