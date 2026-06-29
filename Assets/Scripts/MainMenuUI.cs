using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject instructionsPanel;
    public GameObject creditsPanel;

    public string gameSceneName = "GameScene";

    void Start()
    {
        ShowMainMenu();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void ShowInstructions()
    {
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        instructionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}