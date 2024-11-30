using UnityEngine;

public class GuardStateManager : EntityStateManager
{
    [SerializeField] private Transform[] pathRoute;
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
    
    

}
