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
    public float visionAngle = 45; // cone of vision

    private List<TargetableThing> potentialTargets = new List<TargetableThing>();

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks mouse to game screen
    }

    void Update()
    {
        wantsToTarget = Input.GetButton("Fire2");

        if (!wantsToTarget) target = null;

        cooldownScan -= Time.deltaTime; // countdown
        if (cooldownScan <= 0 || (target == null && wantsToTarget)) ScanForTargets(); // when countdown completed

        cooldownPick -= Time.deltaTime;
        if (cooldownPick <= 0) PickATarget();

        if (target && !CanSeeThing(target))
        {
            target = null;
        }
    }

    private bool CanSeeThing(Transform thing)
    {
        if (!thing) return false; // error break

        Vector3 vToThing = thing.position - transform.position;

        // check distance
        if (vToThing.sqrMagnitude > visionDistance * visionDistance) return false; // too far away to see

        // check direction
        if (Vector3.Angle(transform.forward, vToThing) > visionAngle) return false; // outside of vision cone

        // TODO: chock occlusuion (is something between player and target)

        return true;
    }

    private void ScanForTargets()
    {
        cooldownScan = 1; // restarts scan every 1 seconds

        potentialTargets.Clear(); // empites list

        TargetableThing[] things = GameObject.FindObjectsOfType<TargetableThing>();

        foreach(TargetableThing thing in things)
        {
            //if we can see it
            // add target to potentialTargets

            if (CanSeeThing(thing.transform))
            {
                potentialTargets.Add(thing);
            }

            //Vector3 disToThing = thing.transform.position - transform.position;

            //if (disToThing.magnitude < visionDistance * visionDistance)
            //{
            //    if (Vector3.Angle(transform.forward, disToThing) < 45)
            //    {
            //        potentialTargets.Add(thing);
            //    }
            //} 
        }
    }

    void PickATarget()
    {
        cooldownPick = .25f; 

        //if (target) return; // if you already have a target
        target = null;

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
