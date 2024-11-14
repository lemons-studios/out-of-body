using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class SceneCameraRegion : MonoBehaviour
{ 
    private SceneCameraControls sceneCameraControls;
    
    [Tooltip("Make this the same index as the camera in this region. Or you'll have a bad time. Reminder that array index starts at 0")]
    public int regionIndex;
    
    private void Start()
    {
        sceneCameraControls = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneCameraControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sceneCameraControls.switchSceneCameraView(regionIndex);
            GameObject.FindGameObjectWithTag("GameUIRoot").GetComponent<GameUI>().setCameraText(regionIndex + 1);
        }
    }
}
