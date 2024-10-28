using System;
using System.Collections;
using UnityEngine;

public class SceneCamera : MonoBehaviour
{
    
    public float minimumRotationAngle, maximumRotationAngle;
    
    private PlayerInput playerInput;
    private float previousDistance;
    
    private void Start()
    {
        playerInput = new PlayerInput();
        
        playerInput.Enable();
    }


    private void OnDisable()
    {
        playerInput.Disable();
    }
}
