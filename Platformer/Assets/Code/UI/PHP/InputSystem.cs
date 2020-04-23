using UnityEngine;
using UnityEngine.UI;

public class InputSystem : MonoBehaviour
{
    [SerializeField] InputField inputMail;
    [SerializeField] InputField inputPassword;
    [SerializeField] ServerRequest server;

    public static LoginData loginData;

    public void initiateCreateAccount()
    {
        loginData = new LoginData();
        loginData.email = inputMail.text;
        loginData.password = inputPassword.text;
        string json = JsonUtility.ToJson(loginData);
        StartCoroutine(server.CreateAccount(json));
    }

    public void initiatelogin()
    {
        loginData = new LoginData();
        loginData.email = inputMail.text;
        loginData.password = inputPassword.text;
        string json = JsonUtility.ToJson(loginData);
        StartCoroutine(server.LoginAccount(json));
    }
}
