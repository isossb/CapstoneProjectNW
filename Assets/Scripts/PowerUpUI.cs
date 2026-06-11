using UnityEngine;
using TMPro;

public class PowerUpUI : MonoBehaviour
{
    public TextMeshProUGUI text;

    private float timer = 0f;
    private string currentPowerUp = "";
    private bool active = false;

    void Update()
    {
        if (!active)
            return;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            active = false;
            text.text = "";
            return;
        }

        text.text = currentPowerUp + " : " + timer.ToString("0.0");
    }

    public void Activate(string powerUpName, float duration)
    {
        currentPowerUp = powerUpName;
        timer = duration;
        active = true;
    }
}