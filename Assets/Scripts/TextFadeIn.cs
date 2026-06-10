using TMPro;
using UnityEngine;
using System.Collections;

public class TextFadeIn : MonoBehaviour
{
    public float delayBeforeFade = 2f;
    public float fadeDuration = 2f;

    private TextMeshProUGUI textComponent;
    private Color textColor;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();

        // Start fully transparent
        textColor = textComponent.color;
        textColor.a = 0f;
        textComponent.color = textColor;

        StartCoroutine(DelayedFadeIn());
    }

    IEnumerator DelayedFadeIn()
    {
        // Wait before fading
        yield return new WaitForSeconds(delayBeforeFade);

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;

            float alpha = Mathf.Lerp(
                0f,
                1f,
                timer / fadeDuration
            );

            textColor.a = alpha;
            textComponent.color = textColor;

            yield return null;
        }

        // Ensure fully visible at end
        textColor.a = 1f;
        textComponent.color = textColor;
    }
}