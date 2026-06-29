using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private bool paused = false;

    public static bool IsPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        paused = !paused;

        IsPaused = paused;

        pauseMenu.SetActive(paused);

        Time.timeScale = paused ? 0f : 1f;
    }

    public void Resume()
    {
        paused = false;

        IsPaused = false;

        pauseMenu.SetActive(false);

        Time.timeScale = 1f;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // IMPORTANT: reset time before switching scenes

        SceneManager.LoadScene("Title");
    }
}