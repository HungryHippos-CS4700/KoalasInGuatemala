using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject rocketSmoke;

    private float blastRadius = 2.5f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Projectile"))
        {
            rocketSmoke.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            rocketSmoke.transform.parent = null;
            rocketSmoke.GetComponent<ParticleSystem>().Stop();
            Destroy(rocketSmoke, 2);
            audioManager.Play("RPG_Collision");
            // Instantiate particle and destroy afterwards
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1f);
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                float damage = 80;
                enemy.TakeDamage(damage);
                audioManager.Play("Enemy_Hit", true);
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
            foreach (Collider2D nearbyEnemy in colliders)
            {
                Enemy enemy = nearbyEnemy.GetComponent<Enemy>();
                if (enemy != null)
                {
                    float damage = Random.Range(30, 40);
                    enemy.TakeDamage(damage);
                    audioManager.Play("Enemy_Hit", true);
                }
            }
            Destroy(gameObject);
        }
    }
}
