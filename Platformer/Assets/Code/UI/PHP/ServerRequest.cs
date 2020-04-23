using SoulHunter.Base;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ServerRequest : MonoBehaviour
{
    public static ServerRequest Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator RegisterAccount(string json)
    {
        WWWForm form = new WWWForm();
        form.AddField("userdata", json);
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/edsa-Soul%20Hunter%20Data/CreateAccount.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "Account made")
                {
                    DataManager.createdAccount = true;
                }
                else
                {
                    DataManager.createdAccount = false;
                }
                CustomEvents.OnLoginOrRegistry?.Invoke();
            }
        }
    }

    public IEnumerator LoginAccount(string json)
    {
        WWWForm form = new WWWForm();
        form.AddField("userdata", json);
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/edsa-Soul%20Hunter%20Data/Login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text == "Logged in")
                {
                    DataManager.loggedIn = true;
                }
                else
                {
                    DataManager.loggedIn = false;
                }
                CustomEvents.OnLoginOrRegistry?.Invoke();
            }
        }
    }

    private IEnumerator UploadStats(string json)
    {
        WWWForm form = new WWWForm();
        form.AddField("userstats", json);
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/edsa-Soul%20Hunter%20Data/UpdateStats.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public void UpdateStats()
    {
        UserStats stats = new UserStats();
        stats.username = DataManager.username;
        stats.collected_souls = DataManager.Instance.soulsCollected;
        stats.times_teleported = DataManager.Instance.timesTeleported;
        stats.times_jumped = DataManager.Instance.timesJumped;
        stats.times_grappled = DataManager.Instance.timesHitGrapple;
        string json = JsonUtility.ToJson(stats);
        StartCoroutine(UploadStats(json));
    }


    [System.Serializable]
    public class Data
    {
        public fruit[] fruits;
    }
    [System.Serializable]
    public class fruit
    {
        public int id;
        public string name;
        public string variety;
    }
}

[System.Serializable]
public struct LoginData
{
    public string username;
    public string password;
}

[System.Serializable]
public struct UserStats
{
    public string username;
    public int collected_souls;
    public int times_teleported;
    public int times_jumped;
    public int times_grappled;
}