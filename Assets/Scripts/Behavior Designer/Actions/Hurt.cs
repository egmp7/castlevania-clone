using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class Hurt : EnemyAction
    {
        public override TaskStatus OnUpdate()
        {
            rb.velocity = Vector2.zero;
            animator.Play("Hurt");
            return TaskStatus.Success;
        }
    }
}

