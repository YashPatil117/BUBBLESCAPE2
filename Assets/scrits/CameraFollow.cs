using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset = new Vector3(0, 5, -7); 
    public float smoothSpeed = 0.125f; 

    void LateUpdate()
    {
        if (target != null)
        {
         
            Vector3 desiredPosition = target.position + offset;

            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            
            transform.LookAt(target);
        }
    }
}
