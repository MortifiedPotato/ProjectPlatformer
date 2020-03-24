using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace SoulHunter.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;

        [SerializeField] TextMeshProUGUI volumePercentage;
        [SerializeField] Slider volumeSlider;

        float SavedValue;

        private void OnEnable()
        {
            CalculateValues();

            volumeSlider.value = SavedValue;
            SetVolume(SavedValue);
        }
        void CalculateValues()
        {
            SavedValue = GameSettings.Instance.soundVolume / 10;
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
            GameSettings.Instance.soundVolume = volume * 10;

            float displayValue = Mathf.Round(GameSettings.Instance.soundVolume);
            volumePercentage.text = displayValue.ToString();
        }
    }
}