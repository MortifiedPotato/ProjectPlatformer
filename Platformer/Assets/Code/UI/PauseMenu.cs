using UnityEngine;

using SoulHunter.Input;
using SoulHunter.Dialogue;

namespace SoulHunter.UI
{
    public class PauseMenu : MonoBehaviour, Input.ITogglePause
    {
        public static bool GameIsPaused;

        public GameObject PauseMenuUI;

        private void Start()
        {
            InputController.Instance.i_TogglePause = this;

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
            DialogueManager.Instance.PauseCheck();
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

        public void ChangeScene(int index)
        {
            SceneController.Instance.TransitionScene(index);
        }
    }
}