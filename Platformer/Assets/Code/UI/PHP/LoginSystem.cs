using SoulHunter.Base;
using TMPro;
using UnityEngine;

public class LoginSystem : MonoBehaviour
{
    [SerializeField] GameObject loginFields;
    [SerializeField] GameObject displayUsername;
    [SerializeField] GameObject feedback;
    [SerializeField] GameObject Button_Logout;

    [SerializeField] TMP_InputField usernameField;
    [SerializeField] TMP_InputField passwordField;
    ServerRequest server;

    public static LoginData loginData;

    private void Start()
    {
        server = FindObjectOfType<ServerRequest>();

        HandleLoginPanel();
        CustomEvents.OnLoginOrRegistry += HandleLoginPanel;

        feedback.SetActive(false);

        if (DataManager.loggedIn)
        {
            displayUsername.GetComponent<TextMeshProUGUI>().text = DataManager.username;
        }
    }

    void HandleLoginPanel()
    {
        if (DataManager.loggedIn)
        {
            DataManager.username = usernameField.text;
            displayUsername.GetComponent<TextMeshProUGUI>().text = DataManager.username;

            displayUsername.SetActive(true);
            feedback.SetActive(false);
            loginFields.SetActive(false);
            Button_Logout.SetActive(true);
        }
        else
        {
            loginFields.SetActive(true);
            feedback.SetActive(true);
            displayUsername.SetActive(false);
            Button_Logout.SetActive(false);

            feedback.GetComponent<TextMeshProUGUI>().text = "Wrong Combination";
        }

        if (DataManager.createdAccount)
        {
            feedback.SetActive(true);
            feedback.GetComponent<TextMeshProUGUI>().text = "Account Made";

            DataManager.createdAccount = false;
        }
        else
        {
            feedback.GetComponent<TextMeshProUGUI>().text = "Invalid username or password";
        }
    }

    public void InitiateRegister()
    {
        if (string.IsNullOrEmpty(usernameField.text) || string.IsNullOrEmpty(passwordField.text))
        {
            feedback.SetActive(true);
            feedback.GetComponent<TextMeshProUGUI>().text = "Invalid username or password";
            return;
        }

        loginData = new LoginData();
        loginData.username = usernameField.text;
        loginData.password = passwordField.text;
        string json = JsonUtility.ToJson(loginData);
        StartCoroutine(server.RegisterAccount(json));
    }

    public void InitiateLogin()
    {
        if (string.IsNullOrEmpty(usernameField.text) || string.IsNullOrEmpty(passwordField.text))
        {
            feedback.SetActive(true);
            feedback.GetComponent<TextMeshProUGUI>().text = "Invalid username or password";
            return;
        }

        loginData = new LoginData();
        loginData.username = usernameField.text;
        loginData.password = passwordField.text;
        string json = JsonUtility.ToJson(loginData);
        StartCoroutine(server.LoginAccount(json));
    }

    public void LogOut()
    {
        DataManager.username = null;
        DataManager.loggedIn = false;
        usernameField.text = "";
        passwordField.text = "";

        HandleLoginPanel();
    }
}
