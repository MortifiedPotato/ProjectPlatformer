using UnityEngine.UI;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;

    private void Start()
    {
        GameManager.Instance.UIManager.GameUI = this;
        Resume();
    }

    private void OnDisable()
    {
        Resume();
    }

    public void HandlePause()
    {
        if (!UIManager.isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        UIManager.isPaused = false;
    }

    void Pause()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        UIManager.isPaused = true;
    }
}
