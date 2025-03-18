using UnityEngine;
using UnityEngine.AI;

public class ChaserAI : MonoBehaviour
{
    public Transform target; // The bubble to chase
    public Animator animator; // Reference to the Animator component
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);

            // Update animation
            if (agent.velocity.magnitude > 0.1f)
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
