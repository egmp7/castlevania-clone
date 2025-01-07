using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class Idle : EnemyAction
    {
        public override TaskStatus OnUpdate()
        {
            rb.velocity = Vector2.zero;
            animator.Play("Idle");
            return TaskStatus.Success;
        }
    }
}

