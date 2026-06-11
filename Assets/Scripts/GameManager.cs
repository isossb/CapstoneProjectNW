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
    }

    public void RegisterKill()
    {
        totalKills++;
        killsThisRound++;

        if (killsThisRound >= killsRequiredForNextRound)
        {
            AdvanceRound();
        }
    }

    void AdvanceRound()
    {
        currentRound++;

        killsThisRound = 0;

        // Each round requires 2 more kills
        killsRequiredForNextRound += 2;

        UpdateRoundUI();

        Debug.Log("Round " + currentRound);
    }

    void UpdateRoundUI()
    {
        if (roundText != null)
        {
            roundText.text = "Round " + currentRound;
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        if (gameOverUI != null)
            gameOverUI.SetActive(true);

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
        if (isGameOver &&
            Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
}