using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class CombatIdle2 : EnemyAction
    {
        public SharedFloat attackChance = 0.01f;
        public SharedFloat minimumWaitTime = 1f;
        private float _lastTimeUsed;

        public override void OnStart()
        {
            _lastTimeUsed = Time.time;
        }

        public override TaskStatus OnUpdate()
        {
            // Check if cooldown has passed and attack chance condition is met
            if (Time.time > (_lastTimeUsed + minimumWaitTime.Value) && Random.value < attackChance.Value)
            {
                _lastTimeUsed = Time.time; // Reset the last time used
                return TaskStatus.Success;
            }
            else
            {
                // Maintain idle state
                _rb.velocity = Vector2.zero;
                _animator.Play("Combat Idle");
                return TaskStatus.Running;
            }
        }
    }
}
