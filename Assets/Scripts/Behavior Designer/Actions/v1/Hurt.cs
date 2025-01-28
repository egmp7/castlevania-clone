using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class Hurt : EnemyAction
    {
        public override TaskStatus OnUpdate()
        {
            _rb.velocity = Vector2.zero;
            _animator.Play("Hurt", -1, 0f);
            _healthManager.DecreaseHealth(_processor.GetToAttack().amount);
            return TaskStatus.Success;
        }
    }
}

