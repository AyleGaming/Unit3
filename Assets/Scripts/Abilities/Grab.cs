using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private Transform grabHand;
    [SerializeField] private float syncStrength;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip dropSound;
    private bool pickedUpObject = false;

    private Rigidbody objectGrabbed;


    // Start is called before the first frame update
    public void PickUpObject(Rigidbody objectToGrab)
    {

        if(objectGrabbed != null)
        {
            DropDownObject();
            return;
        }

        if(objectToGrab != null)
        {
            objectGrabbed = objectToGrab;
            objectToGrab.useGravity = false;
            objectToGrab.drag = 10;
            objectToGrab.transform.position = grabHand.position;
            pickedUpObject = true;
            if (pickUpSound != null)
            {
                AudioManager.Instance.PlaySound(pickUpSound);
            }
        }
    }

    public bool HasPickedUpObject()
    {
        return pickedUpObject;
    }

    // Update is called once per frame
    public void DropDownObject()
    {
        pickedUpObject = false;
        objectGrabbed.useGravity = true;
        objectGrabbed.drag = 0;
        objectGrabbed.drag = 0;
        objectGrabbed = null;
        if (dropSound != null)
        {
            AudioManager.Instance.PlaySound(dropSound);
        }
    }

    private void Update()
    {
        if(objectGrabbed != null && Vector3.Distance(grabHand.position, objectGrabbed.transform.position) > 0.01f)
        {
            MoveObject();
        }
    }

    public void MoveObject()
    {
        Vector3 targetDirection = grabHand.position - objectGrabbed.transform.position;
        objectGrabbed.AddForce(targetDirection * syncStrength );
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; // Set line color
        Gizmos.DrawLine(grabHand.position, grabHand.position + grabHand.forward * 5f); // Draw line
    }
}
