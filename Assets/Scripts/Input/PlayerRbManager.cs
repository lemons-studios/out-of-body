using UnityEngine;

public class PlayerRbManager : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Rigidbody platformRigidbody; // The moving platform's rigidbody
    private Vector3 previousPlatformPosition; // To track platform movement

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a Rigidbody (e.g., moving platform)
        if (collision.gameObject.GetComponent<Rigidbody>() != null)
        {
            platformRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            previousPlatformPosition = platformRigidbody.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // Continue syncing player position with the platform
        if (platformRigidbody != null)
        {
            Vector3 platformDelta = platformRigidbody.position - previousPlatformPosition;
            playerRigidbody.position += platformDelta; // Apply platform movement to the player
            previousPlatformPosition = platformRigidbody.position; // Update platform position
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Stop syncing when the player leaves the platform
        if (collision.gameObject.GetComponent<Rigidbody>() == platformRigidbody)
        {
            platformRigidbody = null;
        }
    }
}