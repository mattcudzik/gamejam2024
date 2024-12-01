using UnityEngine;

namespace Entities.States
{
    public class CatchPlayerState : IState
    {
        
        private GuardStateManager _guardStateManager;
        private EntityMovement _entityMovement;
        
        public CatchPlayerState(GuardStateManager guardStateManager)
        {
            _guardStateManager = guardStateManager;
            _entityMovement = _guardStateManager.entityMovement;
        }
        public override void UpdateState()
        {
            if (_entityMovement.HasReachedTarget())
            {
                //END GAME
                Debug.Log("GAME OVER");
            }
        }

        public override void EnterState()
        {
            _entityMovement.SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
            _entityMovement.StartAgent();
        }

        public override void ExitState()
        {
            
        }
    }
}