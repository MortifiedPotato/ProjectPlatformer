using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int HealthPoints;

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

        print(HealthPoints);
    }


    public void TakeDamage()
    {
        if (HealthPoints <= 1)
        {
            DestroyObject(this);
        }
        else
        {
            HealthPoints--;
        }
    }

    public void Healing()
    {
        HealthPoints++;
    }
}
