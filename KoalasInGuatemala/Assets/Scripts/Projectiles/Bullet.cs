using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private GameObject fragments;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (!collider.CompareTag("Projectile"))
        {
            // if (collider.CompareTag("Enemy"))
            // {
            //     main.startColor = Color.red;
            //     Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            //     float damage = Random.Range(18, 22);
            //     enemy.TakeDamage(damage);
            //     audioManager.Play("Enemy_Hit", true);
            // }
            // else if (collider.CompareTag("Trunk") || collider.CompareTag("Branch"))
            // {

            //     audioManager.Play("Bullet_Collision");
            // }

            ParticleSystem fragment = Instantiate(fragments, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            var main = fragment.main;

            switch(collider.tag)
            {
                case "Enemy":
                {
                    main.startColor = Color.red;
                    Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                    enemy.TakeDamage(Random.Range(damage - 2f, damage + 2f));
                    audioManager.Play("Enemy_Hit", true);
                    break;
                }

                case "Trunk":
                case "BranchTile":
                case "Branch":
                {
                    Color color1 = new Color(0.9f, 0.5f, 0.1f);
                    Color color2 = new Color(0.6f, 0.2f, 0.1f);
                    main.startColor = new ParticleSystem.MinMaxGradient(color1, color2);
                    
                    audioManager.Play("Bullet_Collision");
                    break;
                }

                default:
                {
                    audioManager.Play("Bullet_Collision");
                    break;
                }
            }
            Destroy(fragment.gameObject, 0.5f);
            Destroy(gameObject);
        }
    }
}
