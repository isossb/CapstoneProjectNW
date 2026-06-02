using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;

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

        Vector2 direction =
            ((Vector2)player.position - rb.position).normalized;

        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.GameOver();
            Destroy(collision.gameObject);
        }
    }
}