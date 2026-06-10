using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyKeyToStart : MonoBehaviour
{
    public string nextSceneName = "MainGame";

    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}