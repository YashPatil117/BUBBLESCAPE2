// using UnityEngine;

// public class Switch : MonoBehaviour
// {
//     public GameObject door;  // Reference to the door

//     void OnTriggerEnter(Collider other)
//     {
//         // Check if the player bubble is near the switch
//         if (other.CompareTag("Bubble"))
//         {
//             // Open the door when the player hits the switch after collecting all 4 bubbles
//             Door doorScript = door.GetComponent<Door>();
//             if (doorScript != null && doorScript.isOpen)
//             {
//                 Debug.Log("Switch activated! Door is open.");
//                 doorScript.OpenDoor();  // Open the door
//             }
//         }
//     }
// }
