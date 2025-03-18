using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;       // Speed of the bubble's movement
    public float floatForce = 5f;      // Upward force when floating
    public float fallSpeed = 2f;       // Downward force when falling

    private Rigidbody rb;
    private Camera mainCamera;         // Reference to the camera

    private float bubbleSize = 1f;     // Initial size of the bubble
    private int collectedBubbles = 0; // Counter for collected bubbles
    private int requiredBubbles = 4;  // Total bubbles to collect before activating the switch

    public GameObject door;            // Reference to the door object to open
    public GameObject switchObject;    // Reference to the switch object that can be activated
    private bool canActivateSwitch = false; // Flag to determine if the switch can be used

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Get Rigidbody
        mainCamera = Camera.main;        // Get main camera
        rb.freezeRotation = true;        // Prevent rotation of the bubble
        transform.localScale = Vector3.one * bubbleSize;  // Initialize bubble size
        if (switchObject != null)
        {
            switchObject.SetActive(false); // Hide the switch initially
        }
    }

    void Update()
    {
        MoveBubble();   // Move the bubble with WASD keys
        FloatOrFall();  // Make the bubble float or fall based on spacebar

        // Open the door when near the switch and press 'E'
        if (canActivateSwitch && Input.GetKeyDown(KeyCode.E))
        {
            OpenDoor();
        }
    }

    void MoveBubble()
    {
        // Get horizontal and vertical movement inputs (WASD or Arrow keys)
        float horizontal = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float vertical = Input.GetAxis("Vertical");     // W/S or Up/Down Arrow

        // Calculate movement direction relative to the camera's facing direction
        Vector3 direction = (mainCamera.transform.forward * vertical) + (mainCamera.transform.right * horizontal);
        direction.y = 0; // Prevent any unwanted vertical movement

        // Apply the movement force towards the target position
        rb.AddForce(direction.normalized * moveSpeed, ForceMode.Acceleration);
    }

    void FloatOrFall()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            // Make the bubble float upwards when space is held
            rb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
        }
        else 
        {
            // The bubble falls when space is not held
            rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the bubble collides with a collectible bubble
        if (other.CompareTag("CollectibleBubble"))
        {
            collectedBubbles++; // Increment the collectible count
            bubbleSize += 0.2f; // Increase bubble size by a small amount
            transform.localScale = Vector3.one * bubbleSize; // Update bubble's scale
            Destroy(other.gameObject); // Destroy the collectible bubble

            Debug.Log("Collected a bubble! Total: " + collectedBubbles);

            // Activate the switch when all required bubbles are collected
            if (collectedBubbles >= requiredBubbles)
            {
                ActivateSwitch();
            }
        }

        // Check if the bubble collides with a surface (game over logic)
        if (other.CompareTag("Surface"))
        {
            Debug.Log("Bubble popped! Game over.");
            Destroy(gameObject); // Bubble pops on collision

            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver(); // Trigger game over logic
            }
            else
            {
                Debug.LogWarning("GameManager instance is null!");
            }
        }

        // Check if the bubble collides with the switch
        if (other.CompareTag("Switch"))
        {
            if (collectedBubbles >= requiredBubbles)
            {
                Debug.Log("Press 'E' to open the door!");
                canActivateSwitch = true; // Allow the player to open the door
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Disable the ability to activate the switch when leaving its collider
        if (other.CompareTag("Switch"))
        {
            canActivateSwitch = false;
        }
    }

    void OpenDoor()
    {
        if (door != null)
        {
            Debug.Log("Door is now open!");
            Destroy(door); // Destroy the door
        }
        else
        {
            Debug.LogWarning("Door not assigned!");
        }
    }

    void ActivateSwitch()
    {
        if (switchObject != null)
        {
            switchObject.SetActive(true); // Activate the switch (show it in the scene)
            Debug.Log("Switch activated! Press 'E' to open the door.");
        }
        else
        {
            Debug.LogWarning("Switch object is not assigned!");
        }
    }
}
