using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody playerRb;
    private GameObject jumpDetectorFirePoint;

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
            if (movementInput != Vector2.zero)
            {
                // Only call OnPlayerMove() when player input is received, which is slightly more efficient
                OnPlayerMove(movementInput);
            }
        }
        
        // Prevent skidding/sliding after movement by reducing the velocity of the player GameObject
        float[] currentPlayerVelocity = { playerRb.velocity.x, playerRb.velocity.z };
        for (int i = 0; i < 2; i++)
        {
            currentPlayerVelocity[i] *= Time.deltaTime;
        }
        playerRb.velocity = new Vector3(currentPlayerVelocity[0], playerRb.velocity.y, currentPlayerVelocity[1]);
    }

    private void OnPlayerMove(Vector2 moveDelta)
    {
        Vector3 movementVector = transform.TransformDirection(new Vector3(moveDelta.x, 0, moveDelta.y));
        playerRb.AddForce(movementVector * moveSpeed, ForceMode.VelocityChange);
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
        switch (started)
        {
            case true:
                moveSpeed *= sprintMultiplier;
                break;
            case false:
                moveSpeed /= sprintMultiplier;
                break;
        }
    }
    
    private bool isPlayerGrounded()
    {
        // Quick and easy check
        return Physics.Raycast(jumpDetectorFirePoint.transform.position, transform.TransformDirection(Vector3.down), 0.1f);
    }
    
    private void OnDestroy()
    {
        // Prevent scene load shenanigans involving the player controller
        playerInput.Disable();
    }

}
