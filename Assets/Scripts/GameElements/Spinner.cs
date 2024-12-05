using UnityEngine;
public class Spinner : MonoBehaviour
{
    [Tooltip("Degrees of rotation per second. Can be a negative value. Cannot be zero")] [SerializeField] private float minRotationSpeed = 10; 
    [Tooltip("Degrees of rotation per second. Can be a negative value. Cannot be zero")] [SerializeField] private float maxRotationSpeed = 35;
    [SerializeField] private float speedMultiplier = 3.5f; // Probably a temporary thing (lmao)
    
    private Rigidbody spinnerRb;
    private Vector3 rotationVector;
    private float trueRotationSpeed;
    
    private void Start()
    {
        if (minRotationSpeed == 0) minRotationSpeed = 10; // Reset to default
        if (maxRotationSpeed == 0) maxRotationSpeed = 35; // Again, reset to default
        
        trueRotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        rotationVector = new Vector3(0, trueRotationSpeed, 0);
        spinnerRb = GetComponentInChildren<Rigidbody>();
        
        // I do not like working with rotation math
        Vector3 eulerAngles = spinnerRb.gameObject.transform.rotation.eulerAngles;
        eulerAngles.y = Random.Range(-180f, 360f); // f lets the compiler know that floating point numbers are accepted; otherwise they would only pick full numbers between this range
        spinnerRb.gameObject.transform.rotation = Quaternion.Euler(eulerAngles);
    }

    private void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(rotationVector * (Time.fixedDeltaTime * speedMultiplier));
        spinnerRb.MoveRotation(spinnerRb.transform.rotation * deltaRotation);
    }
}
