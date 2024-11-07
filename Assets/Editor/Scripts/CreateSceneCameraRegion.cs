using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CreateSceneCameraRegion : MonoBehaviour
{
    [MenuItem("GameObject/Lemon Studios/Scene Camera Region")]
    public static void AddSceneCameraRegion()
    {
        GameObject parent = new GameObject($"SceneCameraRegion{getHighestSceneCameraRegionNumber()}")
        {
            tag = "SceneCameraRegion"
        };

        parent.AddComponent<BoxCollider>();
        parent.GetComponent<BoxCollider>().isTrigger = true;
        parent.AddComponent<SceneCameraRegion>();
        parent.GetComponent<SceneCameraRegion>().regionIndex = getHighestSceneCameraRegionNumber() - 1;
        
        GameObject child = new GameObject($"SceneCamera{getHighestSceneCameraRegionNumber() - 1}") // Absolutely no clue why this happens here and not for the parent
        {
            tag = "SceneCamera",
            transform =
            {
                parent = parent.transform 
            }
        }; 
        
        child.AddComponent<Camera>();
        child.AddComponent<UniversalAdditionalCameraData>();
        
        Camera childCamera = child.GetComponent<Camera>();
        childCamera.enabled = false;
        childCamera.nearClipPlane = 0.01f;
        childCamera.fieldOfView = 75;
        
        UniversalAdditionalCameraData urpCameraData = child.GetComponent<UniversalAdditionalCameraData>();
        urpCameraData.renderPostProcessing = true;
        urpCameraData.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
        
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneCameraControls>().sceneCameras.Add(childCamera);
    }

    private static int getHighestSceneCameraRegionNumber()
    {
        return GameObject.FindGameObjectsWithTag("SceneCameraRegion").Length;
    }
}
