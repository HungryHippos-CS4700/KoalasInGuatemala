using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public SpawnLocation[] spawnLocations;
    [SerializeField] LeafCoin leafCoin;
    [SerializeField] private PowerUp[] powerUps;
    [SerializeField] private float spawnRate;
    private float nextSpawn;
    [SerializeField] private int pointsNeededForPowerup;
    [SerializeField] private Shooting shooting;

    SpawnLocation GetSpawnLocation(bool spawnPowerup)
    {
        List<SpawnLocation> availableLocations = new List<SpawnLocation>();
        foreach (SpawnLocation location in spawnLocations)
        {
            if (!location.isSpawned)
            {
                availableLocations.Add(location);
            }
        }
        
        // Limit the number of spawned things
        if (availableLocations.Count <= 7 && !spawnPowerup) {
            return null;
        }

        // if (availableLocations.Count != 0)
        // {
            int index = Random.Range(0, availableLocations.Count);
            return availableLocations[index];
        // }
        // else
        // {
        //     return null;
        // }
    }

    void SpawnCoin()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;

            SpawnLocation location = GetSpawnLocation(false);
            if (location != null)
            {
                LeafCoin leafCoinClone;
                leafCoinClone = Instantiate(leafCoin, location.spawnPoint, Quaternion.identity);
                leafCoinClone.spawnLocationIndex = location.index;
                location.isSpawned = true;
            }
            // else
            // {
            //     print("No spawn locations left");
            // }
        }
    }

    void SpawnPowerup()
    {
        if (Score.scoreValue >= pointsNeededForPowerup && !shooting.hasPowerUp && !shooting.powerUpSpawned)
        {
            pointsNeededForPowerup += 1000;

            SpawnLocation location = GetSpawnLocation(true);
            if (location != null)
            {
                PowerUp powerUpClone = powerUps[Random.Range(0, powerUps.Length)];
                Instantiate(powerUpClone, location.spawnPoint, Quaternion.identity);
                shooting.powerUpSpawned = true;
                powerUpClone.spawnLocationIndex = location.index;
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