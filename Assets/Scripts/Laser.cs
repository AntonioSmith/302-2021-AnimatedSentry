using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = .05f;

    public Transform player;

    public Vector3 velocity = new Vector3();

    public float laserLifetime = 3; // Time before laser automatically destroys self

    private void Update()
    {
        transform.position += transform.TransformDirection(Vector3.up * speed); // Have to set direction to down due to model moving upward as you fire

        if (laserLifetime <= 5)
        {
            laserLifetime -= Time.deltaTime; // if bullet still has time left, countdown timer
        }
        if (laserLifetime < .01f)
        {
            Destroy(gameObject); // if bullet has no time left, destroy bullet
        }
    }

    private void OnTriggerEnter(Collider other) // On Overlap
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        HealthSystem playerHealth = player.GetComponent<HealthSystem>();
        //HealthSystem enemyHealth = enemy.GetComponent<HealthSystem>();

        // if overlapping a player object
        if (player)
        {
            // if player has health
            if (playerHealth)
            {
                playerHealth.TakeDamage(5);
                Soundboard.PlayDamage();
            }
            Destroy(gameObject); // remove projectile after overlap
        }
    }
}
