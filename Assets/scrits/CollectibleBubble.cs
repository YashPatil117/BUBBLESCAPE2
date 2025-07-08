using UnityEngine;

public class CollectibleBubble : MonoBehaviour
{
    public SlidingDoor doorToOpen; // Assign this in the inspector
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BubbleMovement bubbleMovement = other.GetComponent<BubbleMovement>();
            if (bubbleMovement != null)
            {
                Debug.Log("Bubble collected!");
                
                // Notify the door that a bubble was collected
                if (doorToOpen != null)
                {
                    doorToOpen.BubbleCollected();
                }
            }

            Destroy(gameObject);
        }
    }
}

