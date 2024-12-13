using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Checkpoint : MonoBehaviour
{
    public int checkpointIndex;
    
    private CheckpointAndAttempts sceneCheckpoints;
    [SerializeField] private AudioClip checkpointReachedSoundEffect;
    private bool isFirstTrigger = true;
    
    private void Start()
    {
        sceneCheckpoints = GameObject.FindGameObjectWithTag("GameController").GetComponent<CheckpointAndAttempts>();
        if(!GetComponent<Collider>().isTrigger) Debug.LogError($"{gameObject.name}'s trigger is not enabled. please enable it in order for it's checkpoint to work properly");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFirstTrigger && other.gameObject.CompareTag("Player"))
        {
            sceneCheckpoints.SetNewCheckpoint(checkpointIndex);
            GameObject.FindGameObjectWithTag("GlobalSfx").GetComponent<AudioSource>().PlayOneShot(checkpointReachedSoundEffect);
        }
        isFirstTrigger = false;
    }
}
