using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Enemy.AI
{
    public class SetSharedBool : EnemyAction
    {
        public SharedBool SharedBool;
        public bool Value;

        public override void OnAwake()
        {
            if (SharedBool == null)
            {
                ErrorManager.LogMissingSharedVariable<SharedBool>(gameObject);
            }
        }

        public override TaskStatus OnUpdate()
        {

            SharedBool.SetValue(Value);
            return TaskStatus.Success;
        }
    }
}