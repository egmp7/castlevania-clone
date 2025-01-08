using Game.AnimationEvent.Source;
using UnityEngine;

namespace Game.AnimationEvent
{
    [RequireComponent(typeof(Animator))]
    
    public class EnemyAnimationHandler : AnimationHandler
    {
        private Animator _animator;
        private DamageProcessor _damageProcessor;

        [SerializeField, Tooltip("Name of the idle animation state.")]
        private string idleStateName = "Idle";

        private void Awake()
        {
            // Cache references to required components
            _animator = GetComponent<Animator>();
            if (_animator == null)
            {
                Debug.LogError("Animator component is missing on this GameObject.");
            }

            _damageProcessor = GetComponent<DamageProcessor>();
            if (_damageProcessor == null)
            {
                Debug.LogError("DamageProcessor component is missing on this GameObject.");
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
    }
}
