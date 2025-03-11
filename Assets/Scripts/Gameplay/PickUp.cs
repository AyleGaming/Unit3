using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private AudioClip pickUpSound;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) // Check if it's the player
        {
            PlayerController player = collider.GetComponent<PlayerController>();

            if (player != null && player.blaster != null)
            {
                player.blaster.SetActive(true); // Activate the player's blaster
                if (pickUpSound != null)
                {
                    AudioManager.Instance.PlaySound(pickUpSound);
                }
                Destroy(gameObject); // Destroy the pickup
            }
        }
    }

}
