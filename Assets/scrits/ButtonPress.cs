using UnityEngine;

public class ButtonPress : MonoBehaviour
{
    public GameObject door; 
    public float requiredSize = 2f;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bubble"))
        {
            if (other.transform.localScale.x >= requiredSize)
            {
                Destroy(door); 
                Debug.Log("Door opened!");
            }
            else
            {
                Debug.Log("Bubble is too small!");
            }
        }
    }
}
