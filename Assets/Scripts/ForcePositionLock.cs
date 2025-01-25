using UnityEngine;

public class ForcePositionLock : MonoBehaviour
{
    [Header("Position Locks")]
    public bool lockX = false;
    public bool lockY = false;
    public bool lockZ = false;

    [Header("Rotation Locks")]
    public bool lockRotationX = false;
    public bool lockRotationY = false;
    public bool lockRotationZ = false;

    [Header("Position Constraints")]
    public bool usePositionConstraints = false;
    public Vector3 minPosition = new Vector3(-10f, -10f, -10f);
    public Vector3 maxPosition = new Vector3(10f, 10f, 10f);

    private Vector3 originalPosition;
    private Vector3 originalRotation;

    void Start()
    {
        // Store initial values
        originalPosition = transform.position;
        originalRotation = transform.eulerAngles;
    }

    void LateUpdate()
    {
        Vector3 currentPosition = transform.position;
        Vector3 currentRotation = transform.eulerAngles;

        // Handle position locks
        if (lockX) currentPosition.x = originalPosition.x;
        if (lockY) currentPosition.y = originalPosition.y;
        if (lockZ) currentPosition.z = originalPosition.z;

        // Handle rotation locks
        if (lockRotationX) currentRotation.x = originalRotation.x;
        if (lockRotationY) currentRotation.y = originalRotation.y;
        if (lockRotationZ) currentRotation.z = originalRotation.z;

        // Apply position constraints if enabled
        if (usePositionConstraints)
        {
            currentPosition.x = Mathf.Clamp(currentPosition.x, minPosition.x, maxPosition.x);
            currentPosition.y = Mathf.Clamp(currentPosition.y, minPosition.y, maxPosition.y);
            currentPosition.z = Mathf.Clamp(currentPosition.z, minPosition.z, maxPosition.z);
        }

        // Apply the modified positions and rotations
        transform.position = currentPosition;
        transform.eulerAngles = currentRotation;
    }
}
