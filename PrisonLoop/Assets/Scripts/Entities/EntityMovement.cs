using System;
using UnityEngine;
using UnityEngine.AI;

public class EntityMovement : MonoBehaviour
{
    public NavMeshAgent agent { get; set; }

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private void FixedUpdate()
    {
        Vector2 velocity = agent.velocity;

        if (velocity.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        
        if(velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = true;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
    
    public void SetTarget(Vector3 target)
    {
        agent.SetDestination(target);
    }
    
}
