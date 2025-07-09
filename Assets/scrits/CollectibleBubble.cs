using UnityEngine;

public class CollectibleBubble : MonoBehaviour
{
    public SlidingDoor doorToOpen; // Assign in inspector
    public float sizeIncreaseAmount = 0.1f;
    public ParticleSystem collectEffect; // Optional visual effect

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BubbleMovement player = other.GetComponent<BubbleMovement>();
            if (player != null)
            {
                // Play collection effect if available
                if (collectEffect != null)
                {
                    Instantiate(collectEffect, transform.position, Quaternion.identity);
                }

                // Increase player size
                player.IncreaseSize(sizeIncreaseAmount);
                
                // Notify door
                if (doorToOpen != null)
                {
                    doorToOpen.BubbleCollected();
                }

                Destroy(gameObject);
            }
        }
    }
}