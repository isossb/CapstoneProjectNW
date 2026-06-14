using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderUI : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value =
            AudioManager.Instance.GetMasterVolume();

        slider.onValueChanged.AddListener(
            ChangeVolume
        );
    }

    void ChangeVolume(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }
}