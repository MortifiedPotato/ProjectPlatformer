using SoulHunter.Base;
using TMPro;
using UnityEngine;

public class LoginSystem : MonoBehaviour
{
    // Variables door Mort
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
        // Start functie door Mort
        server = FindObjectOfType<ServerRequest>();

        HandleLoginPanel();
        CustomEvents.OnLoginOrRegistry += HandleLoginPanel;

        feedback.SetActive(false);
    }

    void HandleLoginPanel()
    {
        // Deze hele functie door mij (Mort) - Ik heb meer de UI functionaliteit gemaakt en bij JSON gedeeltes
        // Thomas geholpen maar zelf laten programmeren.
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
        // If statement door Mort
        if (string.IsNullOrEmpty(usernameField.text) || string.IsNullOrEmpty(passwordField.text))
        {
            feedback.SetActive(true);
            feedback.GetComponent<TextMeshProUGUI>().text = "Invalid username or password";
            return;
        }

        // Onderstaande door Thomas
        loginData = new LoginData();
        loginData.username = usernameField.text;
        loginData.password = passwordField.text;
        string json = JsonUtility.ToJson(loginData);
        StartCoroutine(server.RegisterAccount(json));
    }

    public void InitiateLogin()
    {
        // If statement door Mort gemaakt
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

        // Onderstaande door Thomas
        loginData = new LoginData();
        loginData.username = usernameField.text;
        loginData.password = passwordField.text;
        string json = JsonUtility.ToJson(loginData);
        StartCoroutine(server.LoginAccount(json));
    }

    public void LogOut()
    {
        // Deze functie door Mort
        DataManager.username = null;
        DataManager.loggedIn = false;
        usernameField.text = "";
        passwordField.text = "";

        HandleLoginPanel();
    }

    private void OnDestroy()
    {
        // Mort
        CustomEvents.OnLoginOrRegistry -= HandleLoginPanel;
    }
}
