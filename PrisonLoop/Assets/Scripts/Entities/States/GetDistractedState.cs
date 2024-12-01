using UnityEngine;
using UnityEngine.AI;

namespace Entities.States
{
    public class GetDistractedState : IState
    {
        private GuardStateManager _stateManager;
        private Transform target;
        private EntityMovement _entityMovement;
        
        private float radius = 4f;
        
        public GetDistractedState(GuardStateManager manager, Transform target)
        {
            _stateManager = manager;
            _entityMovement = manager.entityMovement;
            this.target = target;
        }
        public override void UpdateState()
        {
            
        }

        public override void EnterState()
        {
            MoveToClosebyPosition();
        }

        public override void ExitState()
        {
            _entityMovement.StopAgent();
        }
        
        private void MoveToClosebyPosition()
        {
            Vector3 randomPoint = Random.onUnitSphere * radius;
            randomPoint += target.position;
        
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
            {
                _entityMovement.SetTarget(hit.position);
                _entityMovement.StartAgent();
            }
        }
    }
}