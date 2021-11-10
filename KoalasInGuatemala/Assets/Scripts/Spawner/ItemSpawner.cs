using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public SpawnLocation[] spawnLocations;
    [SerializeField] LeafCoin leafCoin;
    [SerializeField] RocketPowerup rocket;
    [SerializeField] private float spawnRate;
    private float nextSpawn;
    [SerializeField] private int pointsNeededForPowerup;

    SpawnLocation GetSpawnLocation()
    {
        List<SpawnLocation> availableLocations = new List<SpawnLocation>();
        foreach (SpawnLocation location in spawnLocations)
        {
            if (!location.isSpawned)
            {
                availableLocations.Add(location);
            }
        }
        if (availableLocations.Count != 0)
        {
            int index = Random.Range(0, availableLocations.Count);
            return availableLocations[index];
        }
        else
        {
            return null;
        }
    }

    void SpawnCoin()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;

            SpawnLocation location = GetSpawnLocation();
            if (location != null)
            {
                LeafCoin leafCoinClone;
                leafCoinClone = Instantiate(leafCoin, location.spawnPoint, Quaternion.identity);
                leafCoinClone.spawnLocationIndex = location.index;
                location.isSpawned = true;
            }
            else
            {
                print("No spawn locations left");
            }
        }
    }

    void SpawnPowerup()
    {
        if (Score.scoreValue >= pointsNeededForPowerup)
        {
            pointsNeededForPowerup += pointsNeededForPowerup;

            SpawnLocation location = GetSpawnLocation();
            if (location != null)
            {
                // powerup instead of leafcoin
                RocketPowerup rocketClone;
                rocketClone = Instantiate(rocket, location.spawnPoint, Quaternion.identity);
                rocketClone.spawnLocationIndex = location.index;
                location.isSpawned = true;
            }
            else
            {
                print("No spawn locations left");
            }
        }
    }

    void Start()
    {
    }
    void Update()
    {
        SpawnCoin();
        SpawnPowerup();
    }
}