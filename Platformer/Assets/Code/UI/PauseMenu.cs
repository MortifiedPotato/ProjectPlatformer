using UnityEngine.UI;
using UnityEngine;

using SoulHunter.Input;
using SoulHunter.Player;
using SoulHunter.Dialogue;

namespace SoulHunter.UI
{
    public class PauseMenu : MonoBehaviour, Input.ITogglePause
    {
        public GameObject PauseMenuUI;
        [SerializeField] Button continueButton;

        private void Start()
        {
            InputController.Instance.i_TogglePause = this;

            GameManager.GameIsPaused = false;
            HandlePause();
        }

        public void TogglePause()
        {
            GameManager.GameIsPaused = !GameManager.GameIsPaused;
            HandlePause();
        }

        void HandlePause()
        {
            if (!GameManager.GameIsPaused)
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
            if (!GameManager.inDialogue)
            {
                PlayerBase.isPaused = false;
            }

            PauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameManager.GameIsPaused = false;
        }

        void Pause()
        {
            PlayerBase.isPaused = true;

            PauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameManager.GameIsPaused = true;

            continueButton.Select();
        }

        public void ChangeScene(int index)
        {
            SceneController.Instance.TransitionScene(index);
        }
    }
}