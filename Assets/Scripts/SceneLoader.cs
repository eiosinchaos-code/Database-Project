using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Load the intro scene
    public void LoadIntroScene()
    {
        SceneManager.LoadScene("intro");
    }

    // Load the showscores scene
    public void LoadShowScoresScene()
    {
        SceneManager.LoadScene("showscores");
    }

    // Load the game scene
    public void LoadGameScene()
    {
        SceneManager.LoadScene("game");
    }

    // Load the exit scene
    public void LoadExitScene()
    {
        SceneManager.LoadScene("exit");
    }

    // Exit the application
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}