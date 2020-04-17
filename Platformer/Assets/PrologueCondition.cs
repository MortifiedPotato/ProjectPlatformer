using SoulHunter.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueCondition : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(reaperAnimation());
    }

    IEnumerator reaperAnimation()
    {
        yield return new WaitForSeconds(5);

        GetComponent<Animator>().SetBool("isPointing", true);

        yield return new WaitForSeconds(3);

        GetComponent<Animator>().SetBool("isPointing", false);

        Destroy(gameObject, 3);

        yield return null;
    }
}
