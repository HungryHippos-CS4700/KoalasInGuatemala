using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private SpawnLocation[] spawnLocations;
    [SerializeField] private GameObject pickUpEffect;
    [SerializeField] private int powerUpTime;
    [SerializeField] private string mode;
    private Shooting shooting;
    public int spawnLocationIndex;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            // Transfer power up control to shooting, does not get destroyed
            shooting.PowerUp(mode, powerUpTime);
            spawnLocations[spawnLocationIndex].isSpawned = false;
            Destroy(Instantiate(pickUpEffect, transform.position, Quaternion.identity), .517f);
            shooting.powerUpSpawned = false;
            Destroy(gameObject);
        }
    }

    void Start()
    {
        spawnLocations = FindObjectOfType<ItemSpawner>().spawnLocations;
        shooting = FindObjectOfType<Shooting>();
    }
}

// public class PowerUp : MonoBehaviour
// {
    // [SerializeField] private SpawnLocation[] spawnLocations;
    // [SerializeField] private float rotationsPerMinute;
    // [SerializeField] private GameObject pickUpEffect;
    // private Shooting shooting;
    // public int spawnLocationIndex;
    // [SerializeField] private Shooting.FireMode mode;
    // [SerializeField] private int powerUpTime;

//     private void OnTriggerEnter2D(Collider2D collider)
//     {
//         if (collider.gameObject.CompareTag("Player"))
//         {
//             // Transfer power up control to shooting, does not get destroyed
//             shooting.PowerUp(mode, powerUpTime);
//             spawnLocations[spawnLocationIndex].isSpawned = false;
//             Destroy(Instantiate(pickUpEffect, transform.position, Quaternion.identity), .517f);
//             shooting.powerUpSpawned = false;
//             Destroy(gameObject);
//         }
//     }

//     // Start is called before the first frame update
//     void Start()
//     {
//         spawnLocations = FindObjectOfType<ItemSpawner>().spawnLocations;
//         shooting = FindObjectOfType<Shooting>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);
//     }
// }
