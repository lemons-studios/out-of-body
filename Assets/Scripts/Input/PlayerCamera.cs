using System.Collections;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float zoomFactor;
    
    private Camera playerCamera;
    private Coroutine zoomRoutine;
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
            if(zoomAmount != 0) zoomCameraHelper(zoomAmount);
        }
    }


    private void onPlayerLook(Vector2 mouseDelta)
    {
        
    }

    private void zoomCameraHelper(float zoomDelta)
    {
        float fovTarget = Mathf.Clamp(playerCamera.fieldOfView + zoomDelta, fieldOfViewClamp[0], fieldOfViewClamp[1]);
        if (zoomRoutine != null)
        {
            StopCoroutine(zoomRoutine);
        }
        zoomRoutine = StartCoroutine(cameraZoomSmooth(fovTarget));
    }

    private IEnumerator cameraZoomSmooth(float fovTarget, float duration = 0.2f)
    {
        float startFov = playerCamera.fieldOfView;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            // Using SmoothStep for a smoother transition
            float currentFieldOfView = Mathf.Lerp(startFov, fovTarget, Mathf.SmoothStep(0f, 1f, t));
            playerCamera.fieldOfView = currentFieldOfView;
            yield return null;
        }
    }
}
