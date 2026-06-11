using System.Collections;
using UnityEngine;

public class PlayerPowerUps : MonoBehaviour
{
    public PlayerController controller;
    public Weapon weapon;
    public PowerUpUI powerUpUI;
    private bool infiniteDashActive = false;

    public void ActivatePowerUp(PowerUp.PowerUpType type, float duration)
    {
        string name = "";

        switch (type)
        {
            case PowerUp.PowerUpType.InfiniteDash:
                StartCoroutine(InfiniteDash(duration));
                name = "Infinite Dash";
                break;

            case PowerUp.PowerUpType.SpeedBoost:
                StartCoroutine(SpeedBoost(duration));
                name = "Speed Boost";
                break;

            case PowerUp.PowerUpType.FireRateBoost:
                StartCoroutine(FireRateBoost(duration));
                name = "Fire Rate Boost";
                break;
        }

        if (powerUpUI != null)
            powerUpUI.Activate(name, duration);
    }

    IEnumerator InfiniteDash(float duration)
    {
        infiniteDashActive = true;

        float originalCooldown = controller.dashCooldown;
        controller.dashCooldown = 0.1f; // almost instant

        yield return new WaitForSeconds(duration);

        controller.dashCooldown = originalCooldown;
        infiniteDashActive = false;
    }

    IEnumerator SpeedBoost(float duration)
    {
        float originalSpeed = controller.moveSpeed;

        controller.moveSpeed *= 1.5f;

        yield return new WaitForSeconds(duration);

        controller.moveSpeed = originalSpeed;
    }

    IEnumerator FireRateBoost(float duration)
    {
        float originalRate = weapon.fireRate;

        weapon.fireRate *= 2f;

        yield return new WaitForSeconds(duration);

        weapon.fireRate = originalRate;
    }
}