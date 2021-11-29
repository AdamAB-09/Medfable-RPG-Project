using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Starts a new scene when user presses 'Start' button
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quits the application when user presses 'Quit' button
    public void QuitGame()
    {
        print("Game closing");
        Application.Quit();
    }

}
