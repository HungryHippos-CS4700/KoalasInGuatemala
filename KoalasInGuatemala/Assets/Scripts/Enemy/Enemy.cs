using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private DamageText damageText;
    [SerializeField] private float damageTextOffset;
    private Rigidbody2D rb;
    public bool isGroundEnemy;

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
        //audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetHealth(health, maxHealth);
    }

    void Update()
    {
        if (health <= 0)
        {
            Score.UpdateScore(500);
            AudioManager.Instance.Play("Enemy_Death");
            Object.Destroy(gameObject);
        }
    }
}
