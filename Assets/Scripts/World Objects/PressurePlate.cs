using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour, IPuzzlePiece
{
    [SerializeField] private bool unlockWithAnyObject;
    [SerializeField] private Rigidbody[] correctRigidBodies;

    public UnityEvent OnPressureStart = new UnityEvent();
    public UnityEvent OnPressureExit = new UnityEvent();

    private bool isPressed;
    private int totalObjectsRequiredToOpen;
    private int totalObjectsOnPressurePad = 0;

    private void OnTriggerEnter(Collider other)
    {
        totalObjectsRequiredToOpen = correctRigidBodies.Length;
        
        foreach (Rigidbody rb in correctRigidBodies)
        {
            if(rb == other.attachedRigidbody)
            {
                totalObjectsOnPressurePad++;
            }
        }

        if (unlockWithAnyObject || totalObjectsOnPressurePad == totalObjectsRequiredToOpen)
        {
            OnPressureStart.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (Rigidbody rb in correctRigidBodies)
        {
            if (rb == other.attachedRigidbody)
            {
                OnPressureExit.Invoke();
                isPressed = false;
                return;
            }
        }
    }

    public void LinkToPuzzle(Puzzle p)
    {
    }

    // Implementation of interface to pressure plate
    public bool IsCorrect()
    {
        return isPressed;
    }
}
