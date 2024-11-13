using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingPlatform : MonoBehaviour
{
    public float timeBeforeFall = 2.5f;
    public float platformFallPreventDistance = 5f;
    
    private GameObject Player;
    private Vector3 originalPosition;
    private Rigidbody platformRb;
    private Coroutine fallWaitRoutine;
    private bool isFalling;
    
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        originalPosition = gameObject.transform.position;
        platformRb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (IsPlayer(other.gameObject) && fallWaitRoutine == null)
        {
            fallWaitRoutine = StartCoroutine(WaitForFall());
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (!IsPlayerAwayFromPlatform() && !isFalling && IsPlayer(other.gameObject))
        {
            StopCoroutine(fallWaitRoutine);
            fallWaitRoutine = null;
        }
    }

    private IEnumerator WaitForFall()
    {
        yield return new WaitForSeconds(timeBeforeFall);
        if (!IsPlayerAwayFromPlatform())
        {
            isFalling = true;
            platformRb.constraints &= ~RigidbodyConstraints.FreezePositionY;
            yield return new WaitForSeconds(3.5f); // Wait for platform to raise up again
            platformRb.constraints |= RigidbodyConstraints.FreezePositionY;
            
            gameObject.transform.localPosition = originalPosition;
            isFalling = false;
        }
        
        fallWaitRoutine = null; // unset fallWaitRoutine after coroutine finishes execution
    }

    private bool IsPlayerAwayFromPlatform()
    {
        return Vector3.Distance(Player.transform.position, originalPosition) > platformFallPreventDistance;
    }

    private bool IsPlayer(GameObject objectCheck)
    {
        return objectCheck.CompareTag("Player");
    }
}
