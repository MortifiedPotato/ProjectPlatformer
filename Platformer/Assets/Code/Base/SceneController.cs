using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;

using SoulHunter.UI;

namespace SoulHunter.Base
{
    public class SceneController : MonoBehaviour
    {
        public static SceneController Instance { get; private set; }

        private void Awake()
        {
            // Set Instance
            Instance = this;
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
            AsyncOperation operation = SceneManager.LoadSceneAsync(index);

            //UIManager.Instance.LoadingScreenCanvas.SetActive(true);

            while (!operation.isDone)
            {
                //float progress = Mathf.Clamp01(operation.progress / .9f);
                //UIManager.Instance.loadingBar.value = progress;
                yield return null;
            }

            //UIManager.Instance.LoadingScreenCanvas.SetActive(false);
        }
    }
}