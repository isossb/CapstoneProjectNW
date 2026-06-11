using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType
    {
        InfiniteDash,
        SpeedBoost,
        FireRateBoost
    }

    [Header("Assigned On Spawn")]
    public PowerUpType type;

    [Header("Duration")]
    public float duration = 10f;

    private void Start()
    {
        // Assign random type when spawned
        type = (PowerUpType)Random.Range(0, 3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPowerUps player = collision.GetComponent<PlayerPowerUps>();

        if (player != null)
        {
            player.ActivatePowerUp(type, duration);
            Destroy(gameObject); // remove pickup after use
        }
    }
}