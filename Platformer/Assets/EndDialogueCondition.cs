using SoulHunter.Dialogue;
using UnityEngine;

public class EndDialogueCondition : MonoBehaviour
{
    private void Update()
    {
        if (!GetComponent<DialogueTrigger>().isActivatable)
        {
            SceneController.Instance.TransitionScene(0);
        }
    }
}
