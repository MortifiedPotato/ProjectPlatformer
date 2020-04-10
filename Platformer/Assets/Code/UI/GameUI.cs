using UnityEngine;

namespace SoulHunter.UI
{
    public class GameUI : MonoBehaviour // Mort
    {
        private void Start()
        {
            // Sets reference inside UIManager
            UIManager.Instance.GameCanvas = gameObject;
            // Disables cursor
            Cursor.visible = false;
        }
    }
}