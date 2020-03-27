using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Player;

public class PortalScript : MonoBehaviour
{
    public PolygonCollider2D confiner;
    public GameObject otherPortal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<PlayerBase>().isTeleporting = true;
        }
    }
}
