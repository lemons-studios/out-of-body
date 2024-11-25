using UnityEngine;
public class Spinner : MonoBehaviour
{
    [Tooltip("Degrees of rotation per second. Can be a negative value. Cannot be zero")]
    public float rotationSpeed = 5;

    private Rigidbody spinnerRb;
    private Vector3 rotationVector;
    private bool playerOnPlatform;
    
    private void Start()
    {
        rotationSpeed = Mathf.Max(rotationSpeed, 1);
        rotationVector = new Vector3(0, rotationSpeed, 0);
        spinnerRb = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(rotationVector * Time.fixedDeltaTime);
        spinnerRb.MoveRotation(spinnerRb.transform.rotation * deltaRotation);
    }
}
