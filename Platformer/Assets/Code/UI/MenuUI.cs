using UnityEngine;

public class MenuUI : MonoBehaviour
{
    //MenuState enum instance
    public MenuStates MenuState = MenuStates.Menu;

    [SerializeField] GameObject[] Panels;

    void Start()
    {
        UIManager.Instance.MenuUI = this;
        SetMenuState();

        Cursor.visible = true;
    }

    public void ChangeMenuState(int index)
    {
        MenuState = (MenuStates)index;
        SetMenuState();
    }

    void SetMenuState()
    {
        // Set all panels to false
        for (int i = 0; i < Panels.Length; i++)
        {
            Panels[i].SetActive(false);
        }

        // Activate proper panel to the scene
        switch (MenuState)
        {
            case MenuStates.Menu:
                Panels[0].SetActive(true);
                break;
            case MenuStates.Controls:
                Panels[1].SetActive(true);
                break;
            case MenuStates.Credits:
                Panels[2].SetActive(true);
                break;
        }
    }
}
