using System.Collections.Generic;
using UnityEngine;

using SoulHunter.Player;
using SoulHunter;
using System.Runtime.Remoting.Messaging;

public class PortalScript : Interactable // Mort
{
    // Scriptable Object
    [SerializeField] PortalData portalType;

    // Closest Cinemachine Confiner
    PolygonCollider2D closestConfiner;

    // List of connected portals
    List<PortalScript> connectedPortals = new List<PortalScript>();

    private void Start()
    {
        AssignConnectedPortals();

        closestConfiner = AssignClosestConfiner();

        // Stop all particles
        foreach (var particleSystem in GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Stop();
        }
    }

    PolygonCollider2D AssignClosestConfiner()
    {
        // Save all objects with "Confiner" tag in an array
        GameObject[] allConfiners = GameObject.FindGameObjectsWithTag("Confiner");
        float shortestDistance = Vector3.Distance(allConfiners[0].transform.position, transform.position);
        GameObject nearestConfiner = allConfiners[0];

        // Loop through all confiner objects and assign the closest one to a variable
        foreach (var confiner in allConfiners)
        {
            var pos = confiner.transform.position;
            var dist = Vector3.Distance(pos, transform.position);

            if (dist <= shortestDistance)
            {
                nearestConfiner = confiner;
            }
        }
        
        // Return the collider of the nearest confiner
        return nearestConfiner.GetComponent<PolygonCollider2D>();
    }

    void AssignConnectedPortals()
    {
        // Save all objects with "Portal" tag in an array
        GameObject[] allPortals = GameObject.FindGameObjectsWithTag("Portal");

        // Save Portals that have the same assigned color into Connected Portals list
        for (int i = 0; i < allPortals.Length; i++)
        {
            if (allPortals[i].Equals(gameObject))
            {
                continue;
            }

            if (allPortals[i].GetComponent<PortalScript>().portalType.color == portalType.color)
            {
                connectedPortals.Add(allPortals[i].GetComponent<PortalScript>());
            }
        }

        // Warn if no portals are connected to current portal
        if (connectedPortals.Count == 0)
        {
            Debug.LogError($"No portals connected to {gameObject.name} in {transform.root.name}.");
        }

        // Warn if multiple portals are connected to current portal
        if (connectedPortals.Count > 1)
        {
            Debug.Log($"Multiple portals connected to {gameObject.name} in {transform.root.name}. This will randomize its destination.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActivatable || isRepeatable)
        {
            // Play all particles
            foreach (var particleSystem in GetComponentsInChildren<ParticleSystem>())
            {
                particleSystem.Play();
            }
        }

        if (collision.CompareTag("Player"))
        {   // Update Cinemachine Confiner
            CameraManager.Instance.UpdateConfiner(closestConfiner);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!GameManager.interacting) return;

        if (connectedPortals.Count == 0) return;

        if (PlayerBase.isSwinging) return;

        if (collision.CompareTag("Player"))
        {
            if (isActivatable || isRepeatable)
            {
                // Set player teleportation state
                PlayerBase.isTeleporting = true;
                PlayerBase.isPaused = true;

                // Set player teleportation destination
                PlayerBase.teleportDestination = connectedPortals[Random.Range(0, connectedPortals.Count)].transform;

                // Play teleportation audio
                AudioManager.PlaySound(AudioManager.Sound.TeleportDissolve, transform.position);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Stop all particles
        foreach (var particleSystem in GetComponentsInChildren<ParticleSystem>())
        {
            particleSystem.Stop();
        }
    }
}