using System;
using System.Collections;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    public GameObject[] movePositions;
    [Space]
    public float moveTime = 2.5f;
    public float waitTime = 1.5f;
    
    private int currentPosition = 0;

    private void Start()
    {
        if (movePositions == null || movePositions.Length == 0)
        {
            Debug.LogError("Move positions array is not set or empty.");
            return;
        }

        currentPosition = 0;  // Ensure the starting position is initialized
        StartCoroutine(PusherMoveHelper());
    }

    private IEnumerator PusherMoveHelper()
    {
        while (true)
        {
            yield return StartCoroutine(MovePusher());
            yield return new WaitForSeconds(waitTime);
            currentPosition = GetNextPosition();
        }
    }

    private IEnumerator MovePusher()
    {
        float elapsed = 0;
        Vector3 objectPosition = transform.position;
        Vector3 targetPosition = movePositions[currentPosition].transform.position;

        while (elapsed < moveTime)
        {
            elapsed += Time.deltaTime;
            float time = Mathf.SmoothStep(0, 1, elapsed / moveTime);
            transform.position = Vector3.Lerp(objectPosition, targetPosition, time);
            yield return null;
        }

        // Ensure the final position is exactly the target
        transform.position = targetPosition;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRb = other.gameObject.GetComponent<Rigidbody>();
            playerRb.AddForce(15.5f * Vector3.up , ForceMode.Acceleration);
        }
    }


    private int GetNextPosition()
    {
        return (currentPosition >= movePositions.Length - 1) ? 0 : currentPosition + 1;
    }
}