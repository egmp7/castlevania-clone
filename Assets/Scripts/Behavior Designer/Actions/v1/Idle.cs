using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class Idle : EnemyAction
    {
        public override TaskStatus OnUpdate()
        {
            _rb.velocity = Vector2.zero;
            _animator.Play("Idle");
            return TaskStatus.Success;
        }
    }
}

