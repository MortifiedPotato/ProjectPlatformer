using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Pause state
    public static bool isPaused;

    public MenuUI MenuUI { get; set; }
    public GameUI GameUI { get; set; }

    void Start()
    {
        //Assign singleton reference
        GameManager.Instance.UIManager = this;
    }

    public void PauseGame()
    {
        if (GameUI)
        {
            GameUI.HandlePause();
        }
    }
}