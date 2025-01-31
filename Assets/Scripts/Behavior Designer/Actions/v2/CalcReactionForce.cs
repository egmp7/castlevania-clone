using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace egmp7.BehaviorDesigner.Enemy
{
    [TaskIcon("Assets/Art/customAction.png")]
    [TaskCategory("egmp7")]
    [TaskDescription("Calculates ReactionForce with proximity scaling and controlled output.")]
    public class CalcReactionForce : EnemyAction
    {
        [Tooltip("The updated reaction force")]
        public SharedVector2 reactionForce;

        [Tooltip("The minimum distance at which the force begins to scale up")]
        public float minDistance = 1.0f;

        [Tooltip("The maximum distance at which the force stops scaling")]
        public float maxDistance = 10.0f;

        [Tooltip("The base magnitude of the reaction force")]
        public float baseForceMagnitude = 32.0f;

        [Tooltip("The maximum multiplier for the force based on proximity")]
        public float maxMultiplier = 2.0f;

        public override TaskStatus OnUpdate()
        {
            if (_player == null)
            {
                return TaskStatus.Failure;
            }

            // Calculate the direction vector between this GameObject and the player
            Vector2 direction = new Vector2(
                transform.position.x - _player.transform.position.x,
                0 // Lock the Y-component to zero for horizontal force only
            );

            // Clamp the X-component to -1 or 1
            float clampedX = Mathf.Clamp(direction.x, -1f, 1f);
            Vector2 normalizedDirection = new Vector2(clampedX, 0);

            // Calculate the distance between objects
            float distance = Mathf.Abs(direction.x); // Use horizontal distance only

            // Determine the multiplier based on proximity
            float multiplier = 1.0f;
            if (distance <= maxDistance)
            {
                if (distance > minDistance)
                {
                    // Linearly interpolate multiplier from 1 to maxMultiplier
                    multiplier = Mathf.Lerp(1.0f, maxMultiplier, 1.0f - ((distance - minDistance) / (maxDistance - minDistance)));
                }
                else
                {
                    // If closer than minDistance, apply maxMultiplier directly
                    multiplier = maxMultiplier;
                }
            }

            // Calculate the final force magnitude
            float finalForceMagnitude = baseForceMagnitude * multiplier;

            // Set the reaction force
            reactionForce.Value = normalizedDirection * finalForceMagnitude;

            return TaskStatus.Success;
        }
    }
}
