using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : MonoBehaviour
{
    public Transform player;
    public Transform barrel;

    public GameObject laser;

    public float cooldownShoot = 0;
    public float roundsPerSecond = 6;

    public ParticleSystem prefabMuzzleFlash;

    void Start()
    {
        
    }

    void Update()
    {
        if (cooldownShoot > 0)
        {
            cooldownShoot -= Time.deltaTime;
        }

        ShootPlayer();
    }

    private void ShootPlayer()
    {
        if (cooldownShoot > 0) return; // still on cooldown

        cooldownShoot = 1 / roundsPerSecond;

        Instantiate(prefabMuzzleFlash, barrel.position, barrel.rotation);
        Instantiate(laser, barrel.transform.position, barrel.transform.rotation);
        Soundboard.PlayLaser();
    }

    void LookAtPlayer()
    {

    }
}
