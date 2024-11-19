using UnityEngine;

public class LevelHazard : MonoBehaviour
{
    [SerializeField] private AudioClip fallSfx;
    private AudioSource sfxAudioSource;

    private void Start()
    {
        sfxAudioSource = GameObject.FindGameObjectWithTag("GlobalSfx").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<CheckpointAndAttempts>().OnPlayerFail(other.gameObject);
            sfxAudioSource.PlayOneShot(fallSfx);
        }
    }
}