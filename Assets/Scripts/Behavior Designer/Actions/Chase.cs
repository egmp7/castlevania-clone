using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using egmp7.Game.Manager;
using UnityEngine;

namespace Enemy.AI
{
    public class ChaseAction : EnemyAction
    {
        public SharedFloat initChaseSpeed = 5f;
        public SharedFloat chaseSpeedRatio = 0.1f;

        public override TaskStatus OnUpdate()
        {
            #region Move Logic
            // Calculate direction to player, ignoring vertical movement
            Vector2 targetPosition = new (_playerTransform.position.x, _rb.position.y);
            Vector2 direction = (targetPosition - _rb.position).normalized;

            // Update speed over time
            var currentSpeed = initChaseSpeed.Value + ( MainManager.Instance.difficulty * chaseSpeedRatio.Value) ;

            // Move the Rigidbody2D horizontally towards the player
            _rb.velocity = new Vector2(direction.x * currentSpeed, _rb.velocity.y);
            #endregion

            Debug.Log($"Current Speed: {currentSpeed}");
            // Play the "Run" animation
            _animator.Play("Run");

            return TaskStatus.Success;
        }
    }
}

