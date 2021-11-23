using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafCoin : MonoBehaviour
{
    [SerializeField] private SpawnLocation[] spawnLocations;
    [SerializeField] private float rotationsPerMinute;
    [SerializeField] private GameObject pickUpEffect;
    public int spawnLocationIndex;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Score.UpdateScore(1000);
            spawnLocations[spawnLocationIndex].isSpawned = false;
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
