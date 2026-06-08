using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject enemyPrefab;

    [Header("Spawn Timing")]
    public float spawnRate = 2f;

    [Header("Spawn Distance")]
    public float minimumSpawnDistance = 10f;
    public float maximumSpawnDistance = 15f;

    [Header("Spawn Validation")]
    public LayerMask obstacleLayer;
    public int maxSpawnAttempts = 20;

    private Transform player;
    private float nextSpawnTime;
    private Vector2 enemySize;

    void Start()
    {
        GameObject playerObj =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;

        // Automatically get enemy collider size
        BoxCollider2D enemyCollider =
            enemyPrefab.GetComponent<BoxCollider2D>();

        if (enemyCollider != null)
        {
            enemySize = enemyCollider.size;
        }
        else
        {
            // Fallback size if no BoxCollider2D found
            enemySize = Vector2.one;
        }
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
        if (player == null)
            return;

        for (int i = 0; i < maxSpawnAttempts; i++)
        {
            Vector2 randomDirection =
                Random.insideUnitCircle.normalized;

            float distance = Random.Range(
                minimumSpawnDistance,
                maximumSpawnDistance
            );

            Vector2 spawnPosition =
                (Vector2)player.position +
                randomDirection * distance;

            Collider2D hit = Physics2D.OverlapBox(
                spawnPosition,
                enemySize,
                0f,
                obstacleLayer
            );

            if (hit == null)
            {
                Instantiate(
                    enemyPrefab,
                    spawnPosition,
                    Quaternion.identity
                );

                return;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (player == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            player.position,
            minimumSpawnDistance
        );

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(
            player.position,
            maximumSpawnDistance
        );
    }
}