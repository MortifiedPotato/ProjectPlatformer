using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public GameObject progressBar;
    public GameObject artwork;
    public TextMeshProUGUI tips;
    public TextMeshProUGUI loadingProgressPercentage;

    [HideInInspector]
    public Slider loadingSlider;


    public LoadingScreenLibrary library;

    private void Awake()
    {
        loadingSlider = progressBar.GetComponent<Slider>();
    }

    /// <summary>
    /// Shuffles loading screen image and tip
    /// </summary>
    public void ShuffleImageAndTip()
    {
        if (library.artwork.Length > 0)
        {
            artwork.GetComponent<Image>().sprite = library.artwork[Random.Range(0, library.artwork.Length)];
        }

        if (library.tips.Length > 0)
        {
            tips.text = library.tips[Random.Range(0, library.tips.Length)].ToString();
        }
    }
}

[System.Serializable]
public class LoadingScreenLibrary
{
    public Sprite[] artwork;
    [TextArea(3, 10)]
    public string[] tips;
}
