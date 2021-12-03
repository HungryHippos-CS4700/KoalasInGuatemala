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
            Destroy(Instantiate(pickUpEffect, transform.position, Quaternion.identity), .517f);
            DestoryPowerUp();
        }
    }

    void DestoryPowerUp()
    {
        spawnLocations[spawnLocationIndex].isSpawned = false;
        shooting.powerUpSpawned = false;
        Destroy(gameObject);
    }

    void Start()
    {
        spawnLocations = FindObjectOfType<ItemSpawner>().spawnLocations;
        shooting = FindObjectOfType<Shooting>();
    }

    void Update()
    {
        if (WaveSpawner.state == WaveSpawner.SpawnState.COUNTING)
        {
            DestoryPowerUp();
            // print("wave over");
        }
    }
}
