using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using TooltipAttribute = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

namespace egmp7.BehaviorDesigner.Enemy
{
    [TaskIcon("Assets/Art/customAction.png")]
    [TaskCategory("egmp7")]
    [TaskDescription("Checks where enemy is facing")]
    public class IsFacingRight : Action
    {
        [Tooltip("The Transform Local Scale")]
        public SharedVector3 localScale;
        [Tooltip("Variable bool")]
        public SharedBool isFacingRight;

        public override TaskStatus OnUpdate()
        {
            if (localScale == null)
            {
                Debug.LogWarning("SharedVector3 localScale is null");
                return TaskStatus.Failure;
            }

            if (localScale.Value.x > 0)
            {
                isFacingRight.Value = true;
            }
            else
            {
                isFacingRight.Value = false;
            }
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            localScale = null;
        }
    }
}
