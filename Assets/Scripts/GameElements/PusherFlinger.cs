using UnityEngine;

public class PusherFlinger : MonoBehaviour
{
    [SerializeField] private float targetAcceleration = 15f;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collided");
        var playerRb = other.gameObject.GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * targetAcceleration, ForceMode.Acceleration);
    }
}
