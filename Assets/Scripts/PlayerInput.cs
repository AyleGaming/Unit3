using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // Directional Inputs
    [SerializeField] private Vector2 lookDirection;
    [SerializeField] private Vector3 moveDirection;

    // Reference to the controller
    [SerializeField] private CharacterController controller;
    // Reference to the head controller
    [SerializeField] private Camera head;

    [SerializeField] private float mouseSensitivity;
   
    // Movement Settings;
    [SerializeField] private float movementSpeed;

    // Gravity and jumping settings
    [SerializeField] private float gravityForce;
    [SerializeField] private float jumpForce;

    [SerializeField] private LayerMask groundLayer;

    [Header("Shooting Settings")]
    [SerializeField] private Transform weaponTip;
    [SerializeField] private Rigidbody projectilePrefab;
    [SerializeField] private float shootingForce;




    // Start is called before the first frame update
    void Start()
    {
        // Hide mouse cursor
        Cursor.visible = false;
        // Lock to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLook();
        PlayerMove();

        // Shoot gun
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody clonedRigidBody = Instantiate(projectilePrefab, weaponTip.position, weaponTip.rotation);
            clonedRigidBody.AddForce(weaponTip.forward * shootingForce);
        }

    }

    private void PlayerMove()
    {
        moveDirection.x = Input.GetAxis("Horizontal");
        moveDirection.z = Input.GetAxis("Vertical");

        Vector3 forwardMovement = moveDirection.z * transform.forward;
        Vector3 sidewaysMovement = moveDirection.x * transform.right;
        Vector3 movementVector = (forwardMovement + sidewaysMovement);

        // movement here
        controller.Move(movementVector * Time.deltaTime * movementSpeed);



        bool isOnGround = Physics.CheckSphere(transform.position, 0.1f, groundLayer);
                
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            gravityForce = jumpForce;
            isOnGround = false;
        }

        // gravity here
        Debug.Log(isOnGround);
        if(!isOnGround)
        {
            gravityForce += -10f * Time.deltaTime;
            controller.Move(Vector3.up * gravityForce * Time.deltaTime);
        } else
        {
            gravityForce = 0;
        }

    }

    private void PlayerLook()
    {
        lookDirection.x += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        lookDirection.y += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        float angleOnY = lookDirection.y;
        lookDirection.y = Mathf.Clamp(angleOnY, -80, 80);

        head.transform.localRotation = Quaternion.Euler(-lookDirection.y, 0, 0);
        transform.rotation = Quaternion.Euler(0, lookDirection.x, 0);

    }

    private void OnDrawGizmos()
    {
        // draw sphere on feet of player (0,0,0)
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
