using UnityEngine;

namespace SoulHunter.Base
{
    public class DataManager : MonoBehaviour // Mort
    {
        // Singleton Instance
        public static DataManager Instance;

        [Header("User")]
        public static string username;
        public static bool loggedIn;
        public static bool createdAccount;

        [Header("Game Statistics")]
        public int soulsCollected;
        public int timesTeleported;
        public int velocityScore;
        public float durationInAir;

        [Header("Input Statistics")]
        public int timesJumped;
        public int timesMissedGrapple;
        public int timesHitGrapple;
        public int timesMissedAttack;
        public int timesHitAttack;

        void Start()
        {
            Instance = this;
        }
    }
}