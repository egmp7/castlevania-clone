using BehaviorDesigner.Runtime.Tasks;
using egmp7.Game.Manager;
using UnityEngine;

namespace Enemy.AI
{
    public class Die : EnemyAction
    {
        public override TaskStatus OnUpdate()
        {
            _rb.velocity = Vector2.zero;
            _animator.Play("Die");
            _rb.bodyType = RigidbodyType2D.Kinematic;
            _collider.enabled = false;
            //gameObject.layer = LayerMask.NameToLayer("Dead");
            return TaskStatus.Success;
        }
    }
}


