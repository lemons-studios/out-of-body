using UnityEngine;

public class LevelHazard : MonoBehaviour
{
    [SerializeField] private AudioClip fallSfx;
    private AudioSource sfxAudioSource;

    private void Start() { sfxAudioSource = GameObject.FindGameObjectWithTag("GlobalSfx").GetComponent<AudioSource>(); }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero; // Prevent scenarios where the player is moving so fast that movement calculations do not know where to place it, and it ends up clipping through the entire level
        GameObject.FindGameObjectWithTag("GameController").GetComponent<CheckpointAndAttempts>().OnPlayerFail(other.gameObject);
        sfxAudioSource.PlayOneShot(fallSfx);
    }
}