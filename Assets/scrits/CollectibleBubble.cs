using UnityEngine;

public class CollectibleBubble : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the bubble that collided has the BubbleMovement component
        if (other.CompareTag("Player"))  // Assuming the player bubble has the "Player" tag
        {
            // Get the BubbleMovement script on the player bubble and increase size
            BubbleMovement bubbleMovement = other.GetComponent<BubbleMovement>();
            if (bubbleMovement != null)
            {
                // This is the trigger for increasing size, already handled in BubbleMovement.cs
                Debug.Log("Bubble collected!");
            }

            // Destroy this collectible bubble
            Destroy(gameObject);
        }
    }
}
