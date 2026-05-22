using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnRate = 2f;

    public float spawnDistance = 10f;

    private Transform player;

    private float nextSpawnTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();

            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        // Random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // Spawn position around player
        Vector2 spawnPosition =
            (Vector2)player.position + randomDirection * spawnDistance;

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}