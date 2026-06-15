using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;

    public GameObject gameOverUI;

    [Header("Round System")]
    public int currentRound = 1;
    public int totalKills = 0;

    private int killsThisRound = 0;
    private int killsRequiredForNextRound = 24;

    [Header("UI")]
    public TMP_Text roundText;

    [Header("Audio")]
    public AudioSource audioSource;

    public AudioClip killSound;

    [Range(0f, 1f)]
    public float killVolume = 0.8f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateRoundUI();

        if (audioSource == null)
        {
            audioSource =
                GetComponent<AudioSource>();
        }
    }

    public void RegisterKill()
    {
        totalKills++;
        killsThisRound++;

        // PLAY KILL SOUND
        if (
            audioSource != null &&
            killSound != null
        )
        {
            audioSource.PlayOneShot(
                killSound,
                killVolume
            );
        }

        if (killsThisRound >= killsRequiredForNextRound)
        {
            AdvanceRound();
        }
    }

    void AdvanceRound()
    {
        currentRound++;

        killsThisRound = 0;

        killsRequiredForNextRound += 2;

        UpdateRoundUI();

        Debug.Log(
            "Round " +
            currentRound
        );
    }

    void UpdateRoundUI()
    {
        if (roundText != null)
        {
            roundText.text =
                "Round " +
                currentRound;
        }
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        isGameOver = true;

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        Debug.Log("GAME OVER");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    void Update()
    {
        if (
            isGameOver &&
            Input.GetKeyDown(KeyCode.R)
        )
        {
            RestartGame();
        }
    }
}