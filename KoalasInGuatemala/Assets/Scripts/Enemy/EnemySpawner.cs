using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject owl;
    [SerializeField] private Vector2[] locations;
    // [SerializeField] private float x;
    [SerializeField] private float spawnRate;
    private float nextSpawn;

    // Start is called before the first frame update
    void SpawnEnemy()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            int index = Random.Range(0, locations.Length);
            Instantiate(owl, locations[index], Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemy();
    }
}
