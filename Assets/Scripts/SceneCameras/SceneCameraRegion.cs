using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SceneCameraRegion : MonoBehaviour
{ 
    
    private SceneCameraControls sceneCameraControls;
    public int regionIndex;
    private void Start()
    {
        sceneCameraControls = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneCameraControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("entered " + other.name);
        sceneCameraControls.switchSceneCameraView(regionIndex);
    }
}
