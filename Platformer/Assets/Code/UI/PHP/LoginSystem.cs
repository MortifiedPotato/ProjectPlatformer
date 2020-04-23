using SoulHunter.Base;
using TMPro;
using UnityEngine;

public class LoginSystem : MonoBehaviour
{
    [SerializeField] GameObject loginFields;
    [SerializeField] GameObject displayUsername;
    [SerializeField] GameObject feedback;

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
            DataManager.username = usernameField.text;
            displayUsername.GetComponent<TextMeshProUGUI>().text = DataManager.username;

            displayUsername.SetActive(true);
            feedback.SetActive(false);
            loginFields.SetActive(false);
        }
        else
        {
            loginFields.SetActive(true);
            feedback.SetActive(true);
            displayUsername.SetActive(false);

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

    public void UpdateStats()
    {
        UserStats stats = new UserStats();
        stats.username = DataManager.username;
        stats.collectedSouls = DataManager.Instance.soulsCollected;
        stats.timesTeleported = DataManager.Instance.timesTeleported;
        string json = JsonUtility.ToJson(stats);
        StartCoroutine(server.GetFruitASync(json));
    }
}
