using UnityEngine;
using UnityEngine.AI;

public class EntityMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = true;
    }

    public bool HasReachedTarget()
    {
        if (agent == null || !agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void StopAgent()
    {
        agent.isStopped = true;
    }

    public void StartAgent()
    {
        agent.isStopped = false;
    }

    public void SetTarget(Transform target)
    {
        agent.SetDestination(target.position);
    }
    
}
