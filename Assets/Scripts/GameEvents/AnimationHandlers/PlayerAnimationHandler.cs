using Game.AnimationEvent.Source;
using Player.StateManagement;

namespace Game.AnimationEvent
{

    public class PlayerAnimationHandler : AnimationHandler
    {
        private StateMachine _stateMachine;
        private DamageProcessor _damageProcessor;

        private void Awake()
        {
            if (!TryGetComponent(out _stateMachine))
            {
                ErrorManager.LogMissingComponent<StateMachine>(gameObject);
            }

            if (!TryGetComponent(out _damageProcessor))
            {
                ErrorManager.LogMissingComponent<DamageProcessor>(gameObject);
            }
        }

        /// <summary>
        /// Handles the end of an animation by transitioning the State Machine to the idle state.
        /// </summary>
        public override void HandleAnimationEnd()
        {
            if (_stateMachine != null)
            {
                _stateMachine.Idle();
            }
        }

        /// <summary>
        /// Handles an attack animation event by triggering damage processing.
        /// </summary>
        public override void HandleAnimationAttack()
        {
            if (_damageProcessor != null)
            {
                _damageProcessor.PerformAttackDetection();
            }
        }
    }
}

