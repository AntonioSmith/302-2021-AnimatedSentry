using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // On Overlap
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        // if overlapping a player object
        if (player)
        {
            HealthSystem playerHealth = player.GetComponent<HealthSystem>();
            // if player has health
            if (playerHealth)
            {
                playerHealth.TakeDamage(10);
            }
            Destroy(gameObject); // remove projectile after overlap
        }
    }
}
