using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public SpawnLocation[] spawnLocations;
    [SerializeField] LeafCoin leafCoin;
    [SerializeField] Heart heart;
    [SerializeField] private PowerUp[] powerUps;
    [SerializeField] private float spawnRate;
    private float nextSpawn;
    [SerializeField] private int pointsNeededForPowerup;
    [SerializeField] private int nextPointsNeededForPowerup;
    [SerializeField] private int pointsNeededForHeart;
    [SerializeField] private int nextPointsNeededForHeart;
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
                print(location.index);
                print(leafCoinClone.spawnLocationIndex);
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
            pointsNeededForPowerup += nextPointsNeededForPowerup;

            SpawnLocation location = GetSpawnLocation(true);
            if (location != null)
            {
                PowerUp powerUpType = powerUps[Random.Range(0, powerUps.Length)];
                PowerUp powerUpClone = Instantiate(powerUpType, location.spawnPoint, Quaternion.identity);
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

    void SpawnHeart() {
        if (Score.scoreValue >= pointsNeededForHeart) {
            pointsNeededForHeart += nextPointsNeededForHeart;
            
            SpawnLocation location = GetSpawnLocation(false);
            if (location != null)
            {
                Heart heartClone;
                heartClone = Instantiate(heart, location.spawnPoint, Quaternion.identity);
                heartClone.spawnLocationIndex = location.index;
                location.isSpawned = true;
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
        SpawnHeart();
    }
}