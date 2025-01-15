using BehaviorDesigner.Runtime.Tasks;
using static egmp7.BehaviorDesigner.CustomVariables;

namespace Enemy.AI
{
    public class WithinSight : Conditional
    {
        public SharedCollisionSensor sharedCollider;

        public override TaskStatus OnUpdate()
        {
            if(sharedCollider.Value.GetState())
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}