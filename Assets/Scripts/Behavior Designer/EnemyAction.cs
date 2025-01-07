using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace Enemy.AI
{
    public abstract class EnemyAction : Action
    {
        protected Rigidbody2D rb;
        protected Animator animator;
        protected Transform playerTransform;

        public override void OnAwake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}
