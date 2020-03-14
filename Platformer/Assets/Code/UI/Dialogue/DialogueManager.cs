using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace SoulHunter.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        private Queue<string> sentences;

        bool nonTriggerable;

        Animator animator;

        [SerializeField] GameObject dialogueBox;

        [SerializeField] Button ContinueButton;

        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI dialogueText;

        void Start()
        {
            Instance = this;
            sentences = new Queue<string>();

            animator = dialogueBox.GetComponent<Animator>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            animator.SetBool("inDialogue", true);
            ContinueButton.Select();

            nameText.text = dialogue.name;

            sentences.Clear();

            foreach (string sentence in dialogue.sentences)
            {
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            ContinueButton.Select();

            if (nonTriggerable)
            {
                animator.SetBool("inDialogue", false);
                nonTriggerable = false;
            }
            else
            {
                if (sentences.Count == 0)
                {
                    dialogueText.fontStyle = FontStyles.Italic;
                    EndDialogue();
                    return;
                }

                dialogueText.fontStyle = FontStyles.Normal;
                string sentence = sentences.Dequeue();
                StopAllCoroutines();
                StartCoroutine(TypeSentence(sentence));
            }
        }

        IEnumerator TypeSentence(string sentence)
        {
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }

        void EndDialogue()
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(">>End of conversation.<<"));
            nonTriggerable = true;
        }
    }

    [System.Serializable]
    public class Dialogue
    {
        public string name;
        [TextArea(3, 10)]
        public string[] sentences;
    }
}