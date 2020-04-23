using SoulHunter.Base;
using UnityEngine;

namespace SoulHunter.UI
{
    public class MenuUI : MonoBehaviour // Mort
    {
        [SerializeField] GameObject feedback;

        void Start()
        {
            // Sets reference inside UIManager
            CanvasHandler.Instance.MenuCanvas = gameObject;
            // Shows cursor
            Cursor.visible = true;
        }

        /// <summary>
        /// Change scene function for buttons
        /// </summary>
        /// <param name="index"></param>
        public void ChangeScene(int index)
        {
            if (DataManager.loggedIn)
            {
                SceneController.Instance.TransitionScene(index);
            }
            else
            {
                feedback.SetActive(true);
            }
        }

        /// <summary>
        /// Quit game function for buttons
        /// </summary>
        public void QuitGame()
        {
            SceneController.Instance.QuitGame();
        }
    }
}