using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;

    [Header("Recoil Visual Effect")]
    public float recoilAmount = 0.2f;      // How much the rectangle shrinks
    public float recoilRecoverSpeed = 5f;  // How fast it returns to normal

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private bool isRecoiling = false;

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.localPosition;
    }

    void Update()
    {
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

            // Stop tiny endless lerping
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
        // Spawn bullet
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            firePoint.rotation
        );

        bullet.GetComponent<Rigidbody2D>().AddForce(
            firePoint.up * fireForce,
            ForceMode2D.Impulse
        );

        ApplyRecoil();
    }

    void ApplyRecoil()
    {
        // Shrink from the top while keeping the bottom fixed

        // Reduce height (Y scale)
        float newYScale = originalScale.y - recoilAmount;
        if (newYScale < 0.1f)
            newYScale = 0.1f;

        transform.localScale = new Vector3(
            originalScale.x,
            newYScale,
            originalScale.z
        );

        // Move object upward/downward so bottom stays fixed
        float heightDifference = originalScale.y - newYScale;
        transform.localPosition = originalPosition - new Vector3(
            0,
            heightDifference / 2f,
            0
        );

        isRecoiling = true;
    }
}