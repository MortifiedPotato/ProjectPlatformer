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
    }

    void HandleLoginPanel()
    {
        if (DataManager.loggedIn)
        {
            displayUsername.GetComponent<TextMeshProUGUI>().text = DataManager.username;

            displayUsername?.SetActive(true);
            Button_Logout?.SetActive(true);
            loginFields?.SetActive(false);
            feedback?.SetActive(false);
        }
        else
        {
            displayUsername?.SetActive(false);
            Button_Logout?.SetActive(false);
            loginFields?.SetActive(true);
            feedback?.SetActive(true);

            feedback.GetComponent<TextMeshProUGUI>().text = "Invalid username or password";

            if (DataManager.createdAccount)
            {
                feedback.GetComponent<TextMeshProUGUI>().text = "Account Made";
                DataManager.createdAccount = false;
            }
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
        else
        {
            DataManager.username = usernameField.text;
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

    private void OnDestroy()
    {
        CustomEvents.OnLoginOrRegistry -= HandleLoginPanel;
    }
}
