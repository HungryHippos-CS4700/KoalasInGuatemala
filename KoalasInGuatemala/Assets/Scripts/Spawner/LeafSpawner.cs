using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafSpawner : MonoBehaviour
{
    [SerializeField] private GameObject leafCoin;
    [SerializeField] private float spawnRange;
    // [SerializeField] private float rightSpawn;
    [SerializeField] private List<Vector2> spawnPoints;
    Vector2 whereToSpawn;
    [SerializeField] private float spawnRate;
    float nextSpawn = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;

            if (spawnPoints.Count > 0)
            {
                int index = Random.Range(0, spawnPoints.Count);
                whereToSpawn = spawnPoints[index];
                print(whereToSpawn);
                spawnPoints.RemoveAt(index);
                Instantiate(leafCoin, whereToSpawn, Quaternion.identity);
            }
        }
    }
}
