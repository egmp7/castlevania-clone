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
            animator = GetComponent<Animator>();
            processor = GetComponent<DamageProcessor>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
