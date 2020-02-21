using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Header("Game Statistics")]
    public int soulsCollected;
    public int velocityScore;
    public float durationInAir;

    [Header("Input Statistics")]
    public int timesJumped;
    public int timesMissedGrapple;
    public int timesHitGrapple;
    public int timesMissedAttack;
    public int timesHitAttack;

    public List<HealthScript> AliveEntities = new List<HealthScript>();

    void Start()
    {
        GameManager.Instance.dataManager = this;
    }

    public void HandleEntityRegistry(HealthScript entity)
    {
        if (!AliveEntities.Contains(entity))
        {
            AliveEntities.Add(entity);
        }
        else
        {
            AliveEntities.Remove(entity);
        }
    } 
}
