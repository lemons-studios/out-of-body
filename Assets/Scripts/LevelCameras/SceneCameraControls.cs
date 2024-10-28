using UnityEngine;

public class SceneCameraControls : MonoBehaviour
{
    public Camera[] sceneCameras;
    private void Start()
    {
        sceneCameras = findAllSceneCameras("PlayerViewCamera");
    }

    public int getCurrentPlayerCamera()
    {
        int x = 0;
        for (int i = 0; i < sceneCameras.Length; i++)
        {
            if (sceneCameras[i].enabled) x = i;
        }
        return x;
    }
    
    private Camera[] findAllSceneCameras(string tagForSearch)
    {
        GameObject[] cameraGameObjects = GameObject.FindGameObjectsWithTag(tagForSearch);
        Camera[] intermediateSceneCameras = {};

        for (int i = 0; i < cameraGameObjects.Length; i++)
        {
            Camera currentCamera = cameraGameObjects[i].GetComponent<Camera>();
            if (currentCamera != null)
            {
                intermediateSceneCameras[i] = currentCamera;
            }
        }
        return intermediateSceneCameras;
    }
}
