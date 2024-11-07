using System.Linq;
using UnityEngine;

public class CheckpointAndAttempts : MonoBehaviour
{
    private int attempts = 1;
    private GameObject[] checkpoints;
    private int currentCheckpoint = 0;
    private void Start()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpoints = checkpoints.OrderBy(checkpoint => checkpoint.GetComponent<Checkpoint>().checkpointIndex)
            .ToArray();
    }

    public void OnPlayerFail(GameObject player)
    {
        Debug.Log("Womp womp");
        player.transform.position = checkpoints[currentCheckpoint].transform.position;
        attempts++;
    }

    public void SetNewCheckpoint(int checkpointIndex)
    {
        Debug.Log($"New checkpoint set! {checkpointIndex}");
        currentCheckpoint = checkpointIndex;
    }

    public int GetAttempts()
    {
        return attempts;
    }

    private void SortCheckpointsByIndex()
    {
        for (int i = 0; i < checkpoints.Length; i++)
        {
            
        }
    }
}
