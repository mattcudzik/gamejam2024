using UnityEngine;
using UnityEngine.AI;

namespace Entities.States
{
    public class IdleState : IState
    {
        private PrisonerStateManager _stateManager;
        
        private float radius = 4f;
        private float idleWalkSpeed = 1f;
        private float waitingTime = 8f;
        
        private Transform _transform;
        private EntityMovement _entityMovement;

        private GameObject WalkDestination;
        private bool isWaiting = false;
        private float startWaitingTime;
        
        
        public IdleState(PrisonerStateManager stateManager)
        {
            _stateManager = stateManager;
            _transform = _stateManager.transform;
            _entityMovement = _stateManager.entityMovement;
            
            //slow down idle prisoners
            _entityMovement.agent.speed = idleWalkSpeed;
        }
        public override void UpdateState()
        {
            if (isWaiting)
            {
                if (startWaitingTime + waitingTime < _stateManager.timer.CurrentTime)
                {
                    isWaiting = false;
                    MoveToRandomPoint();
                }
            }
            else
            {
                if (_entityMovement.HasReachedTarget())
                {
                    StartWaiing();
                }
            }
            
        }

        private void MoveToRandomPoint()
        {
            Vector3 randomPoint = Random.onUnitSphere * radius;
            randomPoint += _transform.position;
        
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, radius, NavMesh.AllAreas))
            {
                _entityMovement.SetTarget(hit.position);
                _entityMovement.StartAgent();
            }
        }

        private void StartWaiing()
        {
            isWaiting = true;
             startWaitingTime = _stateManager.timer.CurrentTime;
            
            _entityMovement.StopAgent();
        }
        public override void EnterState()
        {
            MoveToRandomPoint();
        }

        public override void ExitState()
        {
            //reset speed
            _entityMovement.agent.speed = 3.5f;
        }
    }
}