using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 2f;
    public float distance = 5f;
    public Vector3 offset = new Vector3(0, 1, 0);

    private float currentRotationX = 0f;
    private float currentRotationY = 0f; // Current rotation around Y-axis (left/right)

    void Update()
    {
        // Check if the target is null (destroyed or not assigned)
        if (target == null)
        {
            Debug.LogWarning("Target is null. Camera control will not update.");
            return; // Exit the method early if target is null
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y"); // Mouse vertical movement

        // Update the horizontal and vertical rotation based on mouse input
        currentRotationY += mouseX * rotationSpeed;
        currentRotationX -= mouseY * rotationSpeed;

        currentRotationX = Mathf.Clamp(currentRotationX, -40f, 80f);

        Quaternion rotation = Quaternion.Euler(currentRotationX, currentRotationY, 0);
        Vector3 direction = rotation * Vector3.back * distance;

        transform.position = target.position + offset + direction;

        transform.LookAt(target.position + offset);
    }
}
