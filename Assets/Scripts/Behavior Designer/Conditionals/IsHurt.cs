using BehaviorDesigner.Runtime.Tasks;

using UnityEngine;

namespace Enemy.AI
{
    public class IsHurt : EnemyConditional
    {
        public override TaskStatus OnUpdate()
        {
            // Get the current state information from layer 0 (default layer)
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Check if the current state's name is "Idle"
            if (stateInfo.IsName("Hurt"))
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}