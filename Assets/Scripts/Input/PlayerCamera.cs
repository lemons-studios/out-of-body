using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform playerModel;
    public float maximumAngle, minimumAngle;
    
    private float mouseSensitivity = 5f;
    private PlayerInput playerInput;
    private void Start()
    {
        playerModel = GameObject.FindGameObjectWithTag("Player").transform;
        
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            onPlayerLook(playerInput.Game.Look.ReadValue<Vector2>());
        }
    }


    private void onPlayerLook(Vector2 mouseDelta)
    {
        float cameraX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float cameraY = -mouseDelta.y * mouseSensitivity * Time.deltaTime;
    }
}
