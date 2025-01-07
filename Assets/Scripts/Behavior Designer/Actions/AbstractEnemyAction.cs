using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Game.AnimationEvent.Source;

namespace Enemy.AI
{
    public abstract class EnemyAction : Action
    {
        protected Rigidbody2D rb;
        protected Animator animator;
        protected Transform playerTransform;
        protected DamageProcessor processor;

        public override void OnAwake()
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null)
            {
                Debug.LogError("Rigidbody2D is not assigned.");
            }

            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator is not assigned.");
            }

            processor = GetComponent<DamageProcessor>();
            if (processor == null)
            {
                Debug.LogError("DamageProcessor is not assigned.");
            }

            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            if (playerTransform == null)
            {
                Debug.LogError("PlayerTransform is not assigned.");
            }
        }
    }
}
