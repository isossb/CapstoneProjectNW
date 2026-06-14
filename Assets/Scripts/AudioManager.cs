using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private float masterVolume = 1f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);

            masterVolume =
                PlayerPrefs.GetFloat("MasterVolume", 1f);

            AudioListener.volume = masterVolume;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;

        AudioListener.volume = volume;

        PlayerPrefs.SetFloat(
            "MasterVolume",
            volume
        );

        PlayerPrefs.Save();
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }
}