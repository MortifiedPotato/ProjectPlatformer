using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SceneController : MonoBehaviour // Mort
{
    // Singleton Instance
    public static SceneController Instance;

    LoadingScreen loadingScreen;

    Animator transitionAnimator;

    private void Awake()
    {
        // Set Instance
        Instance = this;
        transitionAnimator = GetComponent<Animator>();
        loadingScreen = GetComponent<LoadingScreen>();
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
        loadingScreen.ShuffleImageAndTip();

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
        loadingScreen.ShuffleImageAndTip();

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

        loadingScreen.artwork.SetActive(true);
        loadingScreen.progressBar.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingScreen.loadingSlider.value = progress;
            loadingScreen.loadingProgressPercentage.text = $"{(progress * 100).ToString()}%";
            yield return null;
        }

        loadingScreen.progressBar.SetActive(false);

        transitionAnimator?.SetBool("isLoading", false);
    }
}