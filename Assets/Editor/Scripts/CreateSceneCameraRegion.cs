using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SceneCameraRegion : MonoBehaviour
{
    [MenuItem("GameObject/Lemon Studios/Scene Camera Region")]
    public static void CreateSceneCameraRegion()
    {
        GameObject parent = new GameObject($"SceneCameraRegion{getHighestSceneCameraRegionNumber()}");
        parent.tag = "SceneCameraRegion";
        
        parent.AddComponent<BoxCollider>();
        parent.GetComponent<BoxCollider>().isTrigger = true;

        GameObject child = new GameObject($"SceneCamera{getHighestSceneCameraRegionNumber() - 1}"); // Absolutely no clue why this happens here and not for the parent
        child.tag = "SceneCamera";
        child.transform.parent = parent.transform;
        child.AddComponent<Camera>();
        child.AddComponent<UniversalAdditionalCameraData>();


        Camera childCamera = child.GetComponent<Camera>();
        childCamera.enabled = false;
        childCamera.nearClipPlane = 0.01f;
        
        UniversalAdditionalCameraData urpCameraData = child.GetComponent<UniversalAdditionalCameraData>();
        urpCameraData.renderPostProcessing = true;
        urpCameraData.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
    }

    private static int getHighestSceneCameraRegionNumber()
    {
        return GameObject.FindGameObjectsWithTag("SceneCameraRegion").Length;
    }
}
