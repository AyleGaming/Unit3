using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField] private Transform grabHand;
    [SerializeField] private float syncStrength;
    private Rigidbody objectGrabbed;
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private AudioClip dropSound;

    // Start is called before the first frame update
    public void PickUpObject(Rigidbody objectToGrab)
    {

        if(objectGrabbed != null)
        {
            DropDownObject();
            if (dropSound != null)
            {
                AudioManager.Instance.PlaySound(dropSound);
            }
            return;
        }

        if(objectToGrab != null)
        {
            objectGrabbed = objectToGrab;
            objectToGrab.useGravity = false;
            objectToGrab.drag = 10;
            objectToGrab.transform.position = grabHand.position;
            if (pickUpSound != null)
            {
                AudioManager.Instance.PlaySound(pickUpSound);
            }
        }
    }

    // Update is called once per frame
    public void DropDownObject()
    {
        objectGrabbed.useGravity = true;
        objectGrabbed.drag = 0;
        objectGrabbed.drag = 0;
        objectGrabbed = null;
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
}
