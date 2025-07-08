using UnityEngine;
using UnityEngine.AI;

public class ChaserAI : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;                // The bubble to chase
    public float followDistance = 10f;      // Maximum distance to follow from
    
    [Header("Movement Settings")]
    public float moveSpeed = 3.5f;          // Movement speed
    public float rotationSpeed = 120f;      // Turning speed (deg/sec)
    public float stoppingDistance = 0.5f;   // Distance to stop moving
    
    [Header("Game Over Settings")]
    public float killDistance = 0.5f;       // Distance for game over
    public GameObject gameOverUI;
    public KeyCode restartKey = KeyCode.R;

    private NavMeshAgent agent;
    private bool isGameOver = false;
    private Vector3 groundTargetPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = rotationSpeed;
        agent.stoppingDistance = stoppingDistance;
        agent.updateUpAxis = false; // Important for ground movement
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(restartKey))
            {
                RestartGame();
            }
            return;
        }

        if (target == null) return;

        // Project target position to ground level (XZ only)
        groundTargetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);

        // Only follow if within range
        if (Vector3.Distance(transform.position, groundTargetPosition) <= followDistance)
        {
            agent.SetDestination(groundTargetPosition);
        }
        else
        {
            agent.ResetPath();
        }

        // Check for game over condition
        if (Vector3.Distance(transform.position, target.position) <= killDistance)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        isGameOver = true;
        agent.isStopped = true;
        
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    void OnDrawGizmosSelected()
    {
        // Draw follow range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, followDistance);
        
        // Draw kill radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, killDistance);
        
        // Draw current target position
        if (target != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, groundTargetPosition);
            Gizmos.DrawSphere(groundTargetPosition, 0.2f);
        }
    }
}