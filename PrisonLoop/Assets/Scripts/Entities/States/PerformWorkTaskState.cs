using UnityEngine;

namespace Entities.States
{
    public class PerformWorkTaskState : IState
    {

        private Transform _workStationTransform;
        private PrisonerStateManager _stateManager;
        private EntityMovement _entityMovement;
        private Timer _timer;

        private float _timeToPerformTask = 3f;
        private float _timeStartedPerformingTask = 0f;
        
        private bool startedPerformingTask = false;
        
        public PerformWorkTaskState(PrisonerStateManager stateManager)
        {
            _workStationTransform = GameManager.Instance.LevelManagerInstance.GetMiniGameBox().gameObject.transform;
            _stateManager = stateManager;
            _entityMovement = _stateManager.entityMovement;
            _timer = GameManager.Instance.Timer;
        }
        public override void UpdateState()
        {
            if (_entityMovement.HasReachedTarget())
            {
                if (!startedPerformingTask)
                {
                    startedPerformingTask = true;
                    _timeStartedPerformingTask = _timer.CurrentTime;
                }

                if (_timer.CurrentTime >= _timeToPerformTask + _timeStartedPerformingTask)
                {
                    GameManager.Instance.IsSceneWorkDone = true;
                    _workStationTransform.gameObject.GetComponent<InteractableBox>().interactable= false;
                    _stateManager.SwitchState(new IdleState(_stateManager));
                }
            }
        }

        public override void EnterState()
        {
            _entityMovement.SetTarget(_workStationTransform);
            _entityMovement.StartAgent();
        }

        public override void ExitState()
        {
            
        }
    }
}