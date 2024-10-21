using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float zoomFactor;
    
    private Camera playerCamera;
    private PlayerInput playerInput;
    private Transform playerModel;
    
    private readonly float[] fieldOfViewClamp = { 60, 150 };
    
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
            
            // Use negative value to make the scroll/zoom direction correct
            float zoomAmount = -playerInput.Game.Zoom.ReadValue<Vector2>().y / zoomFactor; 
            if(zoomAmount != 0) zoomCamera(zoomAmount);
        }
    }


    private void onPlayerLook(Vector2 mouseDelta)
    {
        
    }

    private void zoomCamera(float zoomDelta)
    {
        // TODO: Make zoom smooth
        playerCamera.fieldOfView = Mathf.Clamp(playerCamera.fieldOfView + zoomDelta, fieldOfViewClamp[0], fieldOfViewClamp[1]);
    }
}
