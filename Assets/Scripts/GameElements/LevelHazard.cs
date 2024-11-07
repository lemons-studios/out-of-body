using UnityEngine;

public class LevelHazard : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CheckpointAndAttempts>().OnPlayerFail(other.gameObject);
        }
    }
}