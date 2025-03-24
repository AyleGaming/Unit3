using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IStatusChangeable
{
    [SerializeField] private Vector3 openOffset;
    [SerializeField] private float doorSpeed;
    [SerializeField] private bool isActiveStatus = false;
    [SerializeField] private Colors doorColor;

    private Vector3 closedPosition;

    // Start is called before the first frame update
    void Start()
    {
        closedPosition = transform.position;
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
        if (isActiveStatus == false)
        {
            AudioManager.Instance.PlaySound(SoundType.DoorOpen);
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
