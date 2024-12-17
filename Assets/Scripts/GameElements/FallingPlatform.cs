using System.Collections;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[SuppressMessage("ReSharper", "BitwiseOperatorOnEnumWithoutFlags")]
public class FallingPlatform : MonoBehaviour
{
    public float timeBeforeFall = 2.5f;
    public float platformFallPreventDistance = 5f;
    
    private AudioSource sfxSource;
    [SerializeField] private AudioClip platformFallSoundEffect;
    private GameObject Player;
    private Vector3 originalPosition;
    private Rigidbody platformRb;
    private Collider platformCollider;
    private Coroutine fallWaitRoutine;
    
    private bool isFalling;
    
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        platformCollider = GetComponent<Collider>();
        originalPosition = gameObject.transform.position;
        platformRb = GetComponent<Rigidbody>();
        sfxSource = GameObject.FindGameObjectWithTag("GlobalSfx").GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (IsPlayer(other.gameObject) && fallWaitRoutine == null)
        {
            fallWaitRoutine = StartCoroutine(FallRoutine());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (PlayerInRange() || isFalling || !IsPlayer(other.gameObject)) return;
        StopCoroutine(fallWaitRoutine);
        fallWaitRoutine = null;
        sfxSource.Stop();
    }

    private IEnumerator FallRoutine()
    {
        if (sfxSource.isPlaying) sfxSource.Stop();
        sfxSource.PlayOneShot(platformFallSoundEffect);
        
        yield return new WaitForSeconds(timeBeforeFall);
        if (!PlayerInRange()) 
        {
            isFalling = true;
            platformRb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            platformCollider.enabled = false;
            yield return new WaitForSeconds(3.5f); // Wait for platform to raise up again
            platformRb.constraints |= RigidbodyConstraints.FreezePositionY;
            isFalling = false;
            gameObject.transform.position = originalPosition;
            platformCollider.enabled = true;
        }
        
        fallWaitRoutine = null; // unset fallWaitRoutine after coroutine finishes execution
    }

    private bool PlayerInRange()
    {
        return Vector3.Distance(Player.transform.position, originalPosition) > platformFallPreventDistance;
    }

    private bool IsPlayer(GameObject objectCheck)
    {
        return objectCheck.CompareTag("Player");
    }
}
