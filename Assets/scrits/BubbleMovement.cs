using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float floatForce = 5f;
    public float fallSpeed = 2f;
    
    [Header("Bubble Growth")]
    public float sizeIncreaseAmount = 0.1f;
    public float minSize = 0.5f;
    public float maxSize = 3f;
    
    private Rigidbody rb;
    private Camera mainCamera;
    private int bubblesCollected = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        rb.freezeRotation = true;
    }

    void Update()
    {
        MoveBubble();
        FloatOrFall();
    }

    void MoveBubble()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = (mainCamera.transform.forward * vertical) + 
                          (mainCamera.transform.right * horizontal);
        direction.y = 0;
        rb.AddForce(direction.normalized * moveSpeed, ForceMode.Acceleration);
    }

    void FloatOrFall()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(Vector3.down * fallSpeed, ForceMode.Acceleration);
        }
    }

    public void IncreaseSize(float amount)
    {
        bubblesCollected++;
        transform.localScale = Vector3.one * Mathf.Clamp(
            transform.localScale.x + amount,
            minSize,
            maxSize
        );
        rb.mass = transform.localScale.x;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Surface"))
        {
            Debug.Log("Bubble popped! Game over.");
            Destroy(gameObject);
            
            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
        }
    }
}