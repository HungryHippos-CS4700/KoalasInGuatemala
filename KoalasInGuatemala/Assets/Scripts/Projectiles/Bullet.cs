using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    [SerializeField] private GameObject fragments;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ParticleSystem fragment = Instantiate(fragments, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
        var main = fragment.main;
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

            switch(collider.tag)
            {
                case "Enemy":
                {
                    main.startColor = Color.red;
                    Enemy enemy = collider.gameObject.GetComponent<Enemy>();
                    float damage = Random.Range(18, 22);
                    enemy.TakeDamage(damage);
                    audioManager.Play("Enemy_Hit", true);
                    break;
                }

                case "Trunk":
                case "BranchTile":
                {
                    main.startColor = new Color(Random.Range(0.8f, 0.9f), Random.Range(0.4f, 0.5f), Random.Range(0f, 0.1f));
                    audioManager.Play("Bullet_Collision");
                    break;
                }

                default:
                {
                    audioManager.Play("Bullet_Collision");
                    break;
                }
            }
            Destroy(fragment.gameObject, 1f);
            Destroy(gameObject);
        }
    }
}
