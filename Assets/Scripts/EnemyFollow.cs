using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;

    [Header("Obstacle Avoidance")]
    public float obstacleDetectionDistance = 1f;
    public LayerMask obstacleLayer;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        GameObject playerObj =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
            player = playerObj.transform;

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (player == null)
            return;

        // Direction toward player
        Vector2 direction =
            ((Vector2)player.position - rb.position).normalized;

        // Check for obstacle in front
        RaycastHit2D hit = Physics2D.Raycast(
            rb.position,
            direction,
            obstacleDetectionDistance,
            obstacleLayer
        );

        // If obstacle found, steer around it
        if (hit.collider != null)
        {
            Vector2 leftDirection =
                Vector2.Perpendicular(direction);

            Vector2 rightDirection =
                -leftDirection;

            bool leftBlocked = Physics2D.Raycast(
                rb.position,
                leftDirection,
                obstacleDetectionDistance,
                obstacleLayer
            );

            bool rightBlocked = Physics2D.Raycast(
                rb.position,
                rightDirection,
                obstacleDetectionDistance,
                obstacleLayer
            );

            if (!leftBlocked)
            {
                direction = leftDirection;
            }
            else if (!rightBlocked)
            {
                direction = rightDirection;
            }
            else
            {
                // Both sides blocked
                direction = Vector2.zero;
            }
        }

        rb.MovePosition(
            rb.position +
            direction * moveSpeed * Time.fixedDeltaTime
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
            Destroy(collision.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (rb == null || player == null)
            return;

        Gizmos.color = Color.red;

        Vector2 direction =
            ((Vector2)player.position - rb.position).normalized;

        Gizmos.DrawLine(
            rb.position,
            rb.position + direction * obstacleDetectionDistance
        );
    }
}