using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;

    [Header("Automatic Fire")]
    public float fireRate = 10f; // bullets per second

    [Header("Recoil Visual Effect")]
    public float recoilAmount = 0.2f;
    public float recoilRecoverSpeed = 5f;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private bool isRecoiling = false;

    private float nextFireTime;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Hold left mouse button for automatic fire
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + (1f / fireRate);
        }

        // Gradually return to original size and position
        if (isRecoiling)
        {
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                originalScale,
                Time.deltaTime * recoilRecoverSpeed
            );

            transform.localPosition = Vector3.Lerp(
                transform.localPosition,
                originalPosition,
                Time.deltaTime * recoilRecoverSpeed
            );

            if (Vector3.Distance(transform.localScale, originalScale) < 0.01f)
            {
                transform.localScale = originalScale;
                transform.localPosition = originalPosition;
                isRecoiling = false;
            }
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            bulletRb.AddForce(
                firePoint.up * fireForce,
                ForceMode2D.Impulse
            );
        }

        ApplyRecoil();
    }

    void ApplyRecoil()
    {
        float newYScale = originalScale.y - recoilAmount;

        if (newYScale < 0.1f)
            newYScale = 0.1f;

        transform.localScale = new Vector3(
            originalScale.x,
            newYScale,
            originalScale.z
        );

        float heightDifference = originalScale.y - newYScale;

        transform.localPosition = originalPosition - new Vector3(
            0,
            heightDifference / 2f,
            0
        );

        isRecoiling = true;
    }
}