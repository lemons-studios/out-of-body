using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Transform playerModel;
    private Camera playerCamera;
    public float zoomFactor;
    public float maximumFieldOfView = 150;
    public float minimumFieldOfView = 60;
    
    private float mouseSensitivity = 5f;
    private PlayerInput playerInput;
    private void Start()
    {
        playerModel = GameObject.FindGameObjectWithTag("Player").transform;
        playerCamera = playerModel.gameObject.GetComponentInChildren<Camera>();
        playerInput = new PlayerInput();
        playerInput.Enable();
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            Vector2 playerLook = playerInput.Game.Look.ReadValue<Vector2>();
            if(playerLook != Vector2.zero) onPlayerLook(playerLook);

            Vector2 cameraZoom = playerInput.Game.Zoom.ReadValue<Vector2>();
            float zoomAmount = -cameraZoom.y / zoomFactor;
            
            if(zoomAmount != 0) zoomCamera(zoomAmount);
        }
    }


    private void onPlayerLook(Vector2 mouseDelta)
    {
        
    }

    private void zoomCamera(float zoomDelta)
    {
        playerCamera.fieldOfView = Mathf.Clamp(playerCamera.fieldOfView + zoomDelta, minimumFieldOfView, maximumFieldOfView);
    }
}
