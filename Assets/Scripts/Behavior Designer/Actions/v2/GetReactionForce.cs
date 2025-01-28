using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace egmp7.BehaviorDesigner.Enemy
{
    [TaskIcon("Assets/Art/customAction.png")]
    [TaskCategory("egmp7")]
    [TaskDescription("Calculates ReactionForce")]
    public class GetReactionForce : EnemyAction
    {
        [Tooltip("The updated reaction force")]
        public SharedVector2 reactionForce;

        [Tooltip("The distance at which the lerping starts to increase the magnitude of the force")]
        public float closeDistance = 1.0f;

        [Tooltip("The multiplier for the reaction force magnitude when objects are very close")]
        public float closeDistanceMultiplier = 2.0f;

        public float forceMagnitud = 64.0f;

        public override TaskStatus OnUpdate()
        {
            if (_player == null)
            {
                return TaskStatus.Failure;
            }

            // Calculate the direction vector between this GameObject and the player
            Vector3 direction = transform.position - _player.transform.position;
            float distance = direction.magnitude;

            // Normalize the direction to get only the direction and no magnitude
            Vector2 normalizedDirection = new (Mathf.Sign(direction.x),0);

            // Lerp the magnitude based on the distance
            float magnitude = forceMagnitud;
            if (distance < closeDistance)
            {
                magnitude = Mathf.Lerp(1.0f, closeDistanceMultiplier, 1.0f - (distance / closeDistance)) * forceMagnitud;
            }

            // Set the reaction force
            reactionForce.Value = normalizedDirection * magnitude;

            return TaskStatus.Success;
        }
    }
}
