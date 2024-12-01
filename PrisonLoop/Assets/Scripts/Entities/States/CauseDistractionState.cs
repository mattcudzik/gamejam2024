using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Entities.States
{
    public class CauseDistractionState : IState
    {
        private PrisonerStateManager _stateManager;
        private EntityMovement _entityMovement;
        private GameObject otherPrisoner;
        private Timer _timer;

        private bool startedFighting = false;
        private float fightingTime = 15f;
        private float startFightingTime = 0;

        public static Action<Transform> onFightStart;
        public static Action onFightEnd;
        
        public CauseDistractionState(PrisonerStateManager stateManager)
        {
            _stateManager = stateManager;
            _entityMovement = _stateManager.entityMovement;
            otherPrisoner = getRandomPrisoner();
            _timer = GameManager.Instance.Timer;

        }
        public override void UpdateState()
        {
            if (_entityMovement.HasReachedTarget())
            {
                if (!startedFighting)
                {
                    startedFighting = true;
                    startFightingTime = _timer.CurrentTime;
                    onFightStart.Invoke(_stateManager.transform);
                }

                if (_timer.CurrentTime >= startFightingTime + fightingTime)
                {
                    _stateManager.SwitchState(new IdleState(_stateManager));
                    onFightEnd.Invoke();
                }
            }
        }

        public override void EnterState()
        {
            _entityMovement.SetTarget(otherPrisoner.transform);
            _entityMovement.StartAgent();
        }

        public override void ExitState()
        {
            
        }

        private GameObject getRandomPrisoner()
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Prisoner");
            List<GameObject> prisoners =  objects.ToList();
            
            GameObject randomPrisoner;
            int index;
            do
            {
                index = Random.Range(0, prisoners.Count);
                randomPrisoner = prisoners.ElementAt(index);
                prisoners.RemoveAt(index);
            }
            while(randomPrisoner == _stateManager.gameObject || !randomPrisoner.GetComponent<PrisonerStateManager>().isStandingStill); 
            
            return randomPrisoner;
        }
    }
}