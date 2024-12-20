using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private Transform interactionTip;
    [SerializeField] private LayerMask interactionFilter;

    public void InteractAbility()
    {
        Ray customRay = new Ray(interactionTip.position, interactionTip.forward);
        RaycastHit tempHit;
        
        if (Physics.Raycast(customRay, out tempHit, 5f, interactionFilter))
        {
            Debug.Log(tempHit.collider.name);

            // Destroy what your attacking
            Destroy(tempHit.collider.gameObject);
        }
        else
        {
            Debug.Log("NO HIT");
        }


//        Debug.Log("Did I hit something? "+Physics.Raycast(customRay, 5f, interactionFilter));
        Debug.DrawRay(interactionTip.position, interactionTip.forward * 5f, Color.green);
    }
}
