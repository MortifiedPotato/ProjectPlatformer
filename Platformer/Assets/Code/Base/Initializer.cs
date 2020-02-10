using System.IO;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    // Name and location of the Manager prefab
    static string ManagerPath = "Base/Managers";

    /// <summary>
    /// Loads the necessary prefabs before the scene is loaded
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Initialize()
    {
        if (File.Exists(Application.dataPath + "/Resources/" + ManagerPath + ".prefab"))
        {
            Instantiate(Resources.Load(ManagerPath));
        }
        else
        {
            Debug.LogError("Managers prefab can not be found at Resources/" + ManagerPath);
        }
    }
}