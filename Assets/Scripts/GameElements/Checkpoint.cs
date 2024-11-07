using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    public int checkpointIndex;
    
    private CheckpointAndAttempts sceneCheckpoints;
    private bool isFirstTrigger = true;
    
    private void Start()
    {
        sceneCheckpoints = GameObject.FindGameObjectWithTag("GameController").GetComponent<CheckpointAndAttempts>();
        if(!GetComponent<BoxCollider>().isTrigger) Debug.LogError($"{gameObject.name}'s trigger is not enabled. please enable it in order for it's checkpoint to work properly");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFirstTrigger)
        {
            sceneCheckpoints.SetNewCheckpoint(checkpointIndex);
        }
        isFirstTrigger = false;
    }
}
