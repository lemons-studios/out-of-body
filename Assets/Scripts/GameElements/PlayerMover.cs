using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Tooltip("Value is in meters per second per second. Just like real life")]
    [SerializeField] private float acceleration = 5f;
    [Tooltip("0. Up, 1. Down, 2. Forward, 3. Backward, 4. Left, 5. Right. Any other values default to forward")]
    [SerializeField] private int MoveDirection = 2;
    [Tooltip("0. Force, 1. Impulse, 2. Velocity Change, 5. Acceleration")]
    [SerializeField] private int forceMode = 5;
    
    private bool playerInMover;
    private Rigidbody playerRb;
    
    private void Start()
    {
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        if (forceMode < 2 && forceMode != 5)
        {
            forceMode = 0; // Fallback
        }
    }

    private void FixedUpdate()
    {
        if (playerInMover)
        {
            playerRb.AddForce(GetMoveDirection() * (playerRb.mass * acceleration), (ForceMode) forceMode); // I love forces formulas!!!!! F = ma is my goat!!!
        }
    }

    private Vector3 GetMoveDirection()
    {
        return MoveDirection switch
        {
            0 => Vector3.up,
            1 => Vector3.down,
            2 => Vector3.forward,
            3 => Vector3.back,
            4 => Vector3.left,
            5 => Vector3.right,
            _ => Vector3.up
        };
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInMover = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInMover = false;
        }
    }
}
