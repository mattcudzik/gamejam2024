using UnityEngine;
using UnityEngine.AI;

namespace Entities.States
{
    public class IdleState : IState
    {
        private PrisonerStateManager _stateManager;
        public float radius = 10f;
        private Transform _transform;
        private EntityMovement _entityMovement;

        private GameObject WalkDestination;
        
        public IdleState(PrisonerStateManager stateManager)
        {
            _stateManager = stateManager;
            _transform = _stateManager.transform;
        }
        public override void UpdateState()
        {
            
        }
        
        

        public void MoveToRandomPoint()
        {
            Vector3 randomPoint = Random.insideUnitSphere * radius;
            randomPoint += _transform.position;
        
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas)) // Check if the point is on the NavMesh
            {
                _entityMovement.SetTarget(hit.position);
                _entityMovement.StartAgent();
            }
        }
        public override void EnterState()
        {
            MoveToRandomPoint();
        }

        public override void ExitState()
        {
            
        }
    }
}