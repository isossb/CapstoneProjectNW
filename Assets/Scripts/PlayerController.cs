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

    [Header("Dash Audio")]
    public AudioSource audioSource;
    public AudioClip dashSound;

    [Range(0f, 1f)]
    public float dashVolume = 1f;

    [Header("UI")]
    public TextMeshProUGUI dashText;

    [Header("Visual Effects")]
    public TrailRenderer dashTrail;

    [HideInInspector]
    public bool isInvincible = false;

    private Vector2 moveDirection;
    private Vector2 dashDirection;

    private bool isDashing = false;
    private bool canDash = true;

    private float dashCooldownTimer;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (PauseManager.IsPaused)
            return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection =
            new Vector2(moveX, moveY).normalized;

        if (!isDashing &&
            Input.GetMouseButtonDown(0))
        {
            weapon.Fire();
        }

        if (
            Input.GetKeyDown(KeyCode.LeftShift)
            &&
            canDash
            &&
            !isDashing
        )
        {
            Vector2 mouseWorld =
                cam.ScreenToWorldPoint(
                    Input.mousePosition
                );

            dashDirection =
                moveDirection;

            if (dashDirection == Vector2.zero)
            {
                dashDirection =
                    (
                        mouseWorld -
                        rb.position
                    ).normalized;
            }

            StartCoroutine(Dash());
        }

        UpdateDashUI();
    }

    void FixedUpdate()
    {
        if (!isDashing)
        {
            rb.velocity =
                moveDirection *
                moveSpeed;
        }

        Vector2 mouseWorld =
            cam.ScreenToWorldPoint(
                Input.mousePosition
            );

        Vector2 aimDirection =
            mouseWorld -
            rb.position;

        float aimAngle =
            Mathf.Atan2(
                aimDirection.y,
                aimDirection.x
            )
            *
            Mathf.Rad2Deg
            -
            90f;

        rb.MoveRotation(
            aimAngle
        );
    }

    IEnumerator Dash()
    {
        isDashing = true;
        isInvincible = true;
        canDash = false;

        // PLAY DASH SOUND
        if (
            audioSource != null &&
            dashSound != null
        )
        {
            audioSource.pitch =
                Random.Range(
                    0.97f,
                    1.03f
                );

            audioSource.PlayOneShot(
                dashSound,
                dashVolume
            );

            audioSource.pitch = 1f;
        }

        if (dashTrail != null)
        {
            dashTrail.Clear();
            dashTrail.enabled = true;
        }

        float startTime =
            Time.time;

        while (
            Time.time <
            startTime + dashTime
        )
        {
            rb.velocity =
                dashDirection.normalized *
                dashSpeed;

            yield return null;
        }

        rb.velocity =
            Vector2.zero;

        isDashing = false;
        isInvincible = false;

        if (dashTrail != null)
        {
            StartCoroutine(
                FadeTrailOff()
            );
        }

        dashCooldownTimer =
            dashCooldown;

        while (
            dashCooldownTimer >
            0
        )
        {
            dashCooldownTimer -=
                Time.deltaTime;

            yield return null;
        }

        canDash = true;
    }

    IEnumerator FadeTrailOff()
    {
        yield return new WaitForSeconds(
            dashTrail.time
        );

        if (dashTrail != null)
        {
            dashTrail.enabled =
                false;
        }
    }

    void UpdateDashUI()
    {
        if (dashText == null)
            return;

        if (canDash)
        {
            dashText.text =
                "Dash Ready";

            dashText.color =
                Color.green;
        }
        else
        {
            dashText.text =
                "Dash: "
                +
                dashCooldownTimer
                .ToString("0.0");

            dashText.color =
                Color.red;
        }
    }
}