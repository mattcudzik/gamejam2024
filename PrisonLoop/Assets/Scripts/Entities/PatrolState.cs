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
            _stateManager.SwitchState(new PatrolState(_stateManager, _stateManager.GetNextPathPoint()));
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
