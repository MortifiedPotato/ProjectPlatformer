using UnityEngine.UI;
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

        public GameObject LoadingScreenCanvas;
        public Slider loadingBar;

        private void Awake()
        {
            Instance = this;
        }
    }
}