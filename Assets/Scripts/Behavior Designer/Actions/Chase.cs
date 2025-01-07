using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class ChaseAction : EnemyAction
    {
        public float moveSpeed = 5f; 

        public override TaskStatus OnUpdate()
        {
            #region Move Logic
            // Calculate direction to player, ignoring vertical movement
            Vector2 targetPosition = new Vector2(playerTransform.position.x, rb.position.y);
            Vector2 direction = (targetPosition - rb.position).normalized;

            // Move the Rigidbody2D horizontally towards the player
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            #endregion

            // Play the "Run" animation
            animator.Play("Run");

            return TaskStatus.Success;
        }
    }
}

