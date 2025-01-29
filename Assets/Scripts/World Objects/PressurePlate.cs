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

    private void OnTriggerEnter(Collider other)
    {
        foreach (Rigidbody rb in correctRigidBodies)
        {
            if (unlockWithAnyObject || rb == other.attachedRigidbody)
            {
                OnPressureStart.Invoke();
                isPressed = true;
                return;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        foreach (Rigidbody rb in correctRigidBodies)
        {
            if (unlockWithAnyObject || rb == other.attachedRigidbody)
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
