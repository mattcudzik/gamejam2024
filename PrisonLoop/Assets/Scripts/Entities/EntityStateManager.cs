using UnityEngine;

public class EntityStateManager : MonoBehaviour
{

    protected IState currentState;
    
    
    void FixedUpdate()
    {
        currentState.UpdateState();
    }

    public void SwitchState(IState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
