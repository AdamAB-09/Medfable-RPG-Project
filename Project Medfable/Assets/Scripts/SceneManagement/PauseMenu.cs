using Medfable.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Medfable.SceneManagement
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject pauseMenuUI;
        private GameObject player;
        public static bool isGamePaused = false;

        // Search for the player at the start of the game and whenever the scene changes
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            SceneManager.activeSceneChanged += FindPlayer;
        }

        // Instantiate the player into a variable whenver scene changes
        private void FindPlayer(Scene arg0, Scene arg1)
        {
            player = GameObject.FindWithTag("Player");
        }

        // Checks whether the player pressed ESC and the pause menu is already showing or not
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isGamePaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }

        // Enables the pause menu and disables movement within the game
        public void PauseGame()
        {
            pauseMenuUI.SetActive(true);
            player.GetComponent<PlayerController>().enabled = false;
            Time.timeScale = 0f;
            isGamePaused = true;
        }

        // Hides the pause menu and enables movement within the game
        public void ResumeGame()
        {
            pauseMenuUI.SetActive(false);
            player.GetComponent<PlayerController>().enabled = true;
            Time.timeScale = 1f;
            isGamePaused = false;
        }

        // Quits the game when the button is pressed
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
