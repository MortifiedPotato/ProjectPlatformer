using UnityEngine;

namespace SoulHunter.UI
{
    public class MenuUI : MonoBehaviour
    {
        void Start()
        {
            UIManager.Instance.MenuCanvas = gameObject;
            Cursor.visible = false;
        }

        public void ChangeScene(int index)
        {
            SceneController.Instance.TransitionScene(index);
        }

        public void QuitGame()
        {
            SceneController.Instance.QuitGame();
        }
    }
}