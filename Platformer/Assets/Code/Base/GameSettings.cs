using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulHunter
{
    public class GameSettings : MonoBehaviour
    {
        public static GameSettings Instance;

        public float soundVolume = 10;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            soundVolume = 10;
        }
    }
}