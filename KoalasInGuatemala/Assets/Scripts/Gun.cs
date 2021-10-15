using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject player;
    public Transform arm;
    public Transform armPivot;
    public GameObject bullet;
    public Transform firePoint;
    public float bulletSpeed = 50;
    public float fireRate;
    public bool canFire;
    public bool isFiring;

    Vector2 lookDirection;
    float lookAngle;
    

    private IEnumerator FireDebounce()
    {
        canFire = false;
        // process pre-yield
        yield return new WaitForSeconds(fireRate);
        // process post-yield
        canFire = true;
    }

    void Start()
    {
        isFiring = false;
    }

    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y);
        //lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if(lookAngle > 90 || lookAngle < -90)
        {
            player.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            //player.GetComponent<SpriteRenderer>().flipX = true;
            arm.rotation = Quaternion.Euler(0f, 0f, lookAngle);
        }
        else
        {
            player.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            arm.rotation = Quaternion.Euler(180f, 0f, -lookAngle);
            //player.GetComponent<SpriteRenderer>().flipX = false;
        }

        firePoint.rotation = Quaternion.Euler(0f, 0f, lookAngle);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked mousebutton");
            isFiring = !isFiring;
        }
        if (isFiring && canFire)
        {
            GameObject bulletClone = Instantiate(bullet);
            bulletClone.transform.position = firePoint.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

            bulletClone.GetComponent<Rigidbody2D>().velocity = firePoint.right * bulletSpeed;
            Object.Destroy(bulletClone, 0.3f);
            StartCoroutine(FireDebounce());
        }
    }
}