using egmp7.AI.EventHandlers;
using Game.AnimationEvent.Source;
using UnityEngine;

namespace Game.AnimationEvent
{   
    public class EnemyAnimationHandler : AnimationHandler
    {
        private Animator _animator;
        private DamageProcessor _damageProcessor;
        private BehaviorTreeEventHandler _BTEventHandler;

        [SerializeField, Tooltip("Name of the idle animation state.")]
        private string idleStateName = "Combat Idle";

        private void Awake()
        {
            // Cache references to required components
            if (!TryGetComponent(out _animator))
            {
                ErrorManager.LogMissingComponent<Animator>(gameObject);
            }

            _damageProcessor = GetComponentInParent<DamageProcessor>();
            if (_damageProcessor == null)
            {
                ErrorManager.LogMissingComponent<DamageProcessor>(gameObject);
            }

            _BTEventHandler = GetComponentInParent<BehaviorTreeEventHandler>();
            if (_damageProcessor == null)
            {
                ErrorManager.LogMissingComponent<BehaviorTreeEventHandler>(gameObject);
            }
        }

        /// <summary>
        /// Handles the end of an animation by transitioning the Animator to the idle state.
        /// </summary>
        public override void  HandleAnimationEnd()
        {
            if (_animator != null)
            {
                _animator.Play(idleStateName);
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

        public void HandleAnimationAttackEnd() 
        {
            if (_BTEventHandler != null)
            {
                _BTEventHandler.TriggerAttackEnd();
            }
        }
    }
}
