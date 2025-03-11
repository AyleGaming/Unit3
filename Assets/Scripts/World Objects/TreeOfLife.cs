using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreeOfLife : MonoBehaviour
{
    public static TreeOfLife Instance { get; private set; }

    [SerializeField] private AudioClip gameOverSound;
    public Action OnGameOver;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")) // Check if it's the player
        {
            PlayerController player = collider.GetComponent<PlayerController>();

            if (player != null)
            {
                if (gameOverSound != null)
                {
                    AudioManager.Instance.PlaySound(gameOverSound);
                }
                OnGameOver?.Invoke();
                Time.timeScale = 0f;
            }
        }
    }
}
