using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void Awake()
    {
        // Set Instance
        GameManager.Instance.SceneController = this;
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

    /// <summary>
    /// Loads the scene specified with an int (Scene Index)
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void ChangeScene(int index)
    {
        if (GetBuildIndex() != index)
        {
            StartCoroutine(LoadSceneAsync(index));
        }
    }

    public void ResetScene()
    {
        StartCoroutine(LoadSceneAsync(GetBuildIndex()));
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
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}