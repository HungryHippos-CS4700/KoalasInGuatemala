using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject fragment;
    [SerializeField] private AudioManager audioManager;
    public float speed;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        Object.Destroy(gameObject, 0.8f);
    }

    private void CreateFragments()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject fragmentClone = Instantiate(fragment, gameObject.transform.position, Quaternion.Euler(0f, 0f, 0f));
            fragmentClone.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            Object.Destroy(fragmentClone, 0.25f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!collider.CompareTag("Bullet"))
        {
            CreateFragments();
            if (collider.CompareTag("Enemy"))
            {
                audioManager.Play("Enemy_Hit", true);
            }
            else
            {
                audioManager.Play("Bullet_Collision");
            }
            Object.Destroy(gameObject);
        }

    }

    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
    }
}
