using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float movementSpeed;
    // Movement Settings;

    [Header("References")]
    [SerializeField] private CharacterController controller;

    public void MoveAbility(Vector3 moveDirection)
    {

        Vector3 forwardMovement = moveDirection.z * transform.forward;
        Vector3 sidewaysMovement = moveDirection.x * transform.right;
        Vector3 movementVector = (forwardMovement + sidewaysMovement);

        // movement here
        controller.Move(movementVector * Time.deltaTime * movementSpeed);

    }
}
