using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour 
{
    public Transform target;
    public bool wantsToTarget = false;

    float cooldownScan = 0;
    float cooldownPick = 0;
    public float visionDistance = 10;

    private List<TargetableThing> potentialTargets = new List<TargetableThing>();

    void Start()
    {
        
    }

    void Update()
    {
        wantsToTarget = Input.GetButton("Fire2");

        cooldownScan -= Time.deltaTime; // countdown
        if (cooldownScan <= 0) ScanForTargets(); // when countdown completed

        cooldownPick -= Time.deltaTime;
        if (cooldownPick <= 0) PickATarget();
    }

    private void ScanForTargets()
    {
        cooldownScan = 1; // restarts scan at 2 seconds

        TargetableThing[] things = GameObject.FindObjectsOfType<TargetableThing>();

        foreach(TargetableThing thing in things)
        {
            // check how far away thing is

            Vector3 disToThing = thing.transform.position - transform.position;

            if (disToThing.magnitude < visionDistance * visionDistance)
            {
                if (Vector3.Angle(transform.forward, disToThing) < 45)
                {
                    potentialTargets.Add(thing);
                }
            } 

            // check what direction thing is in
        }
    }

    void PickATarget()
    {
        if (target) return; // if you already have a target

        float closestDistanceSoFar = 0;

        // finds closest targetable thing and sets it as target
        foreach (TargetableThing pt in potentialTargets)
        {
            float dd = (pt.transform.position - transform.position).sqrMagnitude;

            if (dd < closestDistanceSoFar || target == null) 
            {
                target = pt.transform;
                closestDistanceSoFar = dd;
            }
        }
    }

}
