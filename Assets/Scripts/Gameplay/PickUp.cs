using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) // Check if it's the player
        {
            PlayerController player = collider.GetComponent<PlayerController>();

            if (player != null && player.blaster != null)
            {
                player.blaster.SetActive(true); // Activate the player's blaster
                Destroy(gameObject); // Destroy the pickup
            }
        }
    }

}
