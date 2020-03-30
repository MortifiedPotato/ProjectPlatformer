using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace SoulHunter.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;

        [SerializeField] Toggle fullscreenToggle;
        [SerializeField] TMP_Dropdown graphicsDropdown;
        [SerializeField] TMP_Dropdown resolutionsDropdown;

        [SerializeField] Slider volumeSlider;
        [SerializeField] TextMeshProUGUI volumePercentage;
        [SerializeField] Slider dialogueSlider;
        [SerializeField] TextMeshProUGUI dialoguePercentage;

        private void OnEnable()
        {
            SetUpResolutions();
            UpdateValues();
        }

        void SetUpResolutions()
        {
            resolutionsDropdown.ClearOptions();
            resolutionsDropdown.AddOptions(GameSettings.Instance.options);
            resolutionsDropdown.value = GameSettings.resolution;
            resolutionsDropdown.RefreshShownValue();
        }

        void UpdateValues()
        {
            graphicsDropdown.value = GameSettings.qualityIndex;

            if (GameSettings.isFullscreen)
            {
                fullscreenToggle.isOn = true;
                Application.runInBackground = false;
            }
            else
            {
                fullscreenToggle.isOn = false;
                Application.runInBackground = true;
            }

            volumeSlider.value = GameSettings.soundVolume / 10;
            SetVolume(GameSettings.soundVolume / 10);

            dialogueSlider.value = 1.0f - GameSettings.dialogueSpeed;
        }

        public void ToggleFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                Application.runInBackground = false;
                GameSettings.isFullscreen = true;
            }
            else
            {
                Screen.fullScreenMode = FullScreenMode.Windowed;
                Application.runInBackground = true;
                GameSettings.isFullscreen = false;
            }
        }

        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            GameSettings.qualityIndex = qualityIndex;
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = GameSettings.Instance.resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
            GameSettings.resolution = resolutionIndex;
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
            GameSettings.soundVolume = volume * 10;

            float displayValue = Mathf.Round(GameSettings.soundVolume * 10);
            volumePercentage.text = displayValue.ToString();
        }

        public void SetDialogueSpeed(float speed)
        {
            GameSettings.dialogueSpeed = 1.0f - speed;
            dialoguePercentage.text = Mathf.Round(speed * 10).ToString();
        }
    }
}