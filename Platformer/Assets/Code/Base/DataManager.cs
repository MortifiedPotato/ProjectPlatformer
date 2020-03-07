using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Enemy;

namespace SoulHunter.Base
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;

        [Header("Game Statistics")]
        public int soulsCollected;
        public int velocityScore;
        public float durationInAir;

        [Header("Input Statistics")]
        public int timesJumped;
        public int timesMissedGrapple;
        public int timesHitGrapple;
        public int timesMissedAttack;
        public int timesHitAttack;

        public List<EnemyScript> AliveEnemies = new List<EnemyScript>();

        void Start()
        {
            Instance = this;
        }

        public void HandleEntityRegistry(EnemyScript entity)
        {
            if (!AliveEnemies.Contains(entity))
            {
                AliveEnemies.Add(entity);
            }
            else
            {
                AliveEnemies.Remove(entity);
            }
        }
    }
}