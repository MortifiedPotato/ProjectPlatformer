using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ServerRequest : MonoBehaviour
{
    private int Counter;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GetFruitASync());
        }
    }

    private IEnumerator GetFruitASync()
    {
        string json = JsonUtility.ToJson(LoginSystem.loginData);
        WWWForm form = new WWWForm();
        form.AddField("Counter", Counter);
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/edsa-phpServer/fruit.php", form))
        {
            yield return www.SendWebRequest();

            Debug.Log(www.downloadHandler.text);
            Counter++;
        }
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
            }
        }
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
public class LoginData
{
    public string username;
    public string password;
}

