using UnityEngine;

namespace SoulHunter.UI
{
    public class MenuUI : SceneController
    {
        void Start()
        {
            UIManager.Instance.MenuCanvas = gameObject;
            Cursor.visible = true;
        }
    }
}