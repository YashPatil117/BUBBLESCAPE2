using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public int bubblesRequired = 10;
    public float slideDistance = -0.01862f;
    public float slideDuration = 1.5f;

    [Header("References")]
    public Transform doorTransform;
    
    [Tooltip("Debugging - visible in Play Mode")]
    public int collectedBubbles = 0;
    public bool isOpen = false;

    private Vector3 closedPosition;

    void Start()
    {
        if (doorTransform == null)
            doorTransform = transform;
        
        closedPosition = doorTransform.position;
        Debug.Log($"Door ready. Needs {bubblesRequired} bubbles.");
    }

    public void BubbleCollected()
    {
        collectedBubbles++;
        Debug.Log($"Bubble {collectedBubbles}/{bubblesRequired} collected");
        
        if (collectedBubbles >= bubblesRequired && !isOpen)
        {
            StartCoroutine(SlideDoor());
        }
    }

    IEnumerator SlideDoor()
    {
        isOpen = true;
        Vector3 openPosition = closedPosition + doorTransform.forward * slideDistance;
        float elapsed = 0f;

        while (elapsed < slideDuration)
        {
            doorTransform.position = Vector3.Lerp(
                closedPosition, 
                openPosition, 
                elapsed/slideDuration
            );
            elapsed += Time.deltaTime;
            yield return null;
        }

        doorTransform.position = openPosition;
        Debug.Log("Door fully opened!");
    }

    // Optional: Visualize slide path in Editor
    void OnDrawGizmosSelected()
    {
        if (doorTransform == null) return;
        
        Gizmos.color = Color.green;
        Vector3 endPos = doorTransform.position + doorTransform.forward * slideDistance;
        Gizmos.DrawLine(doorTransform.position, endPos);
    }
}