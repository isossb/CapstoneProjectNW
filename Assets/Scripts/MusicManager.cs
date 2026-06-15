using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;

    [Header("Playlist")]
    public AudioClip[] songs;

    [Range(0f, 1f)]
    public float musicVolume = 0.6f;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource =
                GetComponent<AudioSource>();
        }

        PlayRandomSong();
    }

    void PlayRandomSong()
    {
        if (
            songs == null ||
            songs.Length == 0
        )
        {
            Debug.LogWarning(
                "No songs assigned."
            );

            return;
        }

        int randomSong =
            Random.Range(
                0,
                songs.Length
            );

        audioSource.clip =
            songs[randomSong];

        audioSource.volume =
            musicVolume;

        audioSource.loop =
            true;

        audioSource.Play();
    }
}