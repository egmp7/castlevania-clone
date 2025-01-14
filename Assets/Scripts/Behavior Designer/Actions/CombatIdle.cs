using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class CombatIdle : EnemyAction
    {
        public SharedBool isCombatIdle;

        public override TaskStatus OnUpdate()
        {
            if (isCombatIdle.Value)
            {
                _rb.velocity = Vector2.zero;
                _animator.Play("Combat Idle");
                isCombatIdle.SetValue(false);
                return TaskStatus.Success;
            }else
            {
                return TaskStatus.Failure;
            }
        }
    }
}