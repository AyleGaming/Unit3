using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // SINGLETON PATTERN
    public static PlayerInput Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] private Move playerMovement;
    [SerializeField] private Look playerLook;
    [SerializeField] private Shoot playerShoot;
    [SerializeField] private Jump playerJump;
    [SerializeField] private Interact playerInteract;

    // Directional Inputs
    [SerializeField] private Vector2 lookDirection;
    
    [SerializeField] private float mouseSensitivity;

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
        // Movement
        if (playerMovement)
        {
            Vector3 moveDir = new Vector3();
            moveDir.x = Input.GetAxis("Horizontal");
            moveDir.z = Input.GetAxis("Vertical");
            playerMovement.MoveAbility(moveDir);
        }
      
        // Look
        if (playerLook)
        {
            lookDirection.x += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
            lookDirection.y += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
            
            float angleOnY = lookDirection.y;
            lookDirection.y = Mathf.Clamp(angleOnY, -80, 80);

            playerLook.LookAbility(lookDirection);
        }

        // Shoot gun
        bool isBlasterActive = GetComponent<PlayerController>().isBlasterActive;
        if (isBlasterActive && playerShoot != null && Input.GetMouseButtonDown(0))
        {
            playerShoot.ShootAbility();
        }

        // Jump
        if (playerJump != null && Input.GetKeyDown(KeyCode.Space))
        {
            playerJump.JumpAbility();
        }

        if(playerInteract && Input.GetKeyDown(KeyCode.F))
        {
            playerInteract.InteractAbility();
        }
    }

    private void OnDrawGizmos()
    {
        // draw sphere on feet of player (0,0,0)
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
