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

        private void Start()
        {
            volumeSlider.value = GameSettings.Instance.soundVolume;
            SetVolume(GameSettings.Instance.soundVolume);
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("Volume", volume);
            GameSettings.Instance.soundVolume = volume;

            float displayValue = volume / 80;
            volumePercentage.text = Mathf.Round((displayValue + 1) * 100).ToString();
        }
    }
}