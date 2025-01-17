using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using egmp7.Game.Manager;
using System;
using UnityEngine;

namespace Enemy.AI
{
    public class ChaseAction : EnemyAction
    {
        public SharedFloat speed = 5f;
        public SharedFloat slope = 0.1f;

        public override void OnAwake()
        {
            base.OnAwake();
            if (slope.Value < 0)
            {
                throw new Exception("Slope can not be a negative value");
            }
        }

        public override TaskStatus OnUpdate()
        {
            #region Move Logic
            // Calculate direction to player, ignoring vertical movement
            Vector2 targetPosition = new (_playerTransform.position.x, _rb.position.y);
            Vector2 direction = (targetPosition - _rb.position).normalized;

            // Update speed over time
            var currentSpeed = speed.Value + ( MainManager.Instance.difficulty * slope.Value) ;

            // Move the Rigidbody2D horizontally towards the player
            _rb.velocity = new Vector2(direction.x * currentSpeed, _rb.velocity.y);
            #endregion

            // Play the "Run" animation
            _animator.Play("Run");

            // Debug.Log($"Current Speed: {currentSpeed}");
            return TaskStatus.Running;
        }
    }
}

