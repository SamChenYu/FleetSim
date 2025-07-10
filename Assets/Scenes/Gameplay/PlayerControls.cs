using UnityEngine;

/*
    This script provides jumping and movement in Unity 3D - Gatsby
    https://www.youtube.com/watch?v=6FitlbrpjlQ
*/

public class PlayerControls : MonoBehaviour
{   
    private PlayerShipController playerShipController; // Dependency Injection for playercontrols
    private bool mouseDragging = false;

    // Camera Rotation
    public float mouseSensitivity = 2f;
    private float verticalRotation = 0f;
    private Transform cameraTransform;
    
    // Ground Movement
    private Rigidbody rb;
    public float MoveSpeed = 5f;
    private float moveHorizontal;
    private float moveForward;

    // Jumping
    public float jumpForce = 10f;
    public float fallMultiplier = 2.5f; // Multiplies gravity when falling down
    public float ascendMultiplier = 2f; // Multiplies gravity for ascending to peak of jump
    public LayerMask groundLayer;
    private float playerHeight;
    private float raycastDistance;

    void Start()
    {   

        playerShipController = Object.FindFirstObjectByType<PlayerShipController>();
        if(playerShipController == null)
        {
            Debug.LogError("PlayerShipController component not found on PlayerControls GameObject.");
        }


        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        cameraTransform = Camera.main.transform;

        // Set the raycast to be slightly beneath the player's feet
        playerHeight = GetComponent<CapsuleCollider>().height * transform.localScale.y;
        raycastDistance = (playerHeight / 2) + 0.2f;


    }

    void Update()
    {  


        if (Input.GetKeyDown(KeyCode.Alpha1)) playerShipController.SelectShip(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) playerShipController.SelectShip(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) playerShipController.SelectShip(2);

        if (Input.GetMouseButtonDown(1))
        {
            mouseDragging = true;
        }

        if (Input.GetMouseButtonUp(1))
        {
            mouseDragging = false;
            rb.linearVelocity = Vector3.zero; // Stop movement when mouse is released
        }

    
        if(mouseDragging)
        {
            moveHorizontal = Input.GetAxisRaw("Horizontal");
            moveForward = Input.GetAxisRaw("Vertical");

            RotateCamera();
            // Hides the mouse
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void FixedUpdate()
    {
        if(mouseDragging) MovePlayer();
        
    }

    void MovePlayer()
    {
        Vector3 movement = (transform.right * moveHorizontal + transform.forward * moveForward).normalized;

        // New: Add vertical movement (Space = up, Shift = down)
        float verticalInput = 0f;
        if (Input.GetKey(KeyCode.Space)) verticalInput += 5f;
        if (Input.GetKey(KeyCode.LeftShift)) verticalInput -= 5f;

        movement += Vector3.up * verticalInput;
        movement = movement.normalized;

        Vector3 targetVelocity = movement * MoveSpeed;

        // Apply movement to Rigidbody
        rb.linearVelocity = new Vector3(targetVelocity.x, targetVelocity.y, targetVelocity.z);

        // Optional: If no movement input, stop sliding
        if (moveHorizontal == 0 && moveForward == 0 && verticalInput == 0)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }


    void RotateCamera()
    {
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }


}
