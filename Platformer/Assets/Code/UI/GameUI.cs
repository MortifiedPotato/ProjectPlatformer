using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using SoulHunter.Player;

namespace SoulHunter.UI
{
    public class GameUI : MonoBehaviour
    {
        private int playerHealth;
        [SerializeField] private Sprite healthImage;
        public float healthOffset;
        public List<GameObject> healthList; 
        private void Start()
        {
            // Gets player health
            playerHealth = FindObjectOfType<PlayerBase>().Health;
            // Sets reference inside UIManager
            UIManager.Instance.GameCanvas = gameObject;
            // Disables cursor
            Cursor.visible = false;

            for (int i = 0; i < playerHealth; i++)
            {
                CreateHealthImage(new Vector2(-900 + i * healthOffset, 470));
            }
        }

        private Image CreateHealthImage(Vector2 anchoredPosition)
        {
            GameObject healthGameObject = new GameObject("playerHealth", typeof(Image));
            healthGameObject.transform.parent = transform;
            healthGameObject.transform.localPosition = Vector2.zero;

            healthGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            healthGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);

            Image heartImage = healthGameObject.GetComponent<Image>();
            heartImage.sprite = healthImage;
            healthList.Add(healthGameObject);
            return heartImage;
        }

        public void removeHeart()
        {
            GameObject Tempname = healthList[healthList.Count - 1];
            healthList.Remove(Tempname);
            Destroy(Tempname);
        }
    }
}