using UnityEngine;

namespace SoulHunter.UI
{
    public class UIManager : MonoBehaviour // Mort
    {
        // Singleton instance
        public static UIManager Instance;

        // Pause state
        public static bool GameIsPaused;

        // Menu UI variable
        public GameObject MenuCanvas { get; set; }

        // Game UI variable
        public GameObject GameCanvas { get; set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}