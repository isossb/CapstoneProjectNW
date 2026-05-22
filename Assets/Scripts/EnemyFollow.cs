using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;

    private Transform player;

    void Start()
    {
        // Find the player by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null)
            return;

        // Direction toward player
        Vector2 direction = (player.position - transform.position).normalized;

        // Move toward player
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }
}