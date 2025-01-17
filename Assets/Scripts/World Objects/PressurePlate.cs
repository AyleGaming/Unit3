using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private bool unlockWithAnyObject;
    [SerializeField] private Rigidbody[] correctRigidBodies;

    public UnityEvent OnPressureStart;
    public UnityEvent OnPressureExit;

    private void OnTriggerEnter(Collider other)
    {
        foreach (Rigidbody rb in correctRigidBodies)
        {
            if (unlockWithAnyObject || rb == other.attachedRigidbody)
            {
                OnPressureStart.Invoke();
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
                return;
            }
        }
    }
}
