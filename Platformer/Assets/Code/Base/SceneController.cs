using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    public GameObject loadingBar;
    public GameObject loadingArt;
    public TextMeshProUGUI loadingProgress;

    Animator transitionAnimator;
    Slider loadingSlider;

    private void Awake()
    {
        // Set Instance
        Instance = this;
        transitionAnimator = GetComponent<Animator>();
        loadingSlider = loadingBar.GetComponent<Slider>();
    }

    /// <summary>
    /// Returns the build index of the currently active scene
    /// </summary>
    /// <returns></returns>
    public int GetBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    /// <summary>
    /// Returns the total amount of scenes currently in Build Index
    /// </summary>
    /// <returns></returns>
    public int GetTotalBuildIndex()
    {
        return SceneManager.sceneCountInBuildSettings;
    }

    public void TransitionScene(int index)
    {
        transitionAnimator?.SetBool("isLoading", true);
        transitionAnimator?.SetInteger("index", index);
    }

    /// <summary>
    /// Loads the scene specified with an int (Scene Index)
    /// </summary>
    /// <param name="sceneIndex"></param>
    void ChangeScene()
    {
        int index = transitionAnimator.GetInteger("index");
        StartCoroutine(LoadSceneAsync(index));
    }

    public void ResetScene()
    {
        transitionAnimator?.SetBool("isLoading", true);
        transitionAnimator?.SetInteger("index", GetBuildIndex());
    }

    /// <summary>
    /// Ends the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        loadingArt.SetActive(true);
        loadingBar.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingSlider.value = progress;
            loadingProgress.text = $"{(progress * 100).ToString()}%";
            yield return null;
        }

        loadingBar.SetActive(false);

        transitionAnimator?.SetBool("isLoading", false);
    }
}