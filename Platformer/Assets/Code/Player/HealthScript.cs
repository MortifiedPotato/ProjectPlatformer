using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int HealthPoints = 3;
    public bool isDissolving;
    public SpriteRenderer sprite;
    public float deathHeight = -10;
    public float fade = 1;
    

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Healing();
        }

        if (transform.position.y <= deathHeight)
        {
            isDissolving = true;
        }

        Dissolve();
        CheckDeath();
    }


    public void TakeDamage()
    {
        HealthPoints--;

        if (HealthPoints < 1)
        {
            isDissolving = true;
        }
    }

    public void Healing()
    {
        HealthPoints++;
    }

    void CheckDeath()
    {
        if (fade <= 0f)
        {
            if (GetComponent<InputController>())
            {
                GameManager.Instance.SceneController.ResetScene();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    void Dissolve()
    {
        if (isDissolving)
        {
            fade -= Time.deltaTime;
            if (fade <= 0f)
            {
                fade = 0f;
            }

            sprite.material.SetFloat("_Fade", fade);
        }
        else
        {
            fade += Time.deltaTime;
            if (fade >= 1f)
            {
                fade = 1f;
            }

            sprite.material.SetFloat("_Fade", fade);
        }
    }
}
