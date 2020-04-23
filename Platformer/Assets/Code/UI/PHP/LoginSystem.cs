using TMPro;
using UnityEngine;

public class LoginSystem : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_InputField passwordField;
    ServerRequest server;

    public static LoginData loginData;

    private void Start()
    {
        server = FindObjectOfType<ServerRequest>();
    }

    public void InitiateRegister()
    {
        loginData = new LoginData();
        loginData.username = usernameField.text;
        loginData.password = passwordField.text;
        string json = JsonUtility.ToJson(loginData);
        StartCoroutine(server.RegisterAccount(json));
    }

    public void InitiateLogin()
    {
        loginData = new LoginData();
        loginData.username = usernameField.text;
        loginData.password = passwordField.text;
        string json = JsonUtility.ToJson(loginData);
        StartCoroutine(server.LoginAccount(json));
    }
}
