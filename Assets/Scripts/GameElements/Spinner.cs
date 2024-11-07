using UnityEngine;
public class Spinner : MonoBehaviour
{
    [Tooltip("Degrees of rotation per second. Can be a negative value")]
    public float rotationSpeed;
    
    private Rigidbody spinnerRb;
    private Vector3 rotationVector;

    private void Start()
    {
        rotationVector = new Vector3(0, rotationSpeed, 0);
        spinnerRb = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(rotationVector * Time.fixedDeltaTime);
        spinnerRb.MoveRotation(spinnerRb.rotation * deltaRotation);
    }
}
