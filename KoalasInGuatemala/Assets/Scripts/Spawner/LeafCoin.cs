using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeafCoin : MonoBehaviour
{
    [SerializeField] private SpawnLocation[] spawnLocations;
    [SerializeField] private GameObject pickUpEffect;
    [SerializeField] private int addToScore;
    public int spawnLocationIndex;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            PlayerController.gemCount++;
            Score.UpdateScore(addToScore);
            spawnLocations[spawnLocationIndex].isSpawned = false;
            print(spawnLocationIndex);
            Destroy(Instantiate(pickUpEffect, transform.position, Quaternion.identity), .283f);
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnLocations = FindObjectOfType<ItemSpawner>().spawnLocations;
    }
}
