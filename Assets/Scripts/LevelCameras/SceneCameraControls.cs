using System.Collections.Generic;
using UnityEngine;

public class SceneCameraControls : MonoBehaviour
{
    [Tooltip("Populate manually or you're in for a bad time")]
    public List<Camera> sceneCameras = new();

    public int getActivePlayerCamera()
    {
        int x = 0;
        for (int i = 0; i < sceneCameras.Count; i++)
        {
            if (sceneCameras[i].enabled) x = i;
        }
        return x;
    }

    public void switchSceneCameraView(int regionDisable, int regionEnable)
    {
        sceneCameras[regionDisable].enabled = false;
        sceneCameras[regionEnable].enabled = true;
    }
    
}
