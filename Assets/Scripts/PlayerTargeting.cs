using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour 
{
    public Transform target;

    public bool wantsToTarget = false;
    public bool wantsToAttack = false;

    float cooldownScan = 0;
    float cooldownPick = 0;
    float cooldownShoot = 0;

    public float visionDistance = 10;
    public float visionAngle = 45; // cone of vision
    public float roundsPerSecond = 3;

    // References player's arm bones
    public Transform armL;
    public Transform armR;
    public Transform handR;
    public Transform handL;

    public GameObject bullet;

    CameraOrbit camOrbit;

    private Vector3 startPosArmL;
    private Vector3 startPosArmR;

    /// <summary>
    /// A reference ot the particle system prefab to spawn when the gun shoots
    /// </summary>
    public ParticleSystem prefabMuzzleFlash;


    private List<TargetableThing> potentialTargets = new List<TargetableThing>();

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // locks mouse to game screen

        startPosArmL = armL.localPosition;
        startPosArmR = armR.localPosition;

        camOrbit = Camera.main.GetComponentInParent<CameraOrbit>();
    }

    void Update()
    {
        wantsToTarget = Input.GetButton("Fire2");
        wantsToAttack = Input.GetButton("Fire1");

        if (!wantsToTarget) target = null;

        cooldownScan -= Time.deltaTime; // countdown
        if (cooldownScan <= 0 || (target == null && wantsToTarget)) ScanForTargets(); // when countdown completed

        cooldownPick -= Time.deltaTime;
        if (cooldownPick <= 0) PickATarget();

        if (cooldownShoot > 0)
        {
            cooldownShoot -= Time.deltaTime;
        }

        if (target && !CanSeeThing(target))
        {
            target = null;
        }

        SlideArmsHome();

        DoAttack();
    }

    private void SlideArmsHome()
    {
        armL.localPosition = AnimMath.Slide(armL.localPosition, startPosArmL, .01f);
        armR.localPosition = AnimMath.Slide(armR.localPosition, startPosArmR, .01f);
    }

    private void DoAttack()
    {
        // Check if player should attack
        if (cooldownShoot > 0) return; // still on cooldown
        if (!wantsToTarget) return; // not targeting
        if (!wantsToAttack) return; // not shooting
        if (target == null) return; // no target
        if (!CanSeeThing(target)) return; // no target in range

        HealthSystem targetHealth = target.GetComponent<HealthSystem>();

        if (targetHealth)
        {
            targetHealth.TakeDamage(20); // Deal damage to target's health
        }

        cooldownShoot = 1 / roundsPerSecond; 

        // Attack
        if (handL) Instantiate(prefabMuzzleFlash, handL.position, handL.rotation);
        if (handR) Instantiate(prefabMuzzleFlash, handR.position, handR.rotation);

        // Instantiate bullet on hands
        //Instantiate(bullet, handL.transform.position, Quaternion.identity);
        //Instantiate(bullet, handR.transform.position, Quaternion.identity);

        Instantiate(bullet, handL.transform.position, handL.transform.rotation);
        Instantiate(bullet, handR.transform.position, handR.transform.rotation);

        // Trigger arm animation
        armL.localEulerAngles += new Vector3(-20, 0, 0); // LArm recoil on shoot
        armR.localEulerAngles += new Vector3(-20, 0, 0); // RArm recoil on shoot

        armL.position += -armL.forward * .1f; // LArm pushback on shoot 
        armR.position += -armR.forward * .1f; // RArm pushback on shoot 

        camOrbit.Shake(.5f); // Shakes camera with an intensity of 1
        Soundboard.PlayShoot();
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


            if (dd < closestDistanceSoFar || target == null)             {
                target = pt.transform;
                closestDistanceSoFar = dd;
            }
        }
    }
}