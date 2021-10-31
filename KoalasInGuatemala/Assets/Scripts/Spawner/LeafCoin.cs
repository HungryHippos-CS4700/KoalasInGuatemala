using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafCoin : MonoBehaviour
{
    [SerializeField] private Score score;
    [SerializeField] private SpawnLocation[] spawnLocations;
    [SerializeField] private float rotationsPerMinute;
    public int spawnLocationIndex;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            score.AddScore(1000);
            Destroy(gameObject);
            spawnLocations[spawnLocationIndex].isSpawned = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnLocations = FindObjectOfType<ItemSpawner>().spawnLocations;
        score = FindObjectOfType<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 6.0f * rotationsPerMinute * Time.deltaTime, 0);

    }
}
