using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private GameObject fragments;
    [SerializeField] private GameObject leaves;
    public int damage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Projectile"))
        {
            ParticleSystem fragment = Instantiate(fragments, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            var main = fragment.main;

            switch(collider.tag)
            {
                case "Enemy":
                {
                    main.startColor = Color.red;
                    Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(Random.Range(damage - 2, damage + 2));
                    AudioManager.Instance.Play("Enemy_Hit", true);
                    break;
                }

                case "Trunk":
                case "BranchTile":
                case "Branch":
                {
                    Color color1 = new Color(0.9f, 0.5f, 0.1f);
                    Color color2 = new Color(0.6f, 0.2f, 0.1f);
                    main.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
                    
                    AudioManager.Instance.Play("Bullet_Collision");
                    break;
                }
                
                case "Ground":
                {
                    Color color1 = new Color(0.4f, 0.7f, 0.1f);
                    Color color2 = new Color(0.6f, 0.9f, 0.3f);
                    main.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
                    
                    AudioManager.Instance.Play("Bullet_Collision");
                    break;
                }

                default:
                {
                    AudioManager.Instance.Play("Bullet_Collision");
                    break;
                }
            }
            Destroy(fragment.gameObject, 0.5f);
            Destroy(gameObject);
        }
    }
}