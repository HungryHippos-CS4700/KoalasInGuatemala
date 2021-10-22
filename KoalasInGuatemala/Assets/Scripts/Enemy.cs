using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Vector3 offset;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }
    
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
            audioManager.Play("Enemy_Death");
            Object.Destroy(gameObject);
        }
    }
}
