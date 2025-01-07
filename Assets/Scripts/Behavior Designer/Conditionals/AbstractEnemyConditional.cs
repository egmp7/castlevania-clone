using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public abstract class EnemyConditional : Conditional
    {
        protected Animator animator;

        public override void OnAwake()
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator is not assigned.");
            }
        }
    }
}