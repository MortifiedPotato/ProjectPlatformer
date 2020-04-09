using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Enemy;

namespace SoulHunter
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public static bool GameIsPaused;
        public static bool interacting;
        public static bool initiatedDialogue;

        public List<EnemyBase> Enemies = new List<EnemyBase>();

        private void Awake()
        {
            Instance = this;
            name = "Managers";
            DontDestroyOnLoad(this);
        }

        public void EnemyListRegistry(EnemyBase enemy)
        {
            if (!Enemies.Contains(enemy))
            {
                Enemies.Add(enemy);
            }
            else
            {
                Enemies.Remove(enemy);
                if (Enemies.Count == 0)
                {
                    SceneController.Instance.TransitionScene(0);
                }
            }
        }
    }
}