using SoulHunter.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortalCondition : MonoBehaviour
{
    private void Update()
    {
        if (DataManager.Instance.soulsCollected >= 20)
        {
            GetComponent<PortalScript>().isActivatable = true;
        }
    }
}
