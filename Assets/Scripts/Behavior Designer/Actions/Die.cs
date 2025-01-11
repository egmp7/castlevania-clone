using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class Die : EnemyAction
    {
        public override TaskStatus OnUpdate()
        {
            _rb.velocity = Vector2.zero;
            _animator.Play("Die");
            return TaskStatus.Success;
        }
    }
}


