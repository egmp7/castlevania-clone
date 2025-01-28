using Enemy.AI;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class ChasePause : EnemyAction
    {
        public override void OnStart()
        {
            _animator.Play("Combat Idle");
            _rb.velocity = Vector2.zero;
        }
    }
}

