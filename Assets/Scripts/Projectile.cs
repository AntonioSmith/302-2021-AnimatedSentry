using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = .05f;

    public Transform player;
    public Transform enemy;

    public Vector3 velocity = new Vector3();

    public float bulletLifetime = 3; // Time before bullet automatically destroys self

    private void Update()
    {
        //velocity = transform.TransformDirection(Vector3.forward * 10);
        transform.position += transform.TransformDirection(Vector3.down * speed); // Have to set direction to down due to model moving upward as you fire
        //transform.rotation = player.transform.rotation;
        //transform.position += velocity;

        if (bulletLifetime <= 5)
        {
            bulletLifetime -= Time.deltaTime; // if bullet still has time left, countdown timer
        }
        if (bulletLifetime < .01f)
        {
            Destroy(gameObject); // if bullet has no time left, destroy bullet
        }
    }

    private void OnTriggerEnter(Collider other) // On Overlap
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        HealthSystem playerHealth = player.GetComponent<HealthSystem>();
        //HealthSystem enemyHealth = enemy.GetComponent<HealthSystem>();

        //// if overlapping a player object
        //if (player)
        //{
        //    // if player has health
        //    if (playerHealth)
        //    {
        //        playerHealth.TakeDamage(10);
        //    }
        //    Destroy(gameObject); // remove projectile after overlap
        //} 
        if (enemy)
        {
            Destroy(gameObject);
        }
    }
}
