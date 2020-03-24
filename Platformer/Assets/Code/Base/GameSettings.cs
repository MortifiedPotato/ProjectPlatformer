using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoulHunter
{
    public class GameSettings : MonoBehaviour
    {
        public static GameSettings Instance;

        public List<string> options = new List<string>();
        public Resolution[] resolutions;

        public static bool isFullscreen;
        public static int qualityIndex;
        public static int resolution;
        public static float soundVolume;
        public static float dialogueSpeed;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GetResolutions();
            SetDefaultSettings();
        }

        void GetResolutions()
        {
            resolutions = Screen.resolutions;
            System.Array.Reverse(resolutions);

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + "x" + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    resolution = i;
                }
            }
        }

        void SetDefaultSettings()
        {
            isFullscreen = true;
            qualityIndex = 3;
            soundVolume = 10;
            dialogueSpeed = 0f;
        }
    }
}