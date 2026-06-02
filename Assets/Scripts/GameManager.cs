using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameOver = false;

    public GameObject gameOverUI;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // Show UI
        if (gameOverUI != null)
            gameOverUI.SetActive(true);

        Debug.Log("GAME OVER");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }
}