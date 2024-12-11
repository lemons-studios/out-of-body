using System;
using UnityEngine;

public class PusherFlinger : MonoBehaviour
{
    [SerializeField] private float targetAcceleration = 15f;
    private void OnCollisionStay(Collision other)
    {
        Pusher localPusher = GetComponentInParent<Pusher>();
        if (!other.collider.CompareTag("Player") || !localPusher.isExtending) return;
        var playerRb = other.gameObject.GetComponent<Rigidbody>();
        float playerMass = playerRb.mass;
        playerRb.AddForce(Vector3.up * (playerMass * targetAcceleration), ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision other)
    {
        OnCollisionStay(other);
    }
}
