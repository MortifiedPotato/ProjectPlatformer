using UnityEngine;

namespace SoulHunter.UI
{
    public class PauseMenu : SceneController, Input.ITogglePause
    {
        public static bool GameIsPaused;

        public GameObject PauseMenuUI;

        private void Start()
        {
            GameIsPaused = false;
            HandlePause();
        }

        public void TogglePause()
        {
            GameIsPaused = !GameIsPaused;
            HandlePause();
        }

        void HandlePause()
        {
            if (!GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        void Resume()
        {
            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }

        void Pause()
        {
            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }
}