using Game.AnimationEvent.Source;
using Player.StateManagement;
using UnityEngine;

namespace Game.AnimationEvent
{
    [RequireComponent(typeof(StateMachine))]

    public class PlayerAnimationHandler : AnimationHandler
    {
        private StateMachine _stateMachine;
        private DamageProcessor _damageProcessor;

        private void Awake()
        {
            _stateMachine = GetComponent<StateMachine>();
            if (_stateMachine == null)
            {
                Debug.LogError("StateMachine component is missing on this GameObject.");
            }

            _damageProcessor = GetComponent<DamageProcessor>();
            if (_damageProcessor == null)
            {
                Debug.LogError("DamageProcessor component is missing on this GameObject.");
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

