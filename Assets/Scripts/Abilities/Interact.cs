using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private Transform interactionTip;
    [SerializeField] private LayerMask interactionFilter;
    [SerializeField] private Grab grabAbility;

    public void InteractAbility()
    {
        Ray customRay = new Ray(interactionTip.position, interactionTip.forward);
        RaycastHit tempHit;

        if (!Physics.Raycast(customRay, out tempHit, 5f, interactionFilter)) return;

        IInteractable interactFeature = tempHit.collider.GetComponent<IInteractable>();

        if (interactFeature != null)
        {
            interactFeature.StartInteraction();
        }
        else if (tempHit.rigidbody && !tempHit.rigidbody.gameObject.CompareTag("Player"))
        {
            grabAbility.PickUpObject(tempHit.rigidbody);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white; // Set line color
        Gizmos.DrawLine(interactionTip.position, interactionTip.position + interactionTip.forward * 5f); // Draw line
    }
}
