using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private DamageText damageText;
    [SerializeField] private float damageTextOffset;
    private Rigidbody2D rb;
    public bool isGroundEnemy;

    // private void DisableEnemyCollisionWithPlayer()
    // {
    //     PlayerController player = FindObjectOfType<PlayerController>();
    //     Collider2D enemyCollider = GetComponent<Collider2D>();
    //     BoxCollider2D playerCollider = player.GetComponent<BoxCollider2D>();
    //     Physics2D.IgnoreCollision(enemyCollider, playerCollider);
    // }

    public void TakeDamage(int damage)
    {
        health -= damage;
        DamageText damageTextGUI = Instantiate(damageText,
        transform.position + new Vector3(Random.Range(-1f, 1f), damageTextOffset, 0f),
        Quaternion.Euler(0f, 0f, Random.Range(-20, 20)));
        damageTextGUI.damage = damage;
        healthBar.SetHealth(health, maxHealth);
        Score.UpdateScore(50);
    }
    
    void Start()
    {
        // DisableEnemyCollisionWithPlayer();
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetHealth(health, maxHealth);
    }

    void Update()
    {
        if (health <= 0)
        {
            // add check: if the enemy is an owl
            Score.UpdateScore(500);
            audioManager.Play("Enemy_Death");
            Object.Destroy(gameObject);
        }
    }
}
