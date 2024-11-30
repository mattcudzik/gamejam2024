using UnityEngine;
using UnityEngine.AI;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private Transform target;

    private NavMeshAgent agent;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        
    }

    void Update()
    {
        agent.SetDestination(target.position);
    }
    
}
