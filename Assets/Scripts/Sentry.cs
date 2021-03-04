using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sentry : MonoBehaviour
{
    public Transform player;
    public Transform barrel;

    public GameObject bullet;

    public float cooldownShoot = 0;
    float cooldownPick = 0;
    public float roundsPerSecond = 10;

    public ParticleSystem prefabMuzzleFlash;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void ShootPlayer()
    {
        cooldownShoot = 1 / roundsPerSecond;

        Instantiate(prefabMuzzleFlash, barrel.position, barrel.rotation);
        Instantiate(bullet, barrel.transform.position,barrel.transform.rotation);
    }

    void LookAtPlayer()
    {

    }
}
