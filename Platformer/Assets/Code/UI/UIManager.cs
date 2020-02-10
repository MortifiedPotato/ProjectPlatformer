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

        //Disable Mouse Cursor
        //Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (GameUI)
        {
            GameUI.HandlePause();
        }
    }
}