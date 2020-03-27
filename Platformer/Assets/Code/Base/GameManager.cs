using UnityEngine;

namespace SoulHunter
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public static bool GameIsPaused;
        public static bool inDialogue;


        private void Awake()
        {
            Instance = this;
            name = "Managers";
            DontDestroyOnLoad(this);
        }
    }
}