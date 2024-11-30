using Entities.States;
using UnityEngine;

public class PatrolState : IState
{
    
    private GuardStateManager _stateManager;
    private EntityMovement _entityMovement;

    public PatrolState(GuardStateManager stateManager, Transform target)
    {
        _stateManager = stateManager;
        _entityMovement = stateManager.entityMovement;
        _entityMovement.SetTarget(target);
        _entityMovement.StartAgent();
    }
    
    public override void UpdateState()
    {
        //check if point reached 
        if (_entityMovement.HasReachedTarget())
        {
            _stateManager.SwitchState(new LookAroundState(_stateManager, _stateManager.TimeToLookAround));
        }
    }

    public override void EnterState()
    {
        _entityMovement.StartAgent();
    }

    public override void ExitState()
    {
        _entityMovement.StopAgent();
    }
}
