using UnityEngine;
using System.Collections; // Needed for IEnumerator

public class SlidingDoor : MonoBehaviour
{
    [Header("Door Settings")]
    public int bubblesRequired = 3;
    public float slideDistance = 3f;
    public float slideDuration = 2f;

    [Header("References")]
    public Transform doorTransform; // Drag your door visual here

    private Vector3 closedPosition;
    private int collectedBubbles;
    private bool isOpen;

    void Start()
    {
        if (doorTransform == null)
            doorTransform = transform;

        closedPosition = doorTransform.position;
    }

    public void BubbleCollected()
    {
        collectedBubbles++;
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
            doorTransform.position = Vector3.Lerp(closedPosition, openPosition, elapsed/slideDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        doorTransform.position = openPosition;
    }
}