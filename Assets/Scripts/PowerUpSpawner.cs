using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs;

    public float spawnInterval = 12f;

    public float spawnRadius = 20f;

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnPowerUp();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnPowerUp()
    {
        Vector2 randomPoint = (Vector2)transform.position +
                              Random.insideUnitCircle * spawnRadius;

        RaycastHit2D hit = Physics2D.Raycast(randomPoint, Vector2.down, 2f);

        if (hit.collider != null && hit.collider.CompareTag("White"))
        {
            GameObject prefab =
                powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];

            Instantiate(prefab, hit.point, Quaternion.identity);
        }
        else
        {
            // retry if invalid
            SpawnPowerUp();
        }
    }
}