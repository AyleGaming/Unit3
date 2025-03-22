using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private AudioClip pickUpSound;

    public float floatSpeed = 1f;   // Speed of the bounce
    public float floatHeight = 0.5f; // Max height of the bounce

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        // Make the object bounce using a sine wave
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }


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
