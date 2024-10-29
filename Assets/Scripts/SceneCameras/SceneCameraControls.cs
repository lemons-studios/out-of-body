using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneCameraControls : MonoBehaviour
{
    [Tooltip("Populate manually or you're in for a bad time")]
    public List<Camera> sceneCameras;

    private int getActivePlayerCamera()
    {
        // not using a proper variable name since I couldn't find a good thing to call this
        int x = 0; 
        for (int i = 0; i < sceneCameras.Count; i++)
        {
            if (sceneCameras[i].enabled)
            {
                x = i;
                break;
            }
        }
        return x;
    }
    
    public void switchSceneCameraView(int regionEnable)
    {
        if (getActivePlayerCamera() == regionEnable) return;

        sceneCameras[getActivePlayerCamera()].enabled = false;
        sceneCameras[regionEnable].enabled = true;
    }
}
