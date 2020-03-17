using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulHunter
{
    public class GameSettings : MonoBehaviour
    {
        public static GameSettings Instance;

        public float soundVolume = 0;

        private void Awake()
        {
            Instance = this;
        }
    }
}