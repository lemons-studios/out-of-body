using UnityEngine;

// Partial help from the robot on this one (I still wrote most of this script)
public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody playerRb;
    private GameObject jumpDetectorFirePoint;
    private Vector3 currentVelocity;
    
    public float acceleration = 10f;
    public float deceleration = 10f;
    
    public float moveSpeed = 5;
    public float jumpForce = 10;
    public float sprintMultiplier = 2f;
    


    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        jumpDetectorFirePoint = GameObject.FindGameObjectWithTag("PlayerJumpDetector");
        playerRb = GetComponent<Rigidbody>();
        
        // Action bindings
        playerInput = new PlayerInput();
        playerInput.Game.Jump.performed += ctx => Jump();
        playerInput.Game.Sprint.started += ctx => onSprintToggled(true);
        playerInput.Game.Sprint.canceled += ctx => onSprintToggled(false);
        playerInput.Enable();
    }

    private void FixedUpdate()
    {
        if (playerRb)
        {
            Vector2 movementInput = playerInput.Game.Movement.ReadValue<Vector2>();
            Vector3 targetVelocity = Vector3.zero;

            if (movementInput != Vector2.zero)
            {
                targetVelocity = transform.TransformDirection(new Vector3(movementInput.x, 0, movementInput.y)) * moveSpeed;
            }

            // Smoothly interpolate to the target velocity
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, (targetVelocity.magnitude > 0 ? acceleration : deceleration) * Time.fixedDeltaTime);
            currentVelocity.y = playerRb.velocity.y; // Preserve vertical velocity

            playerRb.velocity = currentVelocity;
        }
    }
    
    private void Jump()
    {
        if (isPlayerGrounded())
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void onSprintToggled(bool started)
    {
        moveSpeed *= started ? sprintMultiplier : 1f / sprintMultiplier; // Genuine genius from the robot from this one too, I should absolutely get better at math
    }
    
    private bool isPlayerGrounded()
    {
        // Quick and easy check
        return Physics.Raycast(jumpDetectorFirePoint.transform.position, transform.TransformDirection(Vector3.down), 0.1f);
    }
    
    private void OnDestroy()
    {
        // Prevent scene load shenanigans involving the player controller (I'm serious when I say I lost sleep due to this simple issue)
        playerInput.Disable();
    }
}