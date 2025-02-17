using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace egmp7.BehaviorDesigner.Enemy
{
    [TaskIcon("Assets/Art/egmp7/customAction.png")]
    [TaskCategory("egmp7")]
    [TaskDescription("Sets the scale depending on the target object")]
    public class LookAtByTag : Action
    {
        private GameObject target;
        public SharedString targetTag = "Player";

        public override void OnAwake()
        {
            ValidateFields();
        }

        public override TaskStatus OnUpdate()
        {
            if (target == null) return TaskStatus.Inactive;

            var direction = gameObject.transform.position - target.transform.position;

            if (direction.x < 0)
            {
                gameObject.transform.localScale = new Vector3(
                    1, 
                    gameObject.transform.localScale.y, 
                    gameObject.transform.localScale.z);
            }

            if (direction.x > 0)
            {
                gameObject.transform.localScale = new Vector3(
                    -1, 
                    gameObject.transform.localScale.y, 
                    gameObject.transform.localScale.z);
            }

            return TaskStatus.Success;
        }

        private void ValidateFields()
        {
            var logActionName = $"Action: {FriendlyName}";

            target = GameObject.FindGameObjectWithTag(targetTag.Value);
            if (target == null)
            {
                ErrorManager.LogMissingGameObjectWithTag(targetTag.Value, logActionName);
            }
        }
    }
}
