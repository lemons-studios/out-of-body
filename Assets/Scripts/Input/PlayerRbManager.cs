using UnityEngine;

public class PlayerRbManager : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    private Rigidbody platformRigidbody;
    private Transform platformTransform;
    private Vector3 previousPlatformPosition;
    private Quaternion previousPlatformRotation;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.TryGetComponent(out Rigidbody rb)) return;
        platformRigidbody = rb;
        platformTransform = rb.transform;
        previousPlatformPosition = platformTransform.position;
        previousPlatformRotation = platformTransform.rotation;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (platformRigidbody == null) return;
        Vector3 platformDeltaPosition = platformTransform.position - previousPlatformPosition;
        Quaternion platformDeltaRotation = platformTransform.rotation * Quaternion.Inverse(previousPlatformRotation);

        Vector3 localPlayerPosition = platformTransform.InverseTransformPoint(playerRigidbody.position);
        localPlayerPosition = platformDeltaRotation * localPlayerPosition;
        playerRigidbody.position = platformTransform.TransformPoint(localPlayerPosition) + platformDeltaPosition;

        playerRigidbody.rotation = platformDeltaRotation * playerRigidbody.rotation;

        // Update cached values
        previousPlatformPosition = platformTransform.position;
        previousPlatformRotation = platformTransform.rotation;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody>() != platformRigidbody) return;
        platformRigidbody = null;
        platformTransform = null;
    }
}