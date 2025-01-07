using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class ChasePlayer : EnemyAction
    {
        public override void OnAwake()
        {
            base.OnAwake();
        }

        public override TaskStatus OnUpdate()
        {
            rb.AddForce(new Vector2(0,5));
            animator.Play("Run");
            return TaskStatus.Success;
        }
    }
}

