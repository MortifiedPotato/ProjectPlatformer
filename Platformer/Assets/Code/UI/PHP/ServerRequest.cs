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
        string json = JsonUtility.ToJson(InputSystem.loginData);
        WWWForm form = new WWWForm();
        form.AddField("Counter", Counter);
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/edsa-phpServer/fruit.php", form))
        {
            yield return www.SendWebRequest();

            Debug.Log(www.downloadHandler.text);
            Counter++;
        }
    }

    public IEnumerator CreateAccount(string json)
    {
        WWWForm form = new WWWForm();
        form.AddField("userdata", json);
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/edsa-phpServer/CreateAccount.php", form))
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
        using (UnityWebRequest www = UnityWebRequest.Post("http://127.0.0.1/edsa-phpServer/login.php", form))
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
    public string email;
    public string password;
}

