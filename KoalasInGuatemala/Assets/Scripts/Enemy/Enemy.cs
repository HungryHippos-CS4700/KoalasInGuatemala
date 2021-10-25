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
    [SerializeField] private float speed;
    [SerializeField] private Score score;
    private Rigidbody2D rb;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetHealth(health, maxHealth);
        score = FindObjectOfType<Score>();
    }

    void Update()
    {
        healthBar.transform.position = transform.position + offset;
        if (health <= 0)
        {
            // add check: if the enemy is an owl
            score.addScore(500);
            audioManager.Play("Enemy_Death");
            Object.Destroy(gameObject);
        }
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, 0f, 5f), 0);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.SetHealth(health, maxHealth);
        score.addScore(50);
    }
}
