using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IStatusChangeable
{
    [SerializeField] private PhysicalButton doorButton;
    [SerializeField] private Vector3 openOffset;
    [SerializeField] private float doorSpeed;
    private Vector3 closedPosition;
    [SerializeField] private bool isActiveStatus = false;
    [SerializeField] private Colors doorColor;
    [SerializeField] private AudioClip doorSound;


    // Start is called before the first frame update
    void Start()
    {
        closedPosition = transform.position;
        if(doorButton != null) doorButton.OnPressed += OpenDoor;
        StatusManager.Instance?.RegisterStatusChangeable(this, doorColor);

    }

    // Update is called once per frame
    void Update()
    {
        if (isActiveStatus)
        {
            Vector3 targetPosition = closedPosition + openOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * doorSpeed);
        } 
        else
        {
            transform.position = Vector3.Lerp(transform.position, closedPosition, Time.deltaTime * doorSpeed);
        }
    }

    public void ToggleDoor()
    {
        if(isActiveStatus == false)
        {
            CloseDoor();
        } else
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        if (doorSound != null && isActiveStatus == false)
        {
            AudioManager.Instance.PlaySound(doorSound);
        }
        isActiveStatus = true;
    }

    public void CloseDoor()
    {
        isActiveStatus = false;
    }

    public void SetStatus(bool status)
    {
        isActiveStatus = status;
    }
    public bool GetStatus()
    {
        return isActiveStatus;
    }

}
