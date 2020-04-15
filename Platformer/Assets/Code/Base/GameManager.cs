using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Enemy;

namespace SoulHunter
{
    public class GameManager : MonoBehaviour // Mort
    {
        // Singleton Instance
        public static GameManager Instance;

        // Game Pause State
        public static bool gameIsPaused;

        // Player interact state
        public static bool interacting;

        // Dialogue state
        public static bool initiatedDialogue;

        // List of enemies currently alive
        public List<EnemyBase> Enemies = new List<EnemyBase>();

        private void Awake()
        {
            Instance = this;
            name = "Managers";
            DontDestroyOnLoad(this);

            AudioManager.Initialize();
        }

        /// <summary>
        /// Adds or removes enemies to/from the list of currently alive enemies
        /// </summary>
        /// <param name="enemy"></param>
        public void EnemyListRegistry(EnemyBase enemy)
        {
            if (!Enemies.Contains(enemy))
            {
                Enemies.Add(enemy);
            }
            else
            {
                Enemies.Remove(enemy);
            }
        }
    }
}