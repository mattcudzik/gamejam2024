using System;
using Entities.States;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GuardFOV : MonoBehaviour
{
    public float viewRadius = 5f;
    public float viewAngle = 90f;
    private Transform target;
    [SerializeField] public Tilemap wallTilemap;
    private EntityMovement _entityMovement;
    private GuardStateManager _guardStateManager;
    
    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private  Vector2 viewDirection = Vector2.up;

    //Caught logic
    public static Action onCaught;
    
    [SerializeField] private float caughtTime = 3f;
    private float startCaughtTimer = 0f;
    
    private void Start()
    {
        _entityMovement = GetComponent<EntityMovement>();
        _guardStateManager = GetComponent<GuardStateManager>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (IsTargetInFieldOfView())
        {
            if (contrabandCheck())
            {
                onCaught.Invoke();
                _guardStateManager.SwitchState(new CatchPlayerState(_guardStateManager));
            }
        }

        Vector2 velocity = _entityMovement.agent.velocity.normalized;
        
        if (velocity != Vector2.zero)
        {
            viewDirection = velocity;
        }
        else
        {
            
            viewDirection = RotateVector2(viewDirection, rotationSpeed * Time.deltaTime);
        }
         
    }

    private bool contrabandCheck()
    {
        //TODO implement contraband check
        //1. check if player has contraband in inventory startTimer 
        if (GameManager.Instance.PlayerEq.ContainsContraband())
            return true;
        //2. if digidig 
        var player = target.gameObject.GetComponent<PlayerMovement>();
        if(player != null && player.IsPlayerDigging())
            return true;

        return false;
    }

    private bool IsTargetInFieldOfView()
    {
        if (target == null)
            return false;

        // Calculate direction and angle to the target
        Vector2 directionToTarget = (target.position - transform.position).normalized;
        float angleToTarget = Vector2.Angle(viewDirection, directionToTarget);

        if (angleToTarget <= viewAngle / 2f)
        {
            float distanceToTarget = Vector2.Distance(transform.position, target.position);

            if (distanceToTarget <= viewRadius)
            {
                // Check for walls in the tilemap blocking the view
                if (!IsWallBlockingView(transform.position, target.position))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool IsWallBlockingView(Vector3 start, Vector3 end)
    {
        Vector3 direction = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        // Cast a ray in discrete steps and check tile collisions
        int steps = Mathf.CeilToInt(distance / 0.1f); // Adjust step size for precision
        for (int i = 0; i <= steps; i++)
        {
            Vector3 point = start + direction * (i * 0.1f); // Step along the line
            Vector3Int tilePos = wallTilemap.WorldToCell(point);

            if (wallTilemap.HasTile(tilePos))
            {
                return true; // A wall blocks the view
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector2 forward = viewDirection;
        Vector2 leftBoundary = Quaternion.Euler(0, 0, -viewAngle / 2) * forward;
        Vector2 rightBoundary = Quaternion.Euler(0, 0, viewAngle / 2) * forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)leftBoundary * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)rightBoundary * viewRadius);
    }
    
    Vector2 RotateVector2(Vector2 vector, float angleDegrees)
    {
        float angleRadians = angleDegrees * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRadians);
        float sin = Mathf.Sin(angleRadians);
        return new Vector2(
            vector.x * cos - vector.y * sin,
            vector.x * sin + vector.y * cos
        );
    }
}
