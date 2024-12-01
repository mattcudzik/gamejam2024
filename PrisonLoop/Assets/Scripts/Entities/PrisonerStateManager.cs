using Entities.States;
using UnityEngine;
using UnityEngine.AI;

namespace Entities
{
    public class PrisonerStateManager : EntityStateManager
    {
        [SerializeField] private NavMeshData _meshData;
        [SerializeField] private bool isStandingStill = false;
        public EntityMovement entityMovement { get; set; }
        public Timer timer { get; set; }

        private void Start()
        {
            entityMovement = GetComponent<EntityMovement>();
            timer = GameManager.Instance.Timer;

            if (isStandingStill)
            {
                currentState = new StandingStillState();
            }
            else
            {
                currentState = new IdleState(this);
                currentState.EnterState();
            }
            
            PrisonerHelper ph = GetComponent<PrisonerHelper>();
            if(ph!=null) ph.OnHelpBought += PerformHelpState;
        }

        private void PerformHelpState(HelpOffer  helpOffer)
        {
            
            switch (helpOffer.helpOption)
            {
                case HelpEnum.Distraction:
                    
                    break;
                case HelpEnum.Work:
                    SwitchState(new PerformWorkTaskState(this));
                    break;
            }
        }

        public NavMeshData MeshData => _meshData;

    }
}