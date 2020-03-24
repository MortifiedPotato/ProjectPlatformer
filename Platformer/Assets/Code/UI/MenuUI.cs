using UnityEngine;

namespace SoulHunter.UI
{
    public class MenuUI : MonoBehaviour
    {
        void Start()
        {
            UIManager.Instance.MenuCanvas = gameObject;
            Cursor.visible = true;
        }

        public void ChangeScene(int index)
        {
            SceneController.Instance.ChangeScene(index);
        }

        public void QuitGame()
        {
            SceneController.Instance.QuitGame();
        }
    }
}