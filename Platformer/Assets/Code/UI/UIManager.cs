using UnityEngine;

namespace SoulHunter.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        // Pause state
        public static bool GameIsPaused;
        public GameObject MenuCanvas { get; set; }
        public GameObject GameCanvas { get; set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}