using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPowerup : MonoBehaviour
{
    [SerializeField] private SpawnLocation[] spawnLocations;
    [SerializeField] private float rotationsPerMinute;
    public int spawnLocationIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Shooting shooting = FindObjectOfType<Shooting>();
            shooting.fireMode = Shooting.FireMode.RPG;
            spawnLocations[spawnLocationIndex].isSpawned = false;
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnLocations = FindObjectOfType<ItemSpawner>().spawnLocations;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);

    }
}
