using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    [SerializeField] private enum SpawnState { SPAWNING, WAITING, COUNTING };

    [SerializeField] private Vector2[] airLocations;
    [SerializeField] private Vector2[] groundLocations;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int[] enemiesLeft;
    // [SerializeField] private int startWave = 1;
    [SerializeField] private int waveCount = 1;
    [SerializeField] private float spawnRate = 1f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float waveCountdown;
    [SerializeField] private SpawnState state = SpawnState.COUNTING;
    private float searchCountdown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        // startWave could be used for saving progress
        // waveCount = startWave;

        enemiesLeft = new int[enemies.Length];
        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave());
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private bool EnemyIsAlive()
    {
        searchCountdown = -Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator SpawnWave()
    {
        state = SpawnState.SPAWNING;

        CreateEnemyCount();

        print("Spawning Wave: " + waveCount);

        while (!AllEnemiesSpawned())
        {
            int index = Random.Range(0, enemiesLeft.Length);
            SpawnEnemy(enemies[index]);
            enemiesLeft[index] -= 1;
            yield return new WaitForSeconds(spawnRate); // change the spawn rate later
        }

        state = SpawnState.WAITING;
        yield break;
    }

    private void SpawnEnemy(GameObject enemy)
    {
        // print("Spawning Enemy: " + enemy.name);
        Enemy enemyProperties = enemy.GetComponent<Enemy>();
        if (!enemyProperties.isGroundEnemy)
        {
            int index = Random.Range(0, airLocations.Length);
            Instantiate(enemy, airLocations[index], Quaternion.identity);
        }
        else
        {
            // ground enemies
            int index = Random.Range(0, groundLocations.Length);
            Instantiate(enemy, groundLocations[index], Quaternion.identity);
        }
    }

    private void WaveCompleted()
    {
        print("Wave " + waveCount + " Completed");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        waveCount++;
    }

    private void CreateEnemyCount()
    {
        for (int i = 0; i < enemies.Length; i++) {
            if (enemies[i].name == "Owl") {
                enemiesLeft[i] = waveCount * 2;
            } else if (enemies[i].name == "Squirrel") {
                enemiesLeft[i] = waveCount;
            }
            // add to this for other enemies
        }
    }

    private bool AllEnemiesSpawned()
    {
        for (int i = 0; i < enemiesLeft.Length; i++)
        {
            if (enemiesLeft[i] != 0)
            {
                return false;
            }
        }

        return true;
    }
}
