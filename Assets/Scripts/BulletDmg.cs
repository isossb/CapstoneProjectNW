using UnityEngine;

public class BulletDmg : MonoBehaviour
{
    public int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if object hit is an enemy
        EnemyHealth enemy =
            collision.GetComponent<EnemyHealth>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            // Destroy bullet after hit
            Destroy(gameObject);
        }
    }
}