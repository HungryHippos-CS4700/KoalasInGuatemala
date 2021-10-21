using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    [SerializeField] protected HealthBar healthBar;
    protected Vector3 offset;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            health -= 1f;
        }
    }

    void Update()
    {
        healthBar.SetHealth(health, maxHealth);
        healthBar.transform.position = transform.position + offset;
        if (health <= 0)
        {
            Object.Destroy(gameObject);
        }
    }
}
