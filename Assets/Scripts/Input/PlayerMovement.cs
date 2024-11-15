using System;
using System.Collections;
using UnityEngine;

// Partial help from the robot on this one (I still wrote most of this script)
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    public LayerMask groundLayer;
    
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float rotateSpeed = 0.75f;
    [Space]
    public float moveSpeed = 5;
    public float jumpForce = 10;
    public float sprintMultiplier = 2f;
    [Space] 
    public float rotateDelay = 0.25f;

    private AudioSource rotationSource;
    private Coroutine rotatePlayerRoutine, rotateHelperRoutine;
    private PlayerInput playerInput;
    private Rigidbody playerRb;
    [SerializeField] 
    private Transform groundCheck;
    private Vector3 currentVelocity;
    
    [SerializeField] 
    private float groundDistance = 0.1f;
    private float amountRotated;
    
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        groundCheck = GameObject.FindGameObjectWithTag("PlayerJumpDetector").transform;
        rotationSource = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        
        
        // Action bindings
        playerInput = new PlayerInput();
        playerInput.Game.Jump.performed += _ => Jump();
        playerInput.Game.Sprint.started += _ => onSprintToggled(true);
        playerInput.Game.Sprint.canceled += _ => onSprintToggled(false);
        playerInput.Game.RotateLeft.started += _ => onRotateStart(true);
        playerInput.Game.RotateRight.started += _ => onRotateStart(false);
        playerInput.Game.RotateLeft.canceled += _ => onRotateEnd();
        playerInput.Game.RotateRight.canceled += _ => onRotateEnd();
        playerInput.Enable();
    }

    private void FixedUpdate()
    {
        
        if (playerRb)
        {
            // ReSharper disable once Unity.PerformanceCriticalCodeInvocation (Resource is expensive but it's required for movement so it's ok)
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
        if (IsPlayerGrounded())
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void onSprintToggled(bool started)
    {
        moveSpeed *= started ? sprintMultiplier : 1f / sprintMultiplier; // Genuine genius from the robot here, I should absolutely get better at math
    }

    // Long chain of bool carrying, maybe I should just assign it to a class bool
    private void onRotateStart(bool isClockwise)
    {
        // Enable loop on the rotationSource audio source when player starts rotating
        rotationSource.loop = true;
        rotationSource.Play();
        
        if (rotateHelperRoutine != null)
        {
            StopCoroutine(rotateHelperRoutine);
            rotateHelperRoutine = null;
        }
        rotateHelperRoutine = StartCoroutine(rotatePlayerHelper(isClockwise));
    }

    private void onRotateEnd()
    { 
       // Disable loop on the rotationSource audio source when player stops rotating
       try
       {
           rotationSource.loop = false;
           rotationSource.Stop();
       
           StopCoroutine(rotateHelperRoutine);
           rotateHelperRoutine = null;
       }
       catch (NullReferenceException){} // Expected to happen sometimes if the user tries to rotate too fast
    }
    
    private IEnumerator rotatePlayerHelper(bool isClockwise)
    {
        while (true)
        {
            if (rotatePlayerRoutine != null)
            {
                StopCoroutine(rotatePlayerRoutine);
            }

            rotatePlayerRoutine = StartCoroutine(rotatePlayer(isClockwise));
            yield return new WaitForSeconds(rotateDelay); 
        }
    }

    private IEnumerator rotatePlayer(bool isClockwise, float duration = 0.015f)
    {
        Transform playerTransform = gameObject.transform;
        float startRotation = playerTransform.eulerAngles.y;
        float targetRotation = startRotation + (isClockwise ? -rotateSpeed : rotateSpeed);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            float intermediateRotation = Mathf.LerpAngle(startRotation, targetRotation, Mathf.SmoothStep(0f, 1f, t));
            
            gameObject.transform.rotation = Quaternion.Euler(playerTransform.rotation.x, intermediateRotation, playerTransform.rotation.z);
            amountRotated += (float) Math.Round(gameObject.transform.rotation.y, 5);
            yield return null;
        }
        
        // Adjust to final rotation to prevent rounding errors with floats
        gameObject.transform.rotation = Quaternion.Euler(0, targetRotation, 0);
    }

    private bool IsPlayerGrounded()
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundLayer);
    }
    
    public float GetAmountRotated()
    {
        return amountRotated;
    }
    
    private void OnDestroy()
    {
        // Prevent scene load shenanigans involving the player controller (I'm serious when I say I lost sleep due to this simple issue)
        playerInput.Disable();
    }
}
