using UnityEngine;
using UnityEngine.AI;

public class ChaserAIWithAnimation : MonoBehaviour
{
    public Transform bubble; // Reference to the bubble
    public float gameOverDistance = 1f; // Distance at which the AI triggers Game Over
    public Animator animator; // Reference to the Animator
    private NavMeshAgent agent; // NavMesh Agent for movement

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (animator == null)
        {
            Debug.LogError("Animator not assigned to ChaserAIWithAnimation!");
        }
    }

    void Update()
    {
        if (bubble != null)
        {
            // Update agent destination
            agent.SetDestination(bubble.position);

            // Check if AI is close to the bubble
            if (Vector3.Distance(transform.position, bubble.position) <= gameOverDistance)
            {
                TriggerGameOver();
            }
            else
            {
                // Trigger walking animation based on agent's movement
                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    animator.SetBool("isWalking", true);
                }
                else
                {
                    animator.SetBool("isWalking", false);
                }
            }
        }
    }

    void TriggerGameOver()
    {
        Debug.Log("Game Over! The chaser caught the bubble.");
        GameManager.instance.GameOver();

        // Stop agent movement
        agent.isStopped = true;

        // Trigger stop animation
        animator.SetBool("isWalking", false);
    }
}
