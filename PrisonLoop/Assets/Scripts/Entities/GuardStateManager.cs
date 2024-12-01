using UnityEngine;

public class GuardStateManager : EntityStateManager
{
    [SerializeField] private Transform[] pathRoute;
    [SerializeField] public float TimeToLookAround;
    private int currentPathPoint;
    public EntityMovement entityMovement { get; set; }
    

    void Start()
    {
        entityMovement = GetComponent<EntityMovement>();
        currentPathPoint = 0;
        
        currentState = new PatrolState(this, pathRoute[0]);
        
    }

    public Transform GetNextPathPoint()
    {
        if (currentPathPoint == pathRoute.Length - 1)
        {
            currentPathPoint = 0;
        }
        else
        {
            currentPathPoint++;
        }

        return pathRoute[currentPathPoint];
    }
    
    public float GetTimeToLookAround() => TimeToLookAround;
    

}
