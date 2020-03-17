using UnityEngine;

namespace SoulHunter.UI
{
    public class GameUI : MonoBehaviour
    {
        private void Start()
        {
            UIManager.Instance.GameCanvas = gameObject;
            Cursor.visible = true;
        }
    }
}