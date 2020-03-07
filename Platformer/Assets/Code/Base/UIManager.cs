using UnityEngine;

namespace SoulHunter.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        // Pause state
        public static bool isPaused;

        public MenuUI MenuUI { get; set; }
        public GameUI GameUI { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public void PauseGame()
        {
            if (GameUI)
            {
                GameUI.HandlePause();
            }
        }
    }
}