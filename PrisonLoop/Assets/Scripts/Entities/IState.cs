using UnityEngine;

public abstract class IState
{
    public abstract void UpdateState();
    public abstract void EnterState();
    public abstract void ExitState();
    
}
