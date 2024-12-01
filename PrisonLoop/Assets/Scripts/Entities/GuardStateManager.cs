using Entities;
using Entities.States;
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
        
        CauseDistractionState.onFightStart += OnFightStart;
        CauseDistractionState.onFightEnd += OnFightEnd;
    }

    private void OnFightEnd()
    {
        SwitchState(new PatrolState(this, GetNextPathPoint()));
    }

    private void OnFightStart(Transform prisoner)
    {
        SwitchState(new GetDistractedState(this, prisoner));
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
