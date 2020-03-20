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

        public void ChangeIt(int index)
        {
            SceneController.Instance.ChangeScene(index);
        }

        public void QuitIt()
        {
            SceneController.Instance.QuitGame();
        }
    }
}