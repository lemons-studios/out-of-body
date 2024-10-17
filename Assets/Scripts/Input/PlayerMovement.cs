using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody playerRb;

    public float moveSpeed = 5;

    private bool enableDebug = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerRb = GetComponent<Rigidbody>();
        
        playerInput = new PlayerInput();
        playerInput.Game.Jump.performed += ctx => Jump();
        playerInput.Enable();
    }

    private void Update()
    {
        OnPlayerMove(playerInput.Game.Movement.ReadValue<Vector2>());
    }

    private void OnPlayerMove(Vector2 moveDelta)
    {
        Vector3 movementVector = transform.TransformDirection(new Vector3(moveDelta.x, 0, moveDelta.y));
        playerRb.AddForce(movementVector * moveSpeed, ForceMode.VelocityChange);
    }
    
    private void Jump()
    {
        playerRb.AddForce(Vector3.up * 2.5f, ForceMode.VelocityChange);
    }

    private void OnDestroy()
    {
        // Prevent scene load shenanigans involving the player controller
        playerInput.Disable();
    }
}
