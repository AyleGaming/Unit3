using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Key : MonoBehaviour
{
    public UnityEvent KeyIsPickedUp = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision!!");
        // make sure npcs dont pick up key
        if (other.CompareTag("Player"))
        {
            KeyIsPickedUp.Invoke();
            Destroy(gameObject); // destroy key
            Debug.Log("Key picked up!");
        }
    }
}
