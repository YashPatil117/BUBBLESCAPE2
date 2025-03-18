using UnityEngine;

public class Door : MonoBehaviour
{
    public string triggeringTag = "KeyCube"; // Tag of the object (cube) that will trigger the door to open

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the specified tag
        if (other.CompareTag(triggeringTag))
        {
            Debug.Log("Gate opened!");
            Destroy(gameObject); // Destroy the door GameObject
        }
    }
}
