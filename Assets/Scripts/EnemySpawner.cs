using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public GameObject enemyPrefab;
    public int maxEnemies = 24;

    [Header("Spawn Settings")]
    public float spawnRate = 2f;
    public float spawnDistance = 10f;

    [Header("Spawn Area Settings")]
    public LayerMask spawnAreaLayer;
    public int maxSpawnAttempts = 20;

    private Transform player;
    private float nextSpawnTime;

    void Start()
    {
        GameObject playerObj =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        // Stop spawning after game over
        if (GameManager.instance != null &&
            GameManager.instance.isGameOver)
        {
            return;
        }

        // Stop if player doesn't exist
        if (player == null)
        {
            return;
        }

        // Enemy cap
        if (GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemies)
        {
            return;
        }

        // Spawn timer
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        if (player == null)
            return;

        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            // Random direction around player
            Vector2 randomDirection =
                Random.insideUnitCircle.normalized;

            // Candidate spawn position
            Vector2 spawnPosition =
                (Vector2)player.position +
                randomDirection * spawnDistance;

            // Check if position is inside a valid White spawn area
            Collider2D spawnArea =
                Physics2D.OverlapPoint(
                    spawnPosition,
                    spawnAreaLayer
                );

            if (spawnArea != null)
            {
                Instantiate(
                    enemyPrefab,
                    spawnPosition,
                    Quaternion.identity
                );

                return;
            }
        }

        // If we get here, no valid spawn location was found
        Debug.Log("No valid spawn area found.");
    }
}