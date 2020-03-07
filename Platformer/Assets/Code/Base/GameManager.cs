using UnityEngine;

namespace SoulHunter.Base
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
            name = "Managers";
            DontDestroyOnLoad(this);
        }
    }
}