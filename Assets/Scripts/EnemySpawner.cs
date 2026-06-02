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
        GameObject playerObj =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (GameManager.instance != null &&
            GameManager.instance.isGameOver)
            return;

        if (player == null)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        if (player == null) return;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        Vector2 spawnPosition =
            (Vector2)player.position + randomDirection * spawnDistance;

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}