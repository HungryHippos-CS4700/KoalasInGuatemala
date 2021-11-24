using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Projectile
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject rocketSmoke;
    public int damage;
    private float blastRadius = 2.5f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Projectile"))
        {
            rocketSmoke.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            rocketSmoke.transform.parent = null;
            rocketSmoke.GetComponent<ParticleSystem>().Stop();
            AudioManager.Instance.Play("RPG_Collision");
            // Instantiate particle and destroy afterwards
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 2f);
            if (collider.CompareTag("Enemy"))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                AudioManager.Instance.Play("Enemy_Hit", true);
            }

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
            foreach (Collider2D nearbyEnemy in colliders)
            {
                Enemy enemy = nearbyEnemy.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    AudioManager.Instance.Play("Enemy_Hit", true);
                }
            }
            Destroy(gameObject);
        }
    }
}
