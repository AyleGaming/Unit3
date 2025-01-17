using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public GameObject door;
    public float timer;

    private void OnTriggerEnter(Collider other)
    {
//        door.SetActive(false);
    }

    // Detect every frame in front of door
    private void OnTriggerStay(Collider other)
    {
        timer += Time.deltaTime;

        if(timer >= 3)
        {
            door.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        timer = 0;
        door.SetActive(true);
    }
}
