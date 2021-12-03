using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private SpawnLocation[] spawnLocations;
    [SerializeField] private GameObject pickUpEffect;
    [SerializeField] private int regenAmount;
    public int spawnLocationIndex;
    [SerializeField] private float rotationsPerMinute;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            TreeHealth.treeHealth += regenAmount;
            spawnLocations[spawnLocationIndex].isSpawned = false;
            ItemSpawner.heartSpawned = false;
            Destroy(Instantiate(pickUpEffect, transform.position, Quaternion.identity), .283f);
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
