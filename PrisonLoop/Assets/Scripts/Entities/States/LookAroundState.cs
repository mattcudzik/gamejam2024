using UnityEngine;

namespace Entities.States
{
    public class LookAroundState : IState
    {
        private float TimeToLookAround;
        private Timer timer;
        private float startTime;
        private GuardStateManager _guardStateManager;


        public LookAroundState(GuardStateManager stateManager, float timeToLookAround)
        {
            _guardStateManager = stateManager;
            TimeToLookAround = timeToLookAround;
        }
        public override void UpdateState()
        {
            if (timer.CurrentTime > startTime + TimeToLookAround)
            {
                _guardStateManager.SwitchState(new PatrolState(_guardStateManager, _guardStateManager.GetNextPathPoint()));
            }
        }

        public override void EnterState()
        {
            timer = GameManager.Instance.Timer;
            startTime = timer.CurrentTime;
        }

        public override void ExitState()
        {
            
        }
    }
}