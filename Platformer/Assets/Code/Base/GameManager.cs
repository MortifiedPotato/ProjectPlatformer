using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SceneController SceneController { get; set; }
    public UIManager UIManager { get; set; }
    public Database Database { get; set; }

    private void Awake()
    {
        Instance = this;
        name = "Managers";
        DontDestroyOnLoad(this);
    }
}