using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Enemy.AI
{
    public class FaceTarget : EnemyAction
    {
        private float baseScaleX;

        public override void OnAwake()
        {
            base.OnAwake();
            baseScaleX = Mathf.Abs(transform.localScale.x); // Ensure baseScaleX is always positive
        }

        public override TaskStatus OnUpdate()
        {
            Vector3 scale = transform.localScale; // Get the current local scale

            // Determine the desired direction based on player's position
            float desiredScaleX = transform.position.x > _playerTransform.position.x ? -baseScaleX : baseScaleX;

            // Update only if the scale.x is different from desiredScaleX
            if (Mathf.Abs(scale.x - desiredScaleX) > Mathf.Epsilon)
            {
                scale.x = desiredScaleX;
                transform.localScale = scale; // Assign the modified scale back
            }

            return TaskStatus.Success;
        }
    }


}

