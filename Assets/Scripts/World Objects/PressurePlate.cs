using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour, IPuzzlePiece
{
    [SerializeField] private bool unlockWithAnyObject;
    [SerializeField] private List<Rigidbody> correctRigidBodies = new();
    private bool isPressed;

    public UnityEvent OnPressureStart = new UnityEvent();
    public UnityEvent OnPressureExit = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if(rb != null && correctRigidBodies.Contains(rb))
        {
            correctRigidBodies.Remove(rb);
        }

        if(correctRigidBodies.Count == 0 || unlockWithAnyObject)
        {
            if (isPressed == false)
            {
                AudioManager.Instance.PlaySound(SoundType.PuzzleSuccess);
            }
            OnPressureStart.Invoke();
            isPressed = true;
            transform.gameObject.SetActive(false);
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
