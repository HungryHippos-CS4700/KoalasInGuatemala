using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    public Transform firePoint;
    public float bulletSpeed = 50;
    public float fireRate;
    public bool canFire;

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

    void Update()
    {
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(player.transform.position.x, player.transform.position.y);
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        firePoint.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetAxis("Fire1") == 1 && canFire)
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
