using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GuardFOV : MonoBehaviour
{
    public float viewRadius = 5f; // Detection range
    public float viewAngle = 90f; // Field of view angle
    [SerializeField] public Transform target; // Reference to the player or target
    [SerializeField] public Tilemap wallTilemap; // Reference to the tilemap with walls
    private EntityMovement _entityMovement;

    [SerializeField] private float rotationSpeed = 5f;

    [SerializeField] private  Vector2 viewDirection = Vector2.up;


    private float t = 0f;

    private void Start()
    {
        _entityMovement = GetComponent<EntityMovement>();
    }

    void Update()
    {
        if (IsTargetInFieldOfView())
        {
            Debug.Log("Target detected!");
            // Add your behavior here, like chasing the player
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
