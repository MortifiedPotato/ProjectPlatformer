using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floorchecker : MonoBehaviour
{
    EnemyScript Enemy;

    private void Start()
    {
        Enemy = GetComponentInParent<EnemyScript>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Environment")
        {
            Enemy.MoveRight = !Enemy.MoveRight;
        }
    }
}
