using UnityEngine;

public class CustomEvents : MonoBehaviour
{
    public delegate void UserLogin();
    public static UserLogin OnLoginOrRegistry;
}
