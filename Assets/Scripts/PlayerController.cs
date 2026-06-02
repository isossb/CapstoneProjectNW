using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    [Header("Combat")]
    public Weapon weapon;

    [Header("Dash")]
    public float dashSpeed = 15f;
    public float dashTime = 0.15f;
    public float dashCooldown = 4f;

    [Header("UI")]
    public TextMeshProUGUI dashText;

    [Header("Visual Effects")]
    public TrailRenderer dashTrail;

    // Public so other scripts can check it
    [HideInInspector]
    public bool isInvincible = false;

    private Vector2 moveDirection;
    private Vector2 mousePosition;
    private Vector2 dashDirection;

    private bool isDashing = false;
    private bool canDash = true;

    private float dashCooldownTimer;

    void Update()
    {
        // Movement input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        // Mouse position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Shooting (disabled during dash)
        if (!isDashing && Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        // Dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !isDashing)
        {
            dashDirection = moveDirection;

            // If standing still, dash toward mouse
            if (dashDirection == Vector2.zero)
            {
                dashDirection = (mousePosition - rb.position).normalized;
            }

            StartCoroutine(Dash());
        }

        UpdateDashUI();
    }

    void FixedUpdate()
    {
        // Normal movement disabled during dash
        if (!isDashing)
        {
            rb.velocity = moveDirection * moveSpeed;
        }

        // Aim toward mouse
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    IEnumerator Dash()
    {
        isDashing = true;
        isInvincible = true;
        canDash = false;

        // Enable dash trail
        if (dashTrail != null)
        {
            dashTrail.Clear();
            dashTrail.enabled = true;
        }

        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            rb.velocity = dashDirection.normalized * dashSpeed;
            yield return null;
        }

        // Stop dash movement
        rb.velocity = Vector2.zero;

        isDashing = false;
        isInvincible = false;

        // Allow trail to fade naturally
        if (dashTrail != null)
        {
            StartCoroutine(FadeTrailOff());
        }

        // Cooldown timer
        dashCooldownTimer = dashCooldown;

        while (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
            yield return null;
        }

        canDash = true;
    }

    IEnumerator FadeTrailOff()
    {
        yield return new WaitForSeconds(dashTrail.time);

        if (dashTrail != null)
        {
            dashTrail.enabled = false;
        }
    }

    void UpdateDashUI()
    {
        if (dashText == null)
            return;

        if (canDash)
        {
            dashText.text = "Dash Ready";
            dashText.color = Color.green;
        }
        else
        {
            dashText.text = "Dash: " + dashCooldownTimer.ToString("0.0");
            dashText.color = Color.red;
        }
    }
}