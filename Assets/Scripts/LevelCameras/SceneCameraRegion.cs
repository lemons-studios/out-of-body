using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCameraRegion : MonoBehaviour
{
    public int sceneRegionIndex = 0;
    private SceneCameraControls sceneCameraControls;
    private Camera sceneRegionCamera;
    
    private void Start()
    {
        sceneCameraControls = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneCameraControls>();
        sceneRegionCamera = sceneCameraControls.sceneCameras[sceneRegionIndex];
    }

    private void OnCollisionEnter(Collision sceneCameraRegion)
    {
        Debug.Log("collided");
       sceneCameraControls.switchSceneCameraView(sceneCameraControls.getActivePlayerCamera(), sceneRegionIndex);
    }
}
