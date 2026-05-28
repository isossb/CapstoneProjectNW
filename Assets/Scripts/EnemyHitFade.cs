using System.Collections;
using UnityEngine;

public class EnemyHitFade : MonoBehaviour
{
    private SpriteRenderer sr;
    private Rigidbody2D rb;

    public float fadeDuration = 0.5f;
    public float knockbackForce = 5f;

    private bool isDying = false;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDying) return;

        if (collision.gameObject.CompareTag("Bullet"))
        {
            isDying = true;

            // Knockback
            Vector2 knockDir =
                (transform.position - collision.transform.position).normalized;

            rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);

            StartCoroutine(FadeThenDestroy());
        }
    }

    IEnumerator FadeThenDestroy()
    {
        float t = 0f;

        Color startColor = sr.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);

            sr.color = new Color(
                startColor.r,
                startColor.g,
                startColor.b,
                alpha
            );

            yield return null;
        }

        // FINAL CLEANUP
        Destroy(gameObject);
    }
}