using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryTargeting : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        TurnTowardsPlayer();
    }

    public void TurnTowardsPlayer()
    {
        Vector3 disToTarget = target.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(disToTarget, Vector3.up);

        transform.rotation = AnimMath.Slide(transform.rotation, targetRotation, .01f);
    }
}
