using BehaviorDesigner.Runtime.Tasks;

using UnityEngine;

namespace Enemy.AI
{
    public class IsHurt : EnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            Debug.Log(GetCurrentAnimationName());
            return TaskStatus.Success;
        }

        /// <summary>
        /// Gets the name of the current animation playing on the specified Animator layer.
        /// </summary>
        /// <param name="layerIndex">The Animator layer index. Defaults to 0 (Base Layer).</param>
        /// <returns>The name of the current animation, or an empty string if no animation is playing.</returns>
        private string GetCurrentAnimationName(int layerIndex = 0)
        {
            if (animator == null)
            {
                Debug.LogWarning("Animator is not assigned.");
                return string.Empty;
            }

            // Get the current AnimatorStateInfo for the specified layer
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layerIndex);

            // Check if the layer is playing any animation
            if (stateInfo.length > 0)
            {
                return stateInfo.IsName("") ? "No Animation" : stateInfo.shortNameHash.ToString();
            }

            return "No animation found";
        }
    }
}